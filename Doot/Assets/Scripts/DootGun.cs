using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DootGun : MonoBehaviour
{
    public LayerMask mask = -1;
    private float distance;
    public Vector3 p1;
    CharacterController charCtrl;
    // Start is called before the first frame update
    void Start()
    {
        charCtrl = GetComponentInParent<CharacterController>();
        Debug.Log(name + " has found " + charCtrl.name);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Doot();
        }
    }

    void Doot()
    {
        RaycastHit hit;

        Vector3 p1 = transform.position + charCtrl.center;
        if (Physics.SphereCast(p1, charCtrl.height / 2, transform.forward, out hit, 20))
        {
            if (hit.collider.tag == "Enemy")
            {
                Debug.Log("Push");
            }
        }
    }
}
