using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSphereShape : BulletObjectShape
{
    [SerializeField]
    private float m_SphereRadius = 1.0f;

    public override IntPtr CreateShape()
    {
        m_ObjectShapeIntPtr = BulletPhysicsDLLAccessors.bulletCreateSphereShape(m_SphereRadius);
        return m_ObjectShapeIntPtr;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawSphere(transform.position, m_SphereRadius);
    }
}
