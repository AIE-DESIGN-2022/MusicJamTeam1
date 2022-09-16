using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    PlayerController playerController;
    DootGun dootGun;
    Hud hud;
    public float baseFireRate;
    public float baseWalkingSpeed;
    public float baseRunningSpeed;
    public float baseJumpSpeed;
    public float increasedFireRate;
    public float increasedWalkingSpeed;
    public float increasedRunningSpeed;
    public float increasedJumpSpeed;
    public bool fireRateBoosted;
    public bool speedBoosted;
    public bool jumpBoosted;

    void Start()
    {
        dootGun = FindObjectOfType<DootGun>();
        playerController = FindObjectOfType<PlayerController>();
        hud = FindObjectOfType<Hud>();
        //dootGun.rateOfFire = baseFireRate;
        playerController.walkingSpeed = baseWalkingSpeed;
        playerController.runningSpeed = baseRunningSpeed;
    }

    void Update()
    {

    }
    public IEnumerator RateOfFire(float duration, GameObject pickUp)
    {
        fireRateBoosted = true;
        hud.CheckBuffs();
        dootGun.rateOfFire = increasedFireRate;
        yield return new WaitForSeconds(duration);
        dootGun.rateOfFire = baseFireRate;
        fireRateBoosted = false;
        hud.CheckBuffs();
        Destroy(pickUp);
    }
    public IEnumerator Speed(float duration, GameObject pickUp)
    {
        speedBoosted = true;
        hud.CheckBuffs();
        playerController.runningSpeed = increasedRunningSpeed;
        playerController.walkingSpeed = increasedWalkingSpeed;
        yield return new WaitForSeconds(duration);
        playerController.runningSpeed = baseRunningSpeed;
        playerController.walkingSpeed = baseWalkingSpeed;
        speedBoosted = false;
        hud.CheckBuffs();
        Destroy(pickUp);
    }
    public IEnumerator SuperJump(float duration, GameObject pickUp)
    {
        jumpBoosted = true;
        hud.CheckBuffs();
        playerController.jumpSpeed = increasedJumpSpeed;
        yield return new WaitForSeconds(duration);
        playerController.jumpSpeed = baseJumpSpeed;
        jumpBoosted = false;
        hud.CheckBuffs();
        Destroy(pickUp);
    }
    public void Heal(float healthAmount, GameObject pickUp)
    {
        playerController.currentHealth += healthAmount;
        if (playerController.currentHealth > playerController.maxHealth)
        {
            playerController.currentHealth = playerController.maxHealth;
        }
        Destroy(pickUp);
    }
    public void Grenade(GameObject pickUp)
    {
        playerController.grenadeCount += 1;
        Destroy(pickUp);
    }
}
