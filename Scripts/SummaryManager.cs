using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SummaryManager : MonoBehaviour {

    public Text distanceTravelledText;
    public Text moneyEarnedText;
    public Text totalMoneyEarnedText;
    public Text bonusText;
    public AudioSource backgroundSound;
    private int bonusAmount;

	void Start () {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        bonusAmount = 0;
        if (sceneName == "SummaryMenu") {
            // Get Scores For Player Prefs & Save To Text Fields
            distanceTravelledText.text = "Distance Travelled [m]         " + PlayerPrefs.GetFloat("LastScore").ToString("F2");
            moneyEarnedText.text = "Money Earned [$]                         " + Mathf.Round(PlayerPrefs.GetFloat("LastCash"));

            if(PlayerPrefs.GetInt("BrokeCR") == 1){ // Record Was Achieved Last Run
                bonusAmount += 500;
            }

            if (PlayerPrefs.GetInt("BrokeDTR") == 1) { // Record Was Achieved Last Run
                bonusAmount += 500;
            }

            if (PlayerPrefs.GetInt("BrokeTSR") == 1) { // Record Was Achieved Last Run
                bonusAmount += 500;
            }
            totalMoneyEarnedText.text = "Total                                                     $" + ((PlayerPrefs.GetFloat("LastScore") + bonusAmount + Mathf.Round(PlayerPrefs.GetFloat("LastCash")))).ToString("F2");
            bonusText.text = "Bonus Amount                                 " + bonusAmount;

        }
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1) { // Sound Is Enabled
            backgroundSound.Play();
        }
	}
    public void returnToBase() { // Load Upgrade Menu
        SceneManager.LoadScene("UpgradeMenu");
    }

    public void retryRun(){ // Load Main Level
        SceneManager.LoadScene("MainLevel");
    }
}
