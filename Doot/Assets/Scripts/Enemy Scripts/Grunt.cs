using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Enemy
{
    public GameObject bullet;
    public Transform bulletSpawn;
    [SerializeField] Animator animator;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        animator = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();

        if(agent.velocity.magnitude > 0)
            animator.SetFloat("Walk", 1);
    }

    protected override void FrenzyUpdate()
    {
        animator.SetBool("Rage", true);
        base.FrenzyUpdate();
    }

    protected override void TriggerDead()
    {
        animator.SetBool("Die", true);
        base.TriggerDead();
    }

    public override void Attack()
    {
        base.Attack();
        GameObject projectile = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        Projectile b = projectile.GetComponent<Projectile>();
        b.maxDamage = maxDamage;
    }
}
