using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DootGun : Gun
{
    public LayerMask mask = -1;
    public Vector3 p1;
    CharacterController charCtrl;
    Animator animator;
    AudioSource audioSource;
    BuffManager buffManager;
    public float range;
    private float lastFire;
    public float rateOfFire;
    public float dootForce;
    private int layerMask;

    PlayerController playController;

    bool m_ActivatedDoot = false;
    float Timer = 1f;

    // Start is called before the first frame update
    override protected void Start()
    {
        base.Start();
        charCtrl = GetComponentInParent<CharacterController>();
        playController = GetComponentInParent<PlayerController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        buffManager = FindObjectOfType<BuffManager>();
        lastFire = -rateOfFire;
        layerMask = LayerMask.GetMask("Dootable");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1") && lastFire + rateOfFire < Time.time && playController.alive == Alive.alive)
        {
            Doot();
        }

        if (m_ActivatedDoot == true)
        {
            Timer -= Time.deltaTime;
            if (Timer <= 0)
            {
                m_ActivatedDoot = false;
                Range.SetActive(false);
                Timer = 1f;
            }
        }
    }

    void Doot()
    {
        animator.SetTrigger("Doot");
        audioSource.Play();
        //RaycastHit hit;

        //Vector3 p1 = transform.position + charCtrl.center;
        //if (Physics.SphereCast(p1, charCtrl.height / 2, transform.forward, out hit, range, layerMask))
        //{
        //    if (hit.collider.tag == "Enemy")
        //    {
        //        Debug.Log(hit.collider.name);
        //        Grunt g = hit.collider.GetComponentInParent<Grunt>();
        //        g.Doot(this.transform.forward, dootForce);
        //    }
        //}

        Range.SetActive(true);
        m_ActivatedDoot = true;

        lastFire = Time.time;
    }

    public override void ActivateEnterEffect(Collider other)
    {
        if (other.GetComponent<Enemy>() != null && other.GetComponent<Rigidbody>() != null)
        {
            other.GetComponent<Enemy>().Doot();
            other.GetComponent<Rigidbody>().AddForce(this.transform.forward * dootForce);
        }
    }
}
