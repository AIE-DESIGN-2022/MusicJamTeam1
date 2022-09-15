using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Text healthText;
    public Text buffs;
    private string speed;
    private string jump;
    private string rate;
    public PlayerController playerController;
    public BuffManager buffManager;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        buffManager = FindObjectOfType<BuffManager>();
        speed = "Speed Boosted";
        jump = "Jump Boosted";
        rate = "Rate of Fire";
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "" + playerController.currentHealth * 2;
        CheckBuffs();
    }
    public void CheckBuffs()
    {
        if (buffManager.speedBoosted == true)
        {
            buffs.text = string.Concat(speed);
        }
        if (buffManager.speedBoosted == false)
        {
            buffs.text = speed.Replace(speed, "");
        }
    }
}
