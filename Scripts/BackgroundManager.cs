using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour {

    public float moveSpeed;
    private Vector2 backgroundPos;

	void Update () {
        backgroundPos = new Vector2(Time.time * moveSpeed, 0); // Move Background Position
        GetComponent<Renderer>().material.mainTextureOffset = backgroundPos; // Set Texture Offset To New Position
	}
}
