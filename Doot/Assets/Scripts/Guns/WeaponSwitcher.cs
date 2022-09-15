using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{

    [SerializeField]
    Transform m_WeaponStorage;

    Gun m_CurrentGun;

    [SerializeField]
    List<Gun> m_PotentialGuns;

    private Gun[] m_Guns;

    // Start is called before the first frame update
    void Start()
    {
        SetupGuns();
    }

    // Update is called once per frame
    void Update()
    {
        WeaponSwitchInput();
    }

    void SetupGuns()
    {
        m_Guns = new Gun[m_PotentialGuns.Count];

        for (int i = 0; i < m_PotentialGuns.Count; i++)
        {
            if (m_PotentialGuns[i] != null)
            {
                m_Guns[i] = Instantiate<Gun>(m_PotentialGuns[i], m_WeaponStorage);
                m_Guns[i].transform.localPosition = Vector3.zero;
                m_Guns[i].gameObject.SetActive(false);
            }
        }
    }

    void SelectGun(int gunIndex)
    {
        gunIndex -= 1;

        if (m_Guns != null && m_Guns[gunIndex] != null)
        {
            if (m_CurrentGun != null)
            {
                m_CurrentGun.transform.localPosition = Vector3.zero;
                m_CurrentGun.gameObject.SetActive(false);
            }
            m_CurrentGun = m_Guns[gunIndex];
            m_CurrentGun.gameObject.SetActive(true);
            m_CurrentGun.transform.localPosition = Vector3.zero;
        }
    }

    void WeaponSwitchInput()
    {
        if (Input.anyKey)
        {
            for (int i = 0; i < 10; i++)
            {
                if (Input.GetKeyDown("" + i))
                {
                    SelectGun(i);
                }
            }
        }
    }
}
