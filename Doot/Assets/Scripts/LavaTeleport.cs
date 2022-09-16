using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaTeleport : MonoBehaviour
{
    Vector3 startPos;
    [SerializeField] Transform player;
    CharacterController characterController;


    // Start is called before the first frame update
    void Start()
    {
        startPos = player.position;
        characterController = player.GetComponent<CharacterController>();
        if (characterController != null)
            Debug.Log("Lava Teleport found controller");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Transform>() == player)
        {
            Debug.Log("Player fell in lava");
            characterController.enabled = false;
            player.position = startPos;
            StartCoroutine(TurnOnController(0.1f));
        }
    }

    IEnumerator TurnOnController(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        characterController.enabled = true;
    }
}
