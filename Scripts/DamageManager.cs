using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageManager : MonoBehaviour {

    int health = 1;
    private PlayerManager pm;
    private WeaponManager wm;

    void Start() {
        pm = FindObjectOfType<PlayerManager>();
        wm = FindObjectOfType<WeaponManager>();
    }

    // Called When Player Or Bullet Collides With Object
    void OnTriggerEnter2D(Collider2D other) {
        if ((gameObject.CompareTag("Mine") && other.CompareTag("Player")) || (gameObject.CompareTag("LRock") && other.CompareTag("Player"))) { // Mine Or Rock With Player End Game
            pm.runGameOver();
        }
        else if ( !(gameObject.CompareTag("Seaweed") && other.CompareTag("Player")) ) { // Not Seaweed & Player
            if (gameObject.CompareTag("Seaweed") && other.gameObject.CompareTag("Bullet")) { // Seaweed & Bullet
                if (!wm.getIsBoosting()) { // If Not Boosting, Set Player Speed To Normal - They Were In The Seaweed & Destoryed It
                    pm.setSpeedControlX(1.0f); 
                }
            }
            if (!(gameObject.CompareTag("LRock") && other.CompareTag("Bullet"))) { // Bullet Hit - Decrement Health
                health--;  
            }
        }
    }

    void Update() {
        if (health <= 0) { // Object Is Dead - Destroy
            Die();
        }
    }

    void Die() {
        Destroy(gameObject);
    }
}
