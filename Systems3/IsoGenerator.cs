using System;
using System.Diagnostics; // Required for Process and ProcessStartInfo
using System.IO;
using UnityEngine;

public class IsoGenerator
{
    public static void GenerateISO(string sourcePath, string isoPath)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "mkisofs.exe",
            Arguments = $"-o \"{isoPath}\" -V \"ISO_VOLUME\" \"{sourcePath}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(psi))
        {
            process.WaitForExit();
            UnityEngine.Debug.Log(process.StandardOutput.ReadToEnd());
        }
    }
}
