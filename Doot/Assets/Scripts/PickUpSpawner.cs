using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    public PlayerController controller;
    public int lastLap;
    public float rando;
    private bool collidedThisTurn;
    public GameObject grenadePickUp;
    public GameObject jumpPickUp;
    public GameObject speedPickUp;
    private GameObject objToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        controller = FindObjectOfType<PlayerController>();
        collidedThisTurn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.laps > lastLap && collidedThisTurn == false)
        {
            Spawn();
        }
    }
    void Spawn()
    {
        collidedThisTurn = true;

        rando = Random.Range(0, 3);
        if (rando == 0)
        {
            objToSpawn = grenadePickUp;
        }        
        if (rando == 1)
        {
            objToSpawn = speedPickUp;
        }        
        if (rando == 2)
        {
            objToSpawn = jumpPickUp;
        }
        Instantiate(objToSpawn, this.transform.position, Quaternion.identity);
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collidedThisTurn = false;
            lastLap = controller.laps;
        }
    }
}
