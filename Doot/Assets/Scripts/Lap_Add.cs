using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lap_Add : MonoBehaviour
{
    PlayerController controller;
    private void OnTriggerEnter(Collider other)
    {
        controller = other.GetComponent<PlayerController>();
        if (controller != null)
        {
            controller.laps++;            
        }
    }
}
