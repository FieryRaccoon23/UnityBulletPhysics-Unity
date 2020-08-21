using System;
using System.Collections;
using System.Collections.Generic;
using System.IO.MemoryMappedFiles;
using UnityEngine;

public class MemoryMapping
{
    public const int Camera_Parameters = 12;

    private const string m_MapName = "VisualDebuggerMemMap";
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

    static public bool MemOpen()
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

    static public void MemClose()
    {
        if (m_MemoryMappedFile != null)
            m_MemoryMappedFile.Dispose();

        m_IsMemFileOpen = false;
    }

    static public void WriteToMem(byte[] data)
    {
        try
        {
            using (MemoryMappedViewStream stream = m_MemoryMappedFile.CreateViewStream())
            {
                stream.Write(data, 0, data.Length);
                stream.Flush();
            }
        }
        catch
        {
            Debug.LogWarning("Could not create Memory Mapped File Stream.");
        }

    }

    static public float ReadFromMem()
    {
        return 0.0f;
    }
}
