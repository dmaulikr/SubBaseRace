using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour {

    public float moveSpeed = 60f;

    void Update() {
        Vector3 myPosition = transform.position;
        Vector3 myVelocity = new Vector3(0, moveSpeed * Time.deltaTime, 0); // Move The Bullet In Y Direction With Player Rotation
        myPosition += transform.rotation * myVelocity;
        transform.position = myPosition;

        // Add Destroy Bullet
        // Oops Forgot :/
    }
}
