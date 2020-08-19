using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPhysicsRigidBody : MonoBehaviour
{
    [SerializeField]
    private float m_Mass = 1.0f;

    protected IntPtr m_RigidBodyIntPtr = IntPtr.Zero;

    private IntPtr m_ObjectShapeIntPtr = IntPtr.Zero;

    private Matrix4x4 m_RotationMatrix = new Matrix4x4();

    private Vector3 m_TranslationVector = new Vector3();

    private Matrix4x4 m_LocalToWorldMatrix = new Matrix4x4();

    private void Awake()
    {
        if(!BulletPhysicsManager.Instance)
        {
            Debug.LogError("No BulletPhysicsManager found! It is either not added or disabled." +
                " BulletPhysicsManager creates dynamic world." +
                " Disabling BulletPhysicsRigidBodies!");

            enabled = false;
        }
    }

    private void Start()
    {
        if (GetComponent<BulletObjectShape>())
        {
            m_ObjectShapeIntPtr = GetComponent<BulletObjectShape>().CreateShape();

            m_RigidBodyIntPtr = BulletPhysicsDLLAccessors.bulletCreateRigidBody(
                transform.position.x, transform.position.y, transform.position.z,
                transform.right.x, transform.up.x, transform.forward.x,
                transform.right.y, transform.up.y, transform.forward.y,
                transform.right.z, transform.up.z, transform.forward.z,
                m_Mass, m_ObjectShapeIntPtr);
        }
        else
        {
            Debug.LogError("No BulletObjectShape found! Disabling BulletPhysicsRigidBody!");
            enabled = false;
            return;
        }
    }

    private void FixedUpdate()
    {
        float posX = 0.0f;
        float posY = 0.0f;
        float posZ = 0.0f;

        float xx = 0.0f;
        float xy = 0.0f;
        float xz = 0.0f;
        float yx = 0.0f;
        float yy = 0.0f;
        float yz = 0.0f;
        float zx = 0.0f;
        float zy = 0.0f;
        float zz = 0.0f;

        BulletPhysicsDLLAccessors.bulletGetPositionAndBasis(m_RigidBodyIntPtr,
                                  out posX, out posY, out posZ,
                                  out xx, out xy, out xz,
                                  out yx, out yy, out yz,
                                  out zx, out zy, out zz);

        CreateMatrix(posX, posY, posZ,
                     xx, xy, xz,
                     yx, yy, yz,
                     zx, zy, zz);
    }
    
    private void CreateMatrix(float posX, float posY, float posZ,
                              float xx, float xy, float xz,
                              float yx, float yy, float yz,
                              float zx, float zy, float zz)
    {
        m_RotationMatrix[0, 0] = xx;
        m_RotationMatrix[1, 0] = xy;
        m_RotationMatrix[2, 0] = xz;
        m_RotationMatrix[3, 0] = 0.0f;

        m_RotationMatrix[0, 1] = yx;
        m_RotationMatrix[1, 1] = yy;
        m_RotationMatrix[2, 1] = yz;
        m_RotationMatrix[3, 1] = 0.0f;

        m_RotationMatrix[0, 2] = zx;
        m_RotationMatrix[1, 2] = zy;
        m_RotationMatrix[2, 2] = zz;
        m_RotationMatrix[3, 2] = 0.0f;

        m_RotationMatrix[0, 3] = 0.0f;
        m_RotationMatrix[1, 3] = 0.0f;
        m_RotationMatrix[2, 3] = 0.0f;
        m_RotationMatrix[3, 3] = 1.0f;

        m_TranslationVector[0] = posX;
        m_TranslationVector[1] = posY;
        m_TranslationVector[2] = posZ;

        m_LocalToWorldMatrix = Matrix4x4.Translate(m_TranslationVector) * m_RotationMatrix;
        
        SetGameObjectMatrix();
    }

    private void SetGameObjectMatrix()
    {
        Vector3 position;
        position.x = m_LocalToWorldMatrix.m03;
        position.y = m_LocalToWorldMatrix.m13;
        position.z = m_LocalToWorldMatrix.m23;
        transform.position = position;

        Vector3 forward;
        forward.x = m_LocalToWorldMatrix.m02;
        forward.y = m_LocalToWorldMatrix.m12;
        forward.z = m_LocalToWorldMatrix.m22;

        Vector3 upwards;
        upwards.x = m_LocalToWorldMatrix.m01;
        upwards.y = m_LocalToWorldMatrix.m11;
        upwards.z = m_LocalToWorldMatrix.m21;

        transform.rotation = Quaternion.LookRotation(forward, upwards);
    }

}
