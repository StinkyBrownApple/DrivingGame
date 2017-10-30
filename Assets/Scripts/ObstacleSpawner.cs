using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {

    [SerializeField]
    GameObject car;

    [SerializeField]
    Transform[] spawnPoints;

    
    float spawnTime = 0.5f;
    int obstacleSpeed = -50;
    float currentSpawnTime;
    float twoSpawnChance = 0f;
    int twoSpawnCooldown = 3;
    int twoSpawnCurrentCooldown;

    [SerializeField]
    int speedIncrease = 1;
    [SerializeField]
    float twoSpawnIncrease = 0.5f;

	// Use this for initialization
	void Start () {

        currentSpawnTime = spawnTime;
	}
	
	// Update is called once per frame
	void Update () {
        currentSpawnTime -= Time.deltaTime;


        if(currentSpawnTime <= 0)
        {

            if(Random.Range(0, 100) < twoSpawnChance && twoSpawnCurrentCooldown == 0)
            {
                SpawnTwoObstacles();
                twoSpawnCurrentCooldown = twoSpawnCooldown;
            }
            else
                SpawnObstacle();

            if (twoSpawnCurrentCooldown > 0)
                twoSpawnCurrentCooldown--;
            obstacleSpeed -= speedIncrease;
            twoSpawnChance += twoSpawnIncrease;

            currentSpawnTime = spawnTime;
        }
	}

    private void SpawnObstacle()
    {
        GameObject spawnedObstacle = Instantiate(car, spawnPoints[Random.Range(0, spawnPoints.Length)].position, Quaternion.Euler(0, 180, 0));
        spawnedObstacle.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, obstacleSpeed);
    }

    private void SpawnTwoObstacles()
    {
        int randNo1;
        int randNo2;
        do
        {
            randNo1 = Random.Range(0, spawnPoints.Length);
            randNo2 = Random.Range(0, spawnPoints.Length);
        }
        while (randNo1 == randNo2);
        GameObject spawnedObstacle1 = Instantiate(car, spawnPoints[randNo1].position, Quaternion.Euler(0, 180, 0));
        GameObject spawnedObstacle2 = Instantiate(car, spawnPoints[randNo2].position, Quaternion.Euler(0, 180, 0));
        spawnedObstacle1.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, obstacleSpeed);
        spawnedObstacle2.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, obstacleSpeed);
    }

    private void OnGUI()
    {
        GUI.Label(new Rect(0, 0, 500, 500), (-obstacleSpeed).ToString());
    }
}
