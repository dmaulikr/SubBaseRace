using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {

    public AudioSource backgroundSound;
    public GameObject playBtn;
    public GameObject stopBtn;

    void Start() {
        // Get PlayerPrefs - Check For Sound Enabled - If Not Make A New One
        if (PlayerPrefs.HasKey("isSoundEnabled")) {
            if (PlayerPrefs.GetInt("isSoundEnabled") == 1) { // Sound Is Enabled
                backgroundSound.Play();
                toggleButtonOn();
            }
            else { // Sound Not Enabled
                toggleButtonOff();
            }
        }
        else {
            backgroundSound.Play();
            PlayerPrefs.SetInt("isSoundEnabled", 1);
            toggleButtonOn();
        }

        resetValue();
    }

    public void resetValue() {
        PlayerPrefs.SetInt("HealthLevel", 1);
        PlayerPrefs.SetInt("AirCapacityLevel", 1);
        PlayerPrefs.SetInt("WeaponDamageLevel", 1);
        PlayerPrefs.SetFloat("CashValue", 20000.0f);
        PlayerPrefs.SetInt("CoinRecord", 0);
        PlayerPrefs.SetFloat("DistanceTravelledRecord", 0.0f);
        PlayerPrefs.SetFloat("TotalScoreRecord", 0.0f);
    }

    public void playSound() { // Play Background Sound
        backgroundSound.Play();
        PlayerPrefs.SetInt("isSoundEnabled", 1);
        toggleButtonOn();
    }

    public void stopSound() { // Stop Background Sound
        backgroundSound.Stop();
        PlayerPrefs.SetInt("isSoundEnabled", 0);
        toggleButtonOff();
    }

    public void loadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

    public void quitGame() {
        Application.Quit();
    }

    private void toggleButtonOn() { // Show Sound On Button
        playBtn.SetActive(true);
        stopBtn.SetActive(false);
    }

    private void toggleButtonOff() { // Show Sound Off Button
        playBtn.SetActive(false);
        stopBtn.SetActive(true);
    }
}
