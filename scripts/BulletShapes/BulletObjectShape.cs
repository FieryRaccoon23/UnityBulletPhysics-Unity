using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BulletObjectShape : MonoBehaviour
{
    protected IntPtr m_ObjectShapeIntPtr = IntPtr.Zero;

    public IntPtr ObjectShapeIntPtr
    {
        get
        {
            return m_ObjectShapeIntPtr;
        }
    }

    public virtual IntPtr CreateShape()
    {
        return IntPtr.Zero;
    }
}