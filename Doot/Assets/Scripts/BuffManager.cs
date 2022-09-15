using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    PlayerController playerController;
    DootGun dootGun;
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
        dootGun.rateOfFire = baseFireRate;
        playerController.walkingSpeed = baseWalkingSpeed;
        playerController.runningSpeed = baseRunningSpeed;
    }

    void Update()
    {

    }
    public IEnumerator RateOfFire(float duration, GameObject pickUp)
    {
        fireRateBoosted = true;
        dootGun.rateOfFire = increasedFireRate;
        yield return new WaitForSeconds(duration);
        dootGun.rateOfFire = baseFireRate;
        fireRateBoosted = false;
        Destroy(pickUp);
    }
    public IEnumerator Speed(float duration, GameObject pickUp)
    {
        speedBoosted = true;
        playerController.runningSpeed = increasedRunningSpeed;
        playerController.walkingSpeed = increasedWalkingSpeed;
        yield return new WaitForSeconds(duration);
        playerController.runningSpeed = baseRunningSpeed;
        playerController.walkingSpeed = baseWalkingSpeed;
        speedBoosted = false;
        Destroy(pickUp);
    }
    public IEnumerator SuperJump(float duration, GameObject pickUp)
    {
        jumpBoosted = true;
        playerController.jumpSpeed = increasedJumpSpeed;
        yield return new WaitForSeconds(duration);
        playerController.jumpSpeed = baseJumpSpeed;
        jumpBoosted = false;
        Destroy(pickUp);
    }
    public void Heal(GameObject pickUp)
    {
        playerController.currentHealth += 25;
        if (playerController.currentHealth > playerController.maxHealth)
        {
            playerController.currentHealth = playerController.maxHealth;
        }
        Destroy(pickUp);
    }
}
