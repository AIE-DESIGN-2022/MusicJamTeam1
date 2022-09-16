using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhootDootGun : Gun
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
    private bool animCheck;
    public AudioClip charge;
    public AudioClip doot;
    private bool audioCheck;

    PlayerController playController;

    bool m_ActivatedDoot = false;
    float Timer = 1f;
    bool m_HoldingDownMouseButton = false;

    [SerializeField]
    float m_StopRange = 2.0f;

    List<Collider> m_Targetcolliders = new List<Collider>();

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
        m_HoldingDownMouseButton = Input.GetButton("Fire1");

        if (m_HoldingDownMouseButton && lastFire + rateOfFire < Time.time /*&& playController.alive == Alive.alive*/)
        {
            animator.SetBool("Doot", true);
            if (audioCheck == false)
            {
                audioSource.clip = charge;
                audioSource.loop = true;
                audioSource.Play();
                audioCheck = true;
            }
            PhootDoot();
        }
        else if (!m_HoldingDownMouseButton)
        {
            animator.SetBool("Doot", false);
            if (audioCheck == true)
            {
                audioSource.clip = doot;
                audioSource.loop = false;
                audioSource.Play();
                m_ActivatedDoot = false;
                audioCheck = false;
            }

            if (Range)
            {
                Range.SetActive(false);
            }

            foreach(Collider collider in m_Targetcolliders)
            {
                collider.GetComponent<Rigidbody>().AddForce(-this.transform.right * dootForce * 100.0f);
                collider.GetComponent<Enemy>().Doot();
                lastFire = Time.time;
            }

            m_Targetcolliders.Clear();
        }      
    }

    void PhootDoot()
    {
        //animator.SetTrigger("Doot");
        //audioSource.Play();
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

        if (Range)
        {
            Range.SetActive(true);
        }
        
        m_ActivatedDoot = true;
    }


    public override void ActivateStayEffect(Collider other)
    {
        if (m_HoldingDownMouseButton && playController.alive == Alive.alive)
        {
            if (other.GetComponent<Enemy>() != null && other.GetComponent<Rigidbody>() != null)
            {
                if (m_Targetcolliders != null && !m_Targetcolliders.Contains(other))
                {
                    m_Targetcolliders.Add(other);
                }

                float test = Vector3.Distance(this.transform.position, other.transform.position);

                if (Vector3.Distance(this.transform.position, other.transform.position) > m_StopRange)
                {
                    other.GetComponent<Rigidbody>().AddForce(this.transform.right * dootForce);
                }
                else
                {
                    other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
        
    }

    public override void ActivateExitEffect(Collider other)
    {
        if (m_Targetcolliders != null && m_Targetcolliders.Contains(other))
        {
            m_Targetcolliders.Remove(other);
        }

    }
}
