using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhysicsManager : MonoBehaviour
{
    private static BulletPhysicsManager m_Instance;

    public static BulletPhysicsManager Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = FindObjectOfType<BulletPhysicsManager>();
            }

            return m_Instance;
        }
    }

    private void Awake()
    {
        if (m_Instance != null && m_Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            m_Instance = this;
        }
    }

    private void Start()
    {
        BulletPhysicsDLLAccessors.bulletInitDefault();
    }

    void OnApplicationQuit()
    {
        BulletPhysicsDLLAccessors.bulletClean();
    }

    private void FixedUpdate()
    {
        BulletPhysicsDLLAccessors.bulletStepSimulate(Time.deltaTime);
    }

}
