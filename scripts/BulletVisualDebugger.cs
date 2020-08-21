using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletVisualDebugger : MonoBehaviour
{
    enum VisualDebugState
    {
        Ready = 0,
        DataPresent
    };

    private float[] m_CameraParametersCache = new float[MemoryMapping.Camera_Parameters];
    private float[] m_CameraParametersCurrent = new float[MemoryMapping.Camera_Parameters];

    private void Start()
    {
        if(!MemoryMapping.MemOpen())
        {
            Debug.LogWarning("Could not open Memory File.");
        }
    }

    private void FetchParameters(out float[] parameters)
    {
        parameters = new float[MemoryMapping.Camera_Parameters];

        parameters[0] = transform.position.x;
        parameters[1] = transform.position.y;
        parameters[2] = transform.position.z;

        parameters[3] = transform.right.x;
        parameters[4] = transform.right.y;
        parameters[5] = transform.right.z;

        parameters[6] = transform.up.x;
        parameters[7] = transform.up.y;
        parameters[8] = transform.up.z;

        parameters[9]  = transform.forward.x;
        parameters[10] = transform.forward.y;
        parameters[11] = transform.forward.z;

        Buffer.BlockCopy(parameters, 0, m_CameraParametersCurrent, 0, parameters.Length * sizeof(float));

    }

    private bool AreEqual(float a, float b)
    {
        return Math.Abs(a - b) < float.Epsilon;
    }

    private bool DataChanged()
    {
        for (int i = 0; i < MemoryMapping.Camera_Parameters; ++i)
        {
            if (!AreEqual(m_CameraParametersCache[i], m_CameraParametersCurrent[i]))
            {
                return true;
            }
        }
        return false;
    }

    private void LateUpdate()
    {
        float[] floatArray;
        FetchParameters(out floatArray);

        if (MemoryMapping.IsMemFileOpen && DataChanged())
        {
            Buffer.BlockCopy(m_CameraParametersCurrent, 0, m_CameraParametersCache, 0, m_CameraParametersCurrent.Length * sizeof(float));

            byte[] data = new byte[(floatArray.Length * sizeof(float)) + sizeof(int)];
            Buffer.BlockCopy(floatArray, 0, data, sizeof(int), data.Length - sizeof(int));

            byte[] dataTypeBytes = BitConverter.GetBytes((int)VisualDebugState.DataPresent);
            Buffer.BlockCopy(dataTypeBytes, 0, data, 0, sizeof(int));

            MemoryMapping.WriteToMem(data);
        }
    }

    void OnApplicationQuit()
    {
        MemoryMapping.MemClose();
    }
}
