using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    protected GameObject Range;

    protected virtual void Start()
    {
        foreach (Transform t in TransformNavigation.GetAllChildren(transform, false))
        {
            if (t.GetComponent<MeshCollider>() != null)
            {
                if (t.GetComponent<MeshCollider>().gameObject != null && t.GetComponent<MeshCollider>().sharedMesh.name == "pCone1")
                {
                    Range = t.GetComponent<MeshCollider>().gameObject;
                }
            }
        }
    }

   public virtual void ActivateEffect(Collider other)
   {

   }
}
