using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {
    private float t;
    private float threshold;
    public float distanceThreshold;
    private float pickupCounter;
    private float pickupThreshold;

    // All Prefabs For Objects
    public GameObject rockPrefab;
    public GameObject seaweedPrefab;
    public GameObject minePrefab;
    public GameObject barrelPrefab;
    public GameObject lRockUpPrefab;
    public GameObject lRockDownPrefab;
    public GameObject lRockUp2Prefab;
    public GameObject lRockDown2Prefab; 
    public GameObject airBubblePrefab;
    public GameObject coinPrefab;

    private Vector3 lastPosition;
    private float goMoveAmount;

    void Start() {
        t = 0.0f;
        pickupCounter = 0.0f;
        threshold = 0.5f; // Threshold For Spawing Obstacles - Not Used
        distanceThreshold = 25.0f; // Spawn Obstacles Every 25 Units
        pickupThreshold = 1.5f; // Threshold For Spawing Pickups
        lastPosition = transform.position;
        goMoveAmount = 0.0f;
    }

    void Update () {

        goMoveAmount += transform.position.x - lastPosition.x; // How Much Has The Camera Moved
        if (goMoveAmount >= distanceThreshold) { // Time To Spawn An Obstacle
            goMoveAmount = 0.0f;
            lastPosition = transform.position;

            int randomIndex = randomNum(0, 6); // Generate Random Number - Depending On Number, Generate Obstacle
            if (randomIndex == 0) { // Small Rock - Random Y Location
                GameObject rockGO = (GameObject)Instantiate(rockPrefab, new Vector3(transform.position.x, randomNum(-Mathf.RoundToInt(Camera.main.orthographicSize), Mathf.RoundToInt(Camera.main.orthographicSize)), 0), Quaternion.Euler(0, 0, 0));
            }
            else if (randomIndex == 1) { // Barrel - Random Y Location
                GameObject barrelGO = (GameObject)Instantiate(barrelPrefab, new Vector3(transform.position.x, randomNum(-Mathf.RoundToInt(Camera.main.orthographicSize), Mathf.RoundToInt(Camera.main.orthographicSize)), 0), Quaternion.Euler(0, 0, 0));
            }
            else if (randomIndex == 2) { // Seaweed - Spawns At Bottom Of Screen
                GameObject seaweedGO = (GameObject)Instantiate(seaweedPrefab, new Vector3(transform.position.x, -Camera.main.orthographicSize, 0), Quaternion.Euler(0, 0, 0));
            }
            else if (randomIndex == 3) { // Mine - Spawns At Bottom Of Screen
                GameObject mineGO = (GameObject)Instantiate(minePrefab, new Vector3(transform.position.x, -Camera.main.orthographicSize, 0), Quaternion.Euler(0, 0, 0));
            }
            else if (randomIndex == 4) { // Single Large Rock Top Of Screen
                //int randomIndexRock = randomNum(0, 2);
                //if (randomIndexRock == 0) { // Spawns At Top Of Screen
                    GameObject lRockUpPrefabGO = (GameObject)Instantiate(lRockUp2Prefab, new Vector3(transform.position.x, Camera.main.orthographicSize, 0), Quaternion.Euler(0, 0, 0));
                //}
                //else if (randomIndexRock == 1) { // Spawns At Bottom Of Screen
                    //GameObject lRockDownPrefabGO = (GameObject)Instantiate(lRockDown2Prefab, new Vector3(transform.position.x, -Camera.main.orthographicSize, 0), Quaternion.Euler(0, 0, 0));
                //}
            }
            else if (randomIndex == 5) { // Duel Rocks
                GameObject lRockUpPrefabGO = (GameObject)Instantiate(lRockUpPrefab, new Vector3(transform.position.x, Camera.main.orthographicSize, 0), Quaternion.Euler(0, 0, 0));
                GameObject lRockDownPrefabGO = (GameObject)Instantiate(lRockDownPrefab, new Vector3(transform.position.x, -Camera.main.orthographicSize, 0), Quaternion.Euler(0, 0, 0));
            }
        }

        
        //t += Time.deltaTime;
        pickupCounter += Time.deltaTime;
        /*
        if (t >= threshold) { // Time To Spawn An Obstacle
            t = 0.0f;
        }
        */
        if (pickupCounter >= pickupThreshold) { // Time To Spawn A Pickup
            pickupCounter = 0.0f;

            int randomIndex = randomNum(0, 2);
            if (randomIndex == 0) { // Air Bubble - Random Y
                GameObject airBubbleGO = (GameObject)Instantiate(airBubblePrefab, new Vector3(transform.position.x, randomNum(-Mathf.RoundToInt(Camera.main.orthographicSize), Mathf.RoundToInt(Camera.main.orthographicSize)), 0), Quaternion.Euler(0, 0, 0));
            }
            else if (randomIndex == 1) { // Coin - Random Y
                GameObject coinGO = (GameObject)Instantiate(coinPrefab, new Vector3(transform.position.x, randomNum(-Mathf.RoundToInt(Camera.main.orthographicSize), Mathf.RoundToInt(Camera.main.orthographicSize)), 0), Quaternion.Euler(0, 0, 0));
            }
        }
	}

    // Random Number Generator
    int randomNum(int first, int second) {
        int randomIndexReturn = 0;
        randomIndexReturn = UnityEngine.Random.Range(first, second);
        return randomIndexReturn;
    }
}
