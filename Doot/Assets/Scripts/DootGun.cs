using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DootGun : MonoBehaviour
{
    public LayerMask mask = -1;
    public Vector3 p1;
    CharacterController charCtrl;
    Animator animator;
    AudioSource audioSource;
    private GameObject trumpet;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        trumpet = GameObject.FindGameObjectWithTag("Trumpet");
        charCtrl = GetComponentInParent<CharacterController>();
        animator = trumpet.GetComponent<Animator>();
        audioSource = trumpet.GetComponent<AudioSource>();
        Debug.Log(charCtrl.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Doot();
        }
    }

    void Doot()
    {
        animator.SetTrigger("Doot");
        audioSource.Play();
        RaycastHit hit;

        Vector3 p1 = transform.position + charCtrl.center;
        if (Physics.SphereCast(p1, charCtrl.height / 2, transform.forward, out hit, range))
        {
            if (hit.collider.tag == "Enemy")
            {
                Debug.Log("Push");
            }
        }
    }
}
