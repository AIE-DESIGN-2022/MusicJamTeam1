using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTrigger : MonoBehaviour
{
    Grenade grenade;
    public Vector3 maxSize;

    private void Start()
    {
        grenade = GetComponentInParent<Grenade>();
    }

    private void Update()
    {
        Explode();
    }
    private void OnTriggerEnter(Collider other)
    {
        grenade.enemyList.Add(other.GetComponent<Enemy>());
    }

    void Explode()
    {
        if (transform.localScale != maxSize)
        {
            float scale = 200f;
            Vector3 v = new Vector3(scale * Time.deltaTime, scale * Time.deltaTime, scale * Time.deltaTime);
            transform.localScale += v;
        }
    }
}
