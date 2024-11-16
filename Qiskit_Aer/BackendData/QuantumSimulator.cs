using System;
using System.IO;
using UnityEngine;

public static class QuantumSimulator
{
    public static readonly double SYSTEM_MEMORY_GB = SystemInfo.systemMemorySize / 1024.0;
    public static readonly int MAX_QUBITS_STATEVECTOR = (int)Math.Floor(Math.Log(SYSTEM_MEMORY_GB * Math.Pow(1024, 3) / 16, 2));
    public static readonly string LIBRARY_DIR = Application.dataPath + "/Libraries/";

    public static void InitializeLibraries()
    {
        Debug.Log($"Initializing libraries from: {LIBRARY_DIR}");
        // Call a native method to load libraries (if applicable)
    }
}
