using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private PlayerManager pm;
    private Vector3 pmLastPosition;
    private float cameraMoveAmount;

	void Start () {
        pm = FindObjectOfType<PlayerManager>();
        pmLastPosition = pm.transform.position;
	}

    // Perform In Late Update So Player's Update Has Finished
    void LateUpdate() {
        cameraMoveAmount = pm.transform.position.x - pmLastPosition.x; // Translation Amount Between Player & Camera
        Vector3 newPosition = new Vector3(transform.position.x + cameraMoveAmount, transform.position.y, transform.position.z); // Set New Position
        transform.position = newPosition;
        pmLastPosition = pm.transform.position; // Save Position
	}
}
