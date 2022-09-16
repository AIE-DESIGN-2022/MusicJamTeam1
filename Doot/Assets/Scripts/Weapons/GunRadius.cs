using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRadius : MonoBehaviour
{
    Gun m_OwningGun;

    // Start is called before the first frame update
    void Start()
    {
        m_OwningGun = GetComponentInParent<Gun>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (m_OwningGun != null)
        {
            m_OwningGun.ActivateEnterEffect(other);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (m_OwningGun != null)
        {
            m_OwningGun.ActivateStayEffect(other);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (m_OwningGun != null)
        {
            m_OwningGun.ActivateExitEffect(other);
        }
    }
}
