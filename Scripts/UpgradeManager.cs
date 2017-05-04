using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UpgradeManager : MonoBehaviour {

    public int healthLevel;
    public int airCapacityLevel;
    public int weaponDamageLevel;
    public float cashAmount;

    public float healthUpgradeCost;
    public float airCapacityUpgradeCost;
    public float weaponDamageUpgradeCost;

    public Text healthLevelText;
    public Text airCapacityLevelText;
    public Text weaponDamageLevelText;

    public Text healthCostText;
    public Text airCapacityCostText;
    public Text weaponDamageCostText;

    public Text coinsRecordText;
    public Text distanceTravelledRecordText;
    public Text totalScoreRecordText;

    public Button healthUpgradeBtn;
    public Button airCapacityUpgradeBtn;
    public Button weaponDamageUpgradeBtn;

    public Text cashAmountText;

    public Color greenText;
    public Color redText;

    public Sprite lockedSprite;
    public Sprite plusSprite;
    public AudioSource backgroundSound;
    public AudioSource levelUpSound;

    void Start() {
        // Check & Get All The Keys For Levels And Records
        if (PlayerPrefs.HasKey("HealthLevel")) {
            healthLevel = PlayerPrefs.GetInt("HealthLevel");
        }
        else {
            PlayerPrefs.SetInt("HealthLevel", 1);
            healthLevel = 1;
        }

        if (PlayerPrefs.HasKey("AirCapacityLevel")) {
            airCapacityLevel = PlayerPrefs.GetInt("AirCapacityLevel");
        }
        else {
            PlayerPrefs.SetInt("AirCapacityLevel", 1);
            airCapacityLevel = 1;
        }

        if (PlayerPrefs.HasKey("WeaponDamageLevel")) {
            weaponDamageLevel = PlayerPrefs.GetInt("WeaponDamageLevel");
        }
        else {
            PlayerPrefs.SetInt("WeaponDamageLevel", 1);
            weaponDamageLevel = 1;
        }

        if (PlayerPrefs.HasKey("CashValue")) {
            cashAmount = Mathf.Round(PlayerPrefs.GetFloat("CashValue"));
        }
        else {
            PlayerPrefs.SetFloat("CashValue", 500.0f);
            cashAmount = 500.0f;
        }

        if (PlayerPrefs.HasKey("CoinRecord")) {
            coinsRecordText.text = "MOST COINS COLLECTED: " + PlayerPrefs.GetInt("CoinRecord");
        }
        else {
            coinsRecordText.text = "MOST COINS COLLECTED: 0";
            PlayerPrefs.SetInt("CoinRecord", 0);
        }

        if (PlayerPrefs.HasKey("DistanceTravelledRecord")) {
            distanceTravelledRecordText.text = "FURTHEST DISTANCE TRAVELLED: " + PlayerPrefs.GetFloat("DistanceTravelledRecord").ToString("F2") + "M";
        }
        else {
            distanceTravelledRecordText.text = "FURTHEST DISTANCE TRAVELLED: 0M";
            PlayerPrefs.SetFloat("DistanceTravelledRecord", 0.0f);
        }

        if (PlayerPrefs.HasKey("TotalScoreRecord")) {
            totalScoreRecordText.text = "HIGHEST SCORE: $" + PlayerPrefs.GetFloat("TotalScoreRecord").ToString("F2");
        }
        else {
            totalScoreRecordText.text = "HIGHEST SCORE: $0";
            PlayerPrefs.SetFloat("TotalScoreRecord", 0.0f);
        }

        healthUpgradeCost = 500.0f * Mathf.Pow(healthLevel, 2); //Calculate New Level Costs
        airCapacityUpgradeCost = 500.0f * Mathf.Pow(airCapacityLevel, 2);
        weaponDamageUpgradeCost = 500.0f * Mathf.Pow(weaponDamageLevel, 2);

        cashAmountText.text = "CASH: $ " + cashAmount.ToString();

        healthFunctions(); // Update Text Values
        airCapacityFunctions();
        weaponDamageFunctions();

        if (PlayerPrefs.GetInt("isSoundEnabled") == 1) { // Sound Is Enabled
            backgroundSound.Play();
        }
    }

    public void upgradeHealth() { // Upgrade Health Level
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1) { // Sound Is Enabled
            levelUpSound.Stop();
            levelUpSound.Play();
        }
        healthLevel++; //Increment Health
        cashAmount -= healthUpgradeCost; // Subtract Cost
        healthUpgradeCost = 500.0f * Mathf.Pow(healthLevel, 2); // New Cost

        PlayerPrefs.SetInt("HealthLevel", healthLevel); // Update PrayPrefs
        PlayerPrefs.SetFloat("CashValue", cashAmount);

        cashAmountText.text = "CASH: $ " + cashAmount.ToString();
        healthFunctions(); // Call Other Upgrades To Make Sure New Cost Has Not Affected Them
        airCapacityFunctions();
        weaponDamageFunctions();
    }

    public void upgradeAirBubbleCapacity() {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1) { // Sound Is Enabled
            levelUpSound.Stop();
            levelUpSound.Play();
        }
        airCapacityLevel++;
        cashAmount -= airCapacityUpgradeCost;
        airCapacityUpgradeCost = 500.0f * Mathf.Pow(airCapacityLevel, 2);

        PlayerPrefs.SetInt("AirCapacityLevel", airCapacityLevel);
        PlayerPrefs.SetFloat("CashValue", cashAmount);

        cashAmountText.text = "CASH: $ " + cashAmount.ToString();
        healthFunctions();
        airCapacityFunctions();
        weaponDamageFunctions();
    }

    public void upgradeWeaponry() {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1) { // Sound Is Enabled
            levelUpSound.Stop();
            levelUpSound.Play();
        }
        weaponDamageLevel++;
        cashAmount -= weaponDamageUpgradeCost;
        weaponDamageUpgradeCost = 500.0f * Mathf.Pow(weaponDamageLevel, 2);

        PlayerPrefs.SetInt("WeaponDamageLevel", weaponDamageLevel);
        PlayerPrefs.SetFloat("CashValue", cashAmount);

        cashAmountText.text = "CASH: $ " + cashAmount.ToString();
        healthFunctions();
        airCapacityFunctions();
        weaponDamageFunctions();
    }

    /*
    public void RESET() {
        PlayerPrefs.SetInt("HealthLevel", 1);
        PlayerPrefs.SetInt("AirCapacityLevel", 1);
        PlayerPrefs.SetInt("WeaponDamageLevel", 1);
        PlayerPrefs.SetFloat("CashValue", 500.0f);
    }
     * */

    private void healthFunctions() {
        healthLevelText.text = "HEALTH: " + healthLevel.ToString();

        if (cashAmount < healthUpgradeCost) { // If Current Cash Amount Is Less Than Cost
            healthUpgradeBtn.interactable = false; // Disable Button
            healthUpgradeBtn.GetComponent<Image>().sprite = lockedSprite; // Show Locked Icon
            healthCostText.text = "-$" + healthUpgradeCost.ToString(); // Negative Cost
            healthCostText.color = redText;
        }
        else {
            healthUpgradeBtn.interactable = true; // ENable Button
            healthUpgradeBtn.GetComponent<Image>().sprite = plusSprite;  // Purchase Icon
            healthCostText.text = "$" + healthUpgradeCost.ToString();
            healthCostText.color = greenText;
        }
    }

    private void airCapacityFunctions() {
        airCapacityLevelText.text = "AIR CAPACITY: " + airCapacityLevel.ToString();
        
        if (cashAmount < airCapacityUpgradeCost) {
            airCapacityUpgradeBtn.interactable = false;
            airCapacityUpgradeBtn.GetComponent<Image>().sprite = lockedSprite;
            airCapacityCostText.text = "-$" + airCapacityUpgradeCost.ToString();
            airCapacityCostText.color = redText;
        }
        else {
            airCapacityUpgradeBtn.interactable = true;
            airCapacityUpgradeBtn.GetComponent<Image>().sprite = plusSprite;
            airCapacityCostText.text = "$" + airCapacityUpgradeCost.ToString();
            airCapacityCostText.color = greenText;
        }
    }

    private void weaponDamageFunctions() {
        weaponDamageLevelText.text = "WEAPON: " + weaponDamageLevel.ToString();
        
        if (cashAmount < weaponDamageUpgradeCost) {
            weaponDamageUpgradeBtn.interactable = false;
            weaponDamageUpgradeBtn.GetComponent<Image>().sprite = lockedSprite;
            weaponDamageCostText.text = "-$" + weaponDamageUpgradeCost.ToString();
            weaponDamageCostText.color = redText;
        }
        else {
            weaponDamageUpgradeBtn.interactable = true;
            weaponDamageUpgradeBtn.GetComponent<Image>().sprite = plusSprite;
            weaponDamageCostText.text = "$" + weaponDamageUpgradeCost.ToString();
            weaponDamageCostText.color = greenText;
        }
    }

    public void startGame() {
        SceneManager.LoadScene("MainLevel");
    }

    public void mainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
