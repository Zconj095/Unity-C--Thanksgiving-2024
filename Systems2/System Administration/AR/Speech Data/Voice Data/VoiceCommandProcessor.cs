using UnityEngine;
using System.Collections.Generic;
public class VoiceCommandProcessor : MonoBehaviour
{
    public void ProcessCommand(AudioClip clip)
    {
        // Mock voice processing logic
        string[] mockCommands = { "Start Service", "Stop Service", "Show Stats" };
        string recognizedCommand = mockCommands[Random.Range(0, mockCommands.Length)];

        Debug.Log($"Recognized Command: {recognizedCommand}");
        ExecuteCommand(recognizedCommand);
    }

    void ExecuteCommand(string command)
    {
        switch (command)
        {
            case "Start Service":
                Debug.Log("Service started!");
                break;
            case "Stop Service":
                Debug.Log("Service stopped!");
                break;
            case "Show Stats":
                Debug.Log("Showing stats...");
                break;
        }
    }
}
