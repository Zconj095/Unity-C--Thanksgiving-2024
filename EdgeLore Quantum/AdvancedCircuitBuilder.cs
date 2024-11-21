using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdvancedCircuitBuilder : MonoBehaviour
{
    public GameObject QubitPrefab;
    public GameObject GatePrefab;
    public Transform QubitContainer;
    public Transform GateContainer;
    public Dropdown GateDropdown;
    public Button AddQubitButton;
    public Button AddGateButton;
    public Button UndoButton;
    public Button RedoButton;

    private List<GameObject> qubits = new List<GameObject>();
    private List<QuantumGate> circuitGates = new List<QuantumGate>();
    private Stack<List<QuantumGate>> undoStack = new Stack<List<QuantumGate>>();
    private Stack<List<QuantumGate>> redoStack = new Stack<List<QuantumGate>>();

    void Start()
    {
        AddQubitButton.onClick.AddListener(AddQubit);
        AddGateButton.onClick.AddListener(AddGate);
        UndoButton.onClick.AddListener(Undo);
        RedoButton.onClick.AddListener(Redo);
    }

    void AddQubit()
    {
        GameObject qubit = Instantiate(QubitPrefab, new Vector3(qubits.Count * 2.0f, 0, 0), Quaternion.identity, QubitContainer);
        qubit.name = $"Qubit {qubits.Count}";
        qubits.Add(qubit);
        Debug.Log($"Added Qubit {qubits.Count - 1}");
    }

    void AddGate()
    {
        if (qubits.Count == 0)
        {
            Debug.LogWarning("No qubits available to add gates.");
            return;
        }

        string selectedGate = GateDropdown.options[GateDropdown.value].text;
        QuantumGate gate = null;

        switch (selectedGate)
        {
            case "Hadamard":
                gate = QuantumGate.Hadamard(0); // Default to Qubit 0 for simplicity
                break;
            case "CNOT":
                if (qubits.Count > 1)
                    gate = QuantumGate.CNOT(0, 1); // Default to first two qubits
                break;
            // Add more gates as needed
        }

        if (gate != null)
        {
            circuitGates.Add(gate);
            SaveStateToUndoStack();
            Debug.Log($"Added {selectedGate} gate to the circuit.");
        }
    }

    void Undo()
    {
        if (undoStack.Count > 0)
        {
            redoStack.Push(new List<QuantumGate>(circuitGates));
            circuitGates = undoStack.Pop();
            Debug.Log("Undo action performed.");
        }
    }

    void Redo()
    {
        if (redoStack.Count > 0)
        {
            undoStack.Push(new List<QuantumGate>(circuitGates));
            circuitGates = redoStack.Pop();
            Debug.Log("Redo action performed.");
        }
    }

    private void SaveStateToUndoStack()
    {
        undoStack.Push(new List<QuantumGate>(circuitGates));
        redoStack.Clear(); // Clear redo stack whenever a new action is performed
    }
}
