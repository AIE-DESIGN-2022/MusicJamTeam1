using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Text healthText;
    public Text buffs;
    private string buff;
    public PlayerController playerController;
    public BuffManager buffManager;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        buffManager = FindObjectOfType<BuffManager>();
        buff = ("Jump Boosted \nSpeed Boosted \nRate of Fire");
        CheckBuffs();
    }

    // Update is called once per frame
    void Update()
    {
        healthText.text = "" + playerController.currentHealth * 2;
        buffs.text = buff;
    }
    public void CheckBuffs()
    {
        buff = ("Jump Boosted \nSpeed Boosted \nRate of Fire");
        if (buffManager.jumpBoosted == false)
        {
            buff = buff.Replace("Jump Boosted \n", "");
        }
        if (buffManager.speedBoosted == false)
        {
            buff = buff.Replace("Speed Boosted \n", "");
        }
        if (buffManager.fireRateBoosted == false)
        {
            buff = buff.Replace("Rate of Fire", "");
        }
    }
}
