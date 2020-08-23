using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.MemoryMappedFiles;
using UnityEngine;

public class MemoryMapping
{
    enum VisualDebugState
    {
        Ready = 0,
        DataPresent,
        ClosingMemMap
    };

    public const int Camera_Parameters = 12;

    private const string m_MapName = "VisualDebuggerMemMap";
    private const int m_MapSize = (Camera_Parameters * sizeof(float)) + (sizeof(int));

    private static float[] data = new float[3];
    private static MemoryMappedFile m_MemoryMappedFile;
    private static bool m_IsMemFileOpen = false;

    public static bool IsMemFileOpen
    {
        get
        {
            return m_IsMemFileOpen;
        }
    }

    public static bool  CreateMemoryMappedFIle()
    {
        try 
        {
            m_MemoryMappedFile = MemoryMappedFile.CreateOrOpen(m_MapName, m_MapSize);

            if (m_MemoryMappedFile == null)
            {
                Debug.LogWarning("Could not create/open Memory Mapped File: VisualDebuggerMemMap.");
                return false;
            }
        }
        catch (Exception e)
        {
            Debug.LogWarning("Could not create Memory Mapped File. Error: " + e.ToString());
            return false;
        }

        m_IsMemFileOpen = true;
        return true;
    }

    public static bool OpenMemoryMappedFIle()
    {
        try
        {
            m_MemoryMappedFile = MemoryMappedFile.OpenExisting(m_MapName);
            if(m_MemoryMappedFile == null)
            {
                Debug.LogWarning("Could not open Memory Mapped File: VisualDebuggerMemMap.");
                return false;
            }
            
        }
        catch
        {
            Debug.LogWarning("Could not open Memory Mapped File.");
            return false;
        }

        m_IsMemFileOpen = true;
        return true;
    }

    public static void CloseMemoryMappedFIle()
    {
        byte[] dummyData = new byte[m_MapSize];
        WriteToMem(dummyData, true);

        if (m_MemoryMappedFile != null)
            m_MemoryMappedFile.Dispose();

        m_IsMemFileOpen = false;
    }

    public static void PrepareDataToSend(float[] floatArray, out byte[] data)
    {
        data = new byte[m_MapSize];
        Buffer.BlockCopy(floatArray, 0, data, sizeof(int), data.Length - sizeof(int));
    }

    public static void WriteToMem(byte[] data, bool close = false)
    {
        try
        {
            using (MemoryMappedViewStream stream = m_MemoryMappedFile.CreateViewStream())
            {
                if (!close)
                {
                    byte[] dataTypeBytes = BitConverter.GetBytes((int)VisualDebugState.DataPresent);
                    Buffer.BlockCopy(dataTypeBytes, 0, data, 0, sizeof(int));
                }
                else
                {
                    byte[] dataTypeBytes = BitConverter.GetBytes((int)VisualDebugState.ClosingMemMap);
                    Buffer.BlockCopy(dataTypeBytes, 0, data, 0, sizeof(int));
                }

                stream.Write(data, 0, data.Length);
            }
        }
        catch
        {
            Debug.LogWarning("Could not create Memory Mapped File Stream.");
        }

    }

    public static float ReadFromMem()
    {
        return 0.0f;
    }
}
