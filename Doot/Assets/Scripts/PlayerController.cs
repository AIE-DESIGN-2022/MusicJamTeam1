using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Alive { alive, dead }

public class PlayerController : MonoBehaviour
{
    #region moveVariable
    public float walkingSpeed = 7.5f;
    public float runningSpeed = 11.5f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;
    Camera playerCamera;
    public float lookSpeed = 2.0f;
    public float lookXLimit = 45.0f;
    CharacterController characterController;
    Vector3 moveDirection = Vector3.zero;
    float rotationX = 0;
    [HideInInspector]
    public bool canMove = true;
    #endregion

    #region Health
    public float maxHealth, currentHealth;
    public float maxShield, currentShield;
    public float shieldRechargeTime, shieldRechargeRate;
    public GameObject respawnButton;
    public int laps;
    bool takingDamage = false;
    #endregion

    #region Grenades
    public int grenadeCount;
    public GameObject grenade;
    public Transform grenadeSpawn;
    bool grenadeInCooldown = false;
    public float grenadeCooldownTime;
    #endregion

    #region Melee
    public GameObject melee;
    Rigidbody meleeRigidbody;
    Animator animator;
    #endregion

    public Alive alive;

    void Start()
    {
        currentHealth = maxHealth;
        currentShield = maxShield;
        characterController = GetComponent<CharacterController>();
        playerCamera = Camera.main;
        meleeRigidbody = melee.GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void Awake()
    {
        CursorLock();
    }

    void Update()
    {
        if(alive == Alive.alive)
        {
            PlayerMovement();

            if(currentShield != maxShield && !takingDamage)
            {
                ShieldRecharge();
            }
        }
        else
        {
            respawnButton.SetActive(true);
            CursorUnlock();
        }

        if(Input.GetMouseButtonDown(1) && alive != Alive.dead)
        {
            Melee();
        }
            
        if(Input.GetKeyDown(KeyCode.Q) && grenadeCount >= 1)
        {
            Grenade();
        }
    }

    void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void PlayerMovement()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Vertical") : 0;
        float curSpeedY = canMove ? (isRunning ? runningSpeed : walkingSpeed) * Input.GetAxis("Horizontal") : 0;
        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);

        if (Input.GetKeyDown(KeyCode.Space) && canMove && characterController.isGrounded)
            moveDirection.y = jumpSpeed;
        else
            moveDirection.y = movementDirectionY;

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
            moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
        }
    }

    public void TakeDamage(float damage)
    {
        takingDamage = true;

        if(currentShield > 0)
        {
            currentShield -= damage;
        }
        else
        {
            currentHealth -= damage;
        }

        StartCoroutine(ShieldRechargeTimer(shieldRechargeTime));

        Health();
    }

    IEnumerator ShieldRechargeTimer(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        takingDamage = false;
    }

    void ShieldRecharge()
    {
        currentShield += shieldRechargeRate * Time.deltaTime;
    }
    void Health()
    {
        if (currentHealth <= 0)
        {
            alive = Alive.dead;
            Debug.Log("died");
        }
        else
            alive = Alive.alive;
    }

    public void Respawn()   //Called by button
    {
        currentHealth = maxHealth;
        Health();
        respawnButton.SetActive(false);
        CursorLock();
    }

    void Melee()
    {
        melee.SetActive(true);
        animator.SetBool("MeleeAttack", true);
        StartCoroutine(MeleeDrop());
    }
    IEnumerator MeleeDrop()
    {
        yield return new WaitForSeconds(0.5f);
        
        animator.SetBool("MeleeAttack", false);
    }

    void Grenade()
    {
        if (!grenadeInCooldown)
        {
            grenadeInCooldown = true;
            grenadeCount--;
            GameObject g = Instantiate(grenade, grenadeSpawn.position, transform.rotation);
            g.transform.forward = transform.forward;
            StartCoroutine(GrenadeCooldown(grenadeCooldownTime));
        }
    }

    IEnumerator GrenadeCooldown (float seconds)
    {
        yield return new WaitForSeconds(seconds);
        grenadeInCooldown = false;
    }
}