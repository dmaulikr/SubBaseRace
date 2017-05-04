using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupManager : MonoBehaviour {
    private PlayerManager pm;
 
    void Start() {
        pm = FindObjectOfType<PlayerManager>();
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (gameObject.CompareTag("AirBubble")) { // Collied With Air Bubble, Have PM Update Air Amount
            pm.incrementBubbleTankCapacity(0.25f);
            Destroy(gameObject);
        }
        else if (gameObject.CompareTag("Coin")) { // Collied With Coin, Have PM Update Money Amount
            pm.incrementCashAmount(200.0f);
            Destroy(gameObject);
        }
    }
}
