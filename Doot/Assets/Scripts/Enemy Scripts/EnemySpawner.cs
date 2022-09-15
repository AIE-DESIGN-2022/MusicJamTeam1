using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int spawncount;
    public GameObject enemy;
    public Transform player;
    public float playerThreshold;
    PlayerController playerController;
    [HideInInspector]
    public int enemyspawned;
    public int maxEnemies;
    public int maxEnemiesX;

    public float test;


    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        maxEnemies = 4;
        maxEnemiesX = maxEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        spawncount = 2 * playerController.laps;
        maxEnemies = 4 * playerController.laps;
        float x = DistanceCheck();
        if (enemyspawned <= spawncount)
        {
            SpawnEnemies(x);
        }           
    }

    float DistanceCheck()
    {
        float distance = Vector3.Distance(player.position, transform.position);
        test = distance;
        return distance;
    }

    void SpawnEnemies(float distance)
    {
        if (distance < playerThreshold)
        {
            for (int i = 0; i < spawncount; i++)
            {
                Instantiate(enemy, transform, false);
                enemyspawned++;
                maxEnemiesX--;
            }
        }
        else
        {
            if (maxEnemiesX <= 0)
            {
                maxEnemiesX = maxEnemies;
            }
            

        }
    }
}
