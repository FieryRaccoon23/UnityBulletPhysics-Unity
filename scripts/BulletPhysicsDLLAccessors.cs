using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class BulletPhysicsDLLAccessors
{
    const string m_BulletPhysicsDLL = "BulletPhysics";

    [DllImport(m_BulletPhysicsDLL)]
    public static extern void bulletInitDefault();

    [DllImport(m_BulletPhysicsDLL)]
    public static extern void bulletSetGravity(float gravity);

    [DllImport(m_BulletPhysicsDLL)]
    public static extern IntPtr bulletCreateBoxShape(float length, float width, float height);

    [DllImport(m_BulletPhysicsDLL)]
    public static extern IntPtr bulletCreateSphereShape(float radius);

    [DllImport(m_BulletPhysicsDLL)]
    public static extern IntPtr bulletCreateRigidBody(float posX, float posY, float posZ,
                                               float xx, float xy, float xz,
                                               float yx, float yy, float yz,
                                               float zx, float zy, float zz,
                                               float massValue, IntPtr shape);

    [DllImport(m_BulletPhysicsDLL)]
    public static extern void bulletGetPositionAndBasis(IntPtr body,
                                                 out float posX, out float posY, out float posZ,
                                                 out float xx, out float xy, out float xz,
                                                 out float yx, out float yy, out float yz,
                                                 out float zx, out float zy, out float zz);

    [DllImport(m_BulletPhysicsDLL)]
    public static extern void bulletStepSimulate(float deltaTime);

    [DllImport(m_BulletPhysicsDLL)]
    public static extern void bulletClean();
}
