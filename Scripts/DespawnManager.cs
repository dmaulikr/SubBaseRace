using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnManager : MonoBehaviour {

    private  GameObject despawnPoint;

	void Start () {
        despawnPoint = GameObject.Find("DespawnManager"); // Child Of Camera (Located Behind Camera FOV)
	}
	
	void Update () {
        if (transform.position.x < despawnPoint.transform.position.x) { // Check Is Position Is Less Than Desspawn Location - If So, Destroy
            Destroy(gameObject);    
        }
        
    }
}
