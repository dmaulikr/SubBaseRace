using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour {
    public Vector3 bulletOffset;
    public GameObject bulletPrefab;

    public AudioSource gunShot;
    public Button fireButton;
    public Button boostButton;

    public Text currentAmmoCountText;
    public int currentAmmoCountInt;

    public float fireDelay = 0.25f;
    public float boostDuration = 2.0f;
    private int weaponDamageLevel;
    private float cooldownTimer = 0;
    private float cooldownTimerBoost = 3.0f;
    public bool isBoosting;

    private PlayerManager pm;

    void Start() {
        bulletOffset = new Vector3(3.0f, -0.5f, 0);
        if (PlayerPrefs.HasKey("WeaponDamageLevel")) { // Get Weapon Level - Set To Ammo Count
            weaponDamageLevel = PlayerPrefs.GetInt("WeaponDamageLevel");
        }
        else {
            weaponDamageLevel = 1;
        }
        currentAmmoCountInt = weaponDamageLevel * 1;
        if(currentAmmoCountInt < 10){ // Format Ammo Count
            currentAmmoCountText.text = "00" + currentAmmoCountInt.ToString();
        }
        else if (currentAmmoCountInt < 100) {
            currentAmmoCountText.text = "0" + currentAmmoCountInt.ToString();
        }
        else {
            currentAmmoCountText.text = currentAmmoCountInt.ToString();
        }
        pm = FindObjectOfType<PlayerManager>();
        isBoosting = false;
    }

    void Update(){
        cooldownTimer -= Time.deltaTime; // Decrease Timers For Fire & Boost
        cooldownTimerBoost -= Time.deltaTime;

        if (isBoosting) { // If Boosting - Decrease Time Left
            boostDuration -= Time.deltaTime;
            if (boostDuration <= 0.0f) { // Boost Depleted - Reset Speed 
                pm.setSpeedControlX(1.0f);
                boostDuration = 2.0f;
                cooldownTimerBoost = 3.0f; // Set Cooldown Timer
                isBoosting = false;
            }
        }
        else if (cooldownTimerBoost > 0.0f) { // Timer In Effect So Disable Button
            boostButton.interactable = false;
        }
        else { // Timer Clear - Enable Button
            boostButton.interactable = true;
        }

        if (Input.GetKeyDown("space"))
            fire();

    }

    public void fire() { // Fire A Bullet
        if (cooldownTimer <= 0 && currentAmmoCountInt > 0) { // Make Sure Fire Countdown Less Than 0 And Ammo Still Exists
            cooldownTimer = fireDelay; // Reset Delay
            Vector3 offset = transform.rotation * bulletOffset;

            GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, transform.position + offset, bulletPrefab.transform.rotation); // Create GO
            if (PlayerPrefs.GetInt("isSoundEnabled") == 1) {
                gunShot.Play();
            }
            currentAmmoCountInt--; // Decrease Ammo

            if (currentAmmoCountInt == 0) { // Disable Button If Ammo Count Is 0
                fireButton.interactable = false;
            }

            if (currentAmmoCountInt < 10) { // Format Ammo Count
                currentAmmoCountText.text = "00" + currentAmmoCountInt.ToString();
            }
            else if (currentAmmoCountInt < 100) {
                currentAmmoCountText.text = "0" + currentAmmoCountInt.ToString();
            }
            else {
                currentAmmoCountText.text = currentAmmoCountInt.ToString();
            }
        }
    }

    // Player Clicked Boost
    public void boost() {
        if (cooldownTimerBoost <= 0 && !isBoosting){ // If Countdown Timer Is Less Than 0 and Not Currently Boosting
            pm.setSpeedControlX(2.0f); // Double Speed & Disable Button
            isBoosting = true;
            boostButton.interactable = false;
        }
    }

    public bool getIsBoosting() {
        return isBoosting;
    }
}
