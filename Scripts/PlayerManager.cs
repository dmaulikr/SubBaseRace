using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour {

    public float boundaryOffset;
    public Slider airCapacity;
    public Slider movement;
    public AudioSource explosion;
    public Text healthLevelText;
    public GameObject explosionGO;
    public GameObject hitAnimation;
    public Vector3 explosionOffset;

    public float cashAmount;
    public float distanceTravelled;
    public float baseSpeedControlX;
    public float speedControlX;
    public float speedControlY;
    public float airCapacityLevel;
    public int healthLevel;
    public bool gameOverCheck;

    public float currentSliderY;
    public float movementDistance;

    public int coinCount;
    public float distanceTravelledRecord;
    public float totalScoreRecord;

    private WeaponManager wm;

    void Start() {
        airCapacity.value = 1.0f;
        cashAmount = 0.0f;
        boundaryOffset = 1.5f;
        distanceTravelled = 0.0f;
        baseSpeedControlX = 10.0f;
        speedControlX = baseSpeedControlX;
        speedControlY = 5f;
        movement.value = 0.0f;
        gameOverCheck = false;
        explosionOffset = new Vector3(3.0f, -0.5f, 0);
        coinCount = 0;

        if (PlayerPrefs.HasKey("AirCapacityLevel")) { // Get Air Capacity Level - Used To Calcuate Rate Air Capacity Falls Per Second
            airCapacityLevel = PlayerPrefs.GetInt("AirCapacityLevel");
        }
        else {
            airCapacityLevel = 1;
        }

        if (PlayerPrefs.HasKey("HealthLevel")) { // Get Health Level & Set # Of Lives
            healthLevel = PlayerPrefs.GetInt("HealthLevel");
        }
        else {
            healthLevel = 1;
        }

        // Check If Record Values Have Been Set
        if (!PlayerPrefs.HasKey("CoinRecord")) {
            PlayerPrefs.SetInt("CoinRecord", 0);
        }

        if (!PlayerPrefs.HasKey("DistanceTravelledRecord")) {
            PlayerPrefs.SetFloat("DistanceTravelledRecord", 0.0f);
        }

        if (!PlayerPrefs.HasKey("TotalScoreRecord")) {
            PlayerPrefs.SetFloat("TotalScoreRecord", 0.0f);
        }

        movement.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.height - 30, 30.0f); // For Mobile - Set Slider To Screen Height
        airCapacity.interactable = false;
        updateHealth(); // Format Health
        wm = FindObjectOfType<WeaponManager>();
    }

	void Update () {
        if (!gameOverCheck) { // Make Sure Game Is Not Over (From Coroutine)
            if (healthLevel <= 0) { // Health Below 0 So End Game
                StartCoroutine("gameOver");
            }

            airCapacity.value -= (1.0f / (10.0f + (2 * (airCapacityLevel - 1)))) * Time.deltaTime; // Calculates Air Capacity Loss Per Second
            if (airCapacity.value <= 0.0f) { // If Air Is Depleted The End Game
                StartCoroutine("gameOver");
            }
            Vector3 myPosition = transform.position;
            // PC
            //myPosition.y += Input.GetAxis("Vertical") * speedControlY * Time.deltaTime;

            //MOBILE
            currentSliderY = movement.value * Camera.main.orthographicSize;
            movementDistance = currentSliderY - myPosition.y;

            myPosition.y += movementDistance / 12.0f;

            myPosition.x += speedControlX * Time.deltaTime; // Constant X Speed

            if (myPosition.y + boundaryOffset > Camera.main.orthographicSize) { // Check For Upper And Lower Bounds Of The Camera
                myPosition.y = Camera.main.orthographicSize - boundaryOffset;
            }
            if (myPosition.y - boundaryOffset < -Camera.main.orthographicSize) {
                myPosition.y = -Camera.main.orthographicSize + boundaryOffset;
            }

            distanceTravelled = myPosition.x;
            transform.position = myPosition;
        }
	}

    void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Seaweed")) { // Collided With Seaweed
            if (!wm.getIsBoosting()) { // If Not Boosting Set Speed To 1/3
                setSpeedControlX(1.0f / 3.0f);
            }
        }
        else if (!(other.gameObject.CompareTag("AirBubble") || other.gameObject.CompareTag("Coin"))) { // Air Bubble & Coin Handled In Pickup Manager
            if (!other.gameObject.CompareTag("Mine")) { // Player Did Not Collide With Mine - Play Small Collision Animation
                Vector3 offset = transform.rotation * explosionOffset;
                GameObject hitAnimationGO = (GameObject)Instantiate(hitAnimation);
                hitAnimationGO.transform.position = transform.position + explosionOffset;
            }
            healthLevel--; // Decrease Health
            updateHealth();
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.CompareTag("Seaweed")) { // Left Seaweed
            if (!wm.getIsBoosting()) { // If Not Boosting Set Speed To Normal
                setSpeedControlX(1.0f);  
            }
        }
    }

    public void incrementBubbleTankCapacity(float amount) { // Add More To Air Capacity Amount - Called When Air Bubble Collected
        float bubbleTankCapacity = airCapacity.value;
        bubbleTankCapacity += amount; // Get Current Amount & Add New Amount
        if (bubbleTankCapacity > 1.0f) { // If Over 1, Set To 1
            bubbleTankCapacity = 1.0f;
        }
        airCapacity.value = bubbleTankCapacity;
    }

    public void incrementCashAmount(float amount) { // Add More To Cash (Called When Coin Colledted)
        coinCount++; // Used To Compare With Record
        cashAmount += amount;
    }

    public void setSpeedControlX(float newSpeedControl) {
        speedControlX = baseSpeedControlX * newSpeedControl;
    }

    public void runGameOver() {
        StartCoroutine("gameOver");
    }

    public IEnumerator gameOver(){ // Game Over - Placed Inside Coroutine To Pause Game
        gameOverCheck = true;
        Vector3 offset = transform.rotation * explosionOffset; // Spawn Explosion Animation
        GameObject explosionGOI = (GameObject)Instantiate(explosionGO);
        explosionGOI.transform.position = transform.position + explosionOffset;
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1) {
            explosion.Play();
        }
        
        yield return new WaitForSeconds(0.67f); // Pause Coroutine For x Seconds
        PlayerPrefs.SetFloat("LastCash", cashAmount); // Saves Data To Player Prefs
        PlayerPrefs.SetFloat("LastScore", 2 * distanceTravelled);
        float cashValue = PlayerPrefs.GetFloat("CashValue");
        PlayerPrefs.SetFloat("CashValue", cashValue += (cashAmount + (2*distanceTravelled))); // Set New Cash Value

        if ((cashAmount + (2 * distanceTravelled)) > PlayerPrefs.GetFloat("TotalScoreRecord")) { // Checks If Total Score Broke Record, If True, Set New Record, Add 500 & Raise The Flag For Summary Manager
            PlayerPrefs.SetFloat("TotalScoreRecord", (cashAmount + (2 * distanceTravelled)));
            PlayerPrefs.SetFloat("CashValue", cashValue += 500.0f);
            PlayerPrefs.SetInt("BrokeTSR", 1);
        }
        else { // Didn't Break Record, Set As False
            PlayerPrefs.SetInt("BrokeTSR", 0);
        }

        if ((2 * distanceTravelled) > PlayerPrefs.GetFloat("DistanceTravelledRecord")) { // Checks If Distance Score Broke Record, If True, Set New Record, Add 500 & Raise The Flag For Summary Manager
            PlayerPrefs.SetFloat("DistanceTravelledRecord", (2 * distanceTravelled));
            PlayerPrefs.SetFloat("CashValue", cashValue += 500.0f);
            PlayerPrefs.SetInt("BrokeDTR", 1);
        }
        else { // Didn't Break Record, Set As False
            PlayerPrefs.SetInt("BrokeDTR", 0);
        }

        if (coinCount > PlayerPrefs.GetInt("CoinRecord")) { // Checks If Coin Score Broke Record, If True, Set New Record, Add 500 & Raise The Flag For Summary Manager
            PlayerPrefs.SetInt("CoinRecord", coinCount);
            PlayerPrefs.SetFloat("CashValue", cashValue += 500.0f);
            PlayerPrefs.SetInt("BrokeCR", 1);
        }
        else { // Didn't Break Record, Set As False
            PlayerPrefs.SetInt("BrokeCR", 0);
        }

        SceneManager.LoadScene("SummaryMenu");
    }

    public void updateHealth() { // Formats Health Text
        if (healthLevel < 10) {
            healthLevelText.text = "00" + healthLevel.ToString();
        }
        else if (healthLevel < 100) {
            healthLevelText.text = "0" + healthLevel.ToString();
        }
        else {
            healthLevelText.text = healthLevel.ToString();
        }
    }
}
