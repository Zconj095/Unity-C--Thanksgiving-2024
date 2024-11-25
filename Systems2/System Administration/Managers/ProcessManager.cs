using UnityEngine;
using UnityEngine.UI;
using System.Diagnostics;

public class ProcessManager : MonoBehaviour
{
    public InputField processNameInput;
    public Text feedbackText;

    public void KillProcess()
    {
        string processName = processNameInput.text;

        try
        {
            foreach (var process in Process.GetProcessesByName(processName))
            {
                process.Kill();
            }
            feedbackText.text = $"Process {processName} terminated.";
        }
        catch (System.Exception e)
        {
            feedbackText.text = $"Error: {e.Message}";
        }
    }

    public void ListProcesses()
    {
        Process[] processes = Process.GetProcesses();
        foreach (var process in processes)
        {
            UnityEngine.Debug.Log($"Process: {process.ProcessName}, ID: {process.Id}");
        }
    }
}
