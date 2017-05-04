using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {
    public AudioSource backgroundSound;
    void Start() {
        if (PlayerPrefs.GetInt("isSoundEnabled") == 1) { // Sound Is Enabled
            backgroundSound.Play();
        }
    }
}
