using System.Diagnostics;
using UnityEngine;
using System.Collections.Generic;

public class BiometricAuth : MonoBehaviour
{
    public void Authenticate()
    {
        Process process = new Process();
        process.StartInfo.FileName = "powershell.exe";
        process.StartInfo.Arguments = "Start-Process 'HelloBiometric' -Verb runas"; // Example command
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardOutput = true;

        process.Start();
        process.WaitForExit();

        UnityEngine.Debug.Log("Authentication process completed.");
    }
}
