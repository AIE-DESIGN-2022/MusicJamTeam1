using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed;
    Rigidbody rb;
    public float maxDamage;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {
        Force();
    }

    void Force()
    {
        rb.AddForce(transform.forward * projectileSpeed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            playerController = collision.collider.GetComponent<PlayerController>();
            if(playerController == null)
                playerController = collision.collider.GetComponentInParent<PlayerController>();

            DealDamage();
        }
            
        StartCoroutine(Despawn());
    }

    public virtual void DealDamage()
    {
        float damage = rb.velocity.magnitude;
        if(rb.velocity.magnitude > maxDamage)
            damage = maxDamage;

        playerController.TakeDamage(damage);
        Debug.Log("Hit Player for " + (damage));
    }

    IEnumerator Despawn()
    {
        yield return new WaitForSeconds(2);
        Destroy(this.gameObject);
    }
}
