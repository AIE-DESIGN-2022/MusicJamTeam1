using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool rateOfFireBuff;
    public bool speedBuff;
    public bool superJump;
    public bool healthPack;
    public bool grenadePickup;
    public float duration;
    public float healthPackAmount;
    BuffManager buffManager;
    private bool check;
    private GameObject child;
    // Start is called before the first frame update
    void Start()
    {
        buffManager = FindObjectOfType<BuffManager>();
        Debug.Log(buffManager.name);
        child = gameObject.transform.GetChild(0).gameObject;
        check = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && check == true)
        {
            check = false;
            child.SetActive(false);
            if (rateOfFireBuff)
            {
                StartCoroutine(buffManager.RateOfFire(duration, this.gameObject));
            }
            if (speedBuff)
            {
                StartCoroutine(buffManager.Speed(duration, this.gameObject));
            }
            if (superJump)
            {
                StartCoroutine(buffManager.SuperJump(duration, this.gameObject));
            }
            if (healthPack)
            {
                buffManager.Heal(healthPackAmount, this.gameObject);
            }
            if (grenadePickup)
            {
                buffManager.Grenade(this.gameObject);
            }
        }
    }
}
