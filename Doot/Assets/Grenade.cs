using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField]
    GameObject trigger;
    public float throwForce;
    public List<Enemy> enemyList;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 go = transform.up + transform.forward * throwForce;
        rb.AddForce(go);
        StartCoroutine(BlowUp());
    }

    IEnumerator BlowUp()
    {
        yield return new WaitForSeconds(3);
        trigger.SetActive(true);
        yield return new WaitForSeconds(0.2f);

        for (int i = 0; i < enemyList.Count; i++)
            GrenadeEffect(enemyList[i]);

        Destroy(this.gameObject);
    }

    void GrenadeEffect(Enemy enemy)
    {
        if(enemy != null)
            Debug.Log(enemy.name + " was hit by grenade.");
    }
}
