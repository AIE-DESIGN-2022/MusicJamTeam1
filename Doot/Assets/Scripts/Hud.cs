using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Text healthText;
    public Text buffs;
    public Text wave;
    public Text score;
    public Text grenades;
    public Text shieldText;
    public Text one;
    public Text two;
    public Text three;
    public Text four;
    public Text five;
    public Text six;
    public GameObject dooted;
    public GameObject notDooted;
    private string buff;
    private Color black;
    private Color yellow;
    public PlayerController playerController;
    public BuffManager buffManager;
    // Start is called before the first frame update
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        buffManager = FindObjectOfType<BuffManager>();
        buff = ("Jump Boosted \nSpeed Boosted \nRate of Fire");
        black = Color.black;
        yellow = Color.yellow;
        one.color = black;
        two.color = black;
/*        three.color = black;
        four.color = black;
        five.color = black;
        six.color = black;*/
        CheckBuffs();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.currentHealth > 0)
        {
            healthText.text = "" + Mathf.Round(playerController.currentHealth * 2);
        }
        else
        {
            healthText.text = "0";
        }
        if (playerController.currentShield > 0)
        {
            shieldText.text = "" + Mathf.Round(playerController.currentShield);
        }
        else
        {
            shieldText.text = "0";
        }
        grenades.text = "Grenades: " + playerController.grenadeCount;
        wave.text = "Wave: " + playerController.laps;
        score.text = "Score: " + Mathf.Round(playerController.score);
        buffs.text = buff;
        if (Input.GetButtonDown("Fire1"))
        {
            dooted.SetActive(true);
            notDooted.SetActive(false);
        }
        if (Input.GetButtonUp("Fire1"))
        {
            dooted.SetActive(false);
            notDooted.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            one.color = yellow;
            two.color = black;
/*            three.color = black;
            four.color = black;
            five.color = black;
            six.color = black;*/
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            one.color = black;
            two.color = yellow;
/*            three.color = black;
            four.color = black;
            five.color = black;
            six.color = black;*/
        }
/*        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            one.color = black;
            two.color = black;
            three.color = yellow;
            four.color = black;
            five.color = black;
            six.color = black;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            one.color = black;
            two.color = black;
            three.color = black;
            four.color = yellow;
            five.color = black;
            six.color = black;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            one.color = black;
            two.color = black;
            three.color = black;
            four.color = black;
            five.color = yellow;
            six.color = black;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            one.color = black;
            two.color = black;
            three.color = black;
            four.color = black;
            five.color = black;
            six.color = yellow;
        }*/

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
