using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grunt : Enemy
{
    public GameObject bullet;
    public Transform bulletSpawn;

    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
    }

    public override void Attack()
    {
        base.Attack();
        GameObject projectile = Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        Projectile b = projectile.GetComponent<Projectile>();
        b.maxDamage = maxDamage;
    }
}
