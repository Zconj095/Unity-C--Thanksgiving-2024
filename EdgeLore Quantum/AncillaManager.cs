using System.Collections.Generic;
using UnityEngine;

public class AncillaManager : MonoBehaviour
{
    [Header("Registers")]
    public int primaryQubitCount = 4; // Changed to public
    public int ancillaQubitCount = 2; // Changed to public

    private List<string> primaryRegisters = new List<string>();
    private List<string> ancillaRegisters = new List<string>();

    public void InitializeRegisters() // Changed to public
    {
        primaryRegisters.Clear();
        ancillaRegisters.Clear();

        for (int i = 0; i < primaryQubitCount; i++)
            primaryRegisters.Add($"q{i}");

        for (int i = 0; i < ancillaQubitCount; i++)
            ancillaRegisters.Add($"a{i}");

        Debug.Log("Primary Registers: " + string.Join(", ", primaryRegisters));
        Debug.Log("Ancilla Registers: " + string.Join(", ", ancillaRegisters));
    }

    public string GetAncilla(int index)
    {
        if (index < 0 || index >= ancillaRegisters.Count)
        {
            Debug.LogError("Invalid ancilla index.");
            return null;
        }
        return ancillaRegisters[index];
    }

    public void AddAncilla()
    {
        string newAncilla = $"a{ancillaRegisters.Count}";
        ancillaRegisters.Add(newAncilla);
        Debug.Log($"Added Ancilla: {newAncilla}");
    }

    public void RemoveAncilla(int index)
    {
        if (index < 0 || index >= ancillaRegisters.Count)
        {
            Debug.LogError("Invalid ancilla index.");
            return;
        }
        string removed = ancillaRegisters[index];
        ancillaRegisters.RemoveAt(index);
        Debug.Log($"Removed Ancilla: {removed}");
    }
}
