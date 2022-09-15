using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Text healthText;
    public PlayerController playerController;
    public BuffManager buffManager;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        buffManager = FindObjectOfType<BuffManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "" + playerController.currentHealth * 2;
    }
}
