using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelMovement : MonoBehaviour {

    public float rotateSpeed;
    public float fallSpeed;

	void Update () {
        // Roate The Barrel Around Move It Downward
        transform.Rotate(new Vector3(0, 0, rotateSpeed) * Time.deltaTime);
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y + (-fallSpeed) * Time.deltaTime, transform.position.z);
        transform.position = newPosition;
	}
}
