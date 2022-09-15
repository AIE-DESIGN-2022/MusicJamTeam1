using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public int laps;
    public int spawncount;
    public GameObject enemy;
    public Transform player;
    public float playerThreshold;
    [HideInInspector]
    public int enemyspawned;
    public int maxEnemies;
    public int maxEnemiesX;

    // Start is called before the first frame update
    void Start()
    {
        maxEnemies = 4;
        maxEnemiesX = maxEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        spawncount = 2 * laps;
        maxEnemies = 4 * laps;
        if (enemyspawned <= spawncount && maxEnemiesX >= 1)
        {
            DistanceCheck();
        }           
    }

    void DistanceCheck()
    {
        float distance = Vector3.Distance(player.position, transform.position);
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
            maxEnemiesX = maxEnemies;
        }
    }
}
