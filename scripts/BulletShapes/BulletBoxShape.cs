using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBoxShape : BulletObjectShape
{
    [SerializeField]
    private Vector3 m_BoxDimensions = new Vector3(1.0f, 1.0f, 1.0f);

    public override IntPtr CreateShape()
    {
        m_ObjectShapeIntPtr = BulletPhysicsDLLAccessors.bulletCreateBoxShape(m_BoxDimensions.x, m_BoxDimensions.y, m_BoxDimensions.z);
        return m_ObjectShapeIntPtr;
    }

    void OnDrawGizmosSelected()
    {
        Matrix4x4 rotationMatrix = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(1.0f, 1.0f, 1.0f));
        Gizmos.matrix = rotationMatrix;

        Gizmos.color = new Color(1, 0, 0, 0.5f);
        Gizmos.DrawCube(transform.position, m_BoxDimensions);
    }
}
