using System;
using System.Collections.Generic;

public class StatevectorSimulator
{
    private string backendName = "statevector_simulator";
    private string backendVersion = "1.0.0"; // Placeholder for version
    private int maxQubits = 30; // Example value for maximum qubits
    private List<string> basisGates;
    private List<string> customInstructions;
    private string description;
    private Dictionary<string, object> options;

    public StatevectorSimulator(Dictionary<string, object> backendOptions = null)
    {
        // Initialize default basis gates
        basisGates = new List<string>
        {
            "u1", "u2", "u3", "cx", "cz", "h", "x", "y", "z", "id"
        };

        // Initialize default custom instructions
        customInstructions = new List<string>
        {
            "save_statevector", "save_density_matrix", "set_statevector", "reset"
        };

        // Initialize default options
        options = backendOptions ?? new Dictionary<string, object>();
        description = "A C++ statevector circuit simulator";

        // Add a warning about deprecation
        Console.WriteLine(
            "Warning: The `StatevectorSimulator` backend is deprecated. Use `AerSimulator` instead."
        );
    }

    public override string ToString()
    {
        string device = options.ContainsKey("device") ? (string)options["device"] : "CPU";
        return $"StatevectorSimulator(backendName={backendName}, device={device})";
    }

    public void SetOption(string key, object value)
    {
        if (key == "device")
        {
            SetDevice((string)value);
        }
        else
        {
            options[key] = value;
        }
    }

    private void SetDevice(string device)
    {
        if (!IsValidDevice(device))
        {
            throw new ArgumentException($"Invalid device: {device}. Available devices are CPU and GPU.");
        }
        options["device"] = device;
    }

    private bool IsValidDevice(string device)
    {
        List<string> validDevices = new List<string> { "CPU", "GPU" };
        return validDevices.Contains(device);
    }

    public List<string> AvailableDevices()
    {
        // Simulated available devices
        return new List<string> { "CPU", "GPU" };
    }

    public Dictionary<string, object> Configuration()
    {
        return new Dictionary<string, object>
        {
            { "backend_name", backendName },
            { "backend_version", backendVersion },
            { "n_qubits", maxQubits },
            { "basis_gates", basisGates },
            { "custom_instructions", customInstructions },
            { "description", description }
        };
    }

    public void Execute(List<string> circuits)
    {
        string device = options.ContainsKey("device") ? (string)options["device"] : "CPU";
        Console.WriteLine($"Executing on device: {device}");

        foreach (string circuit in circuits)
        {
            Console.WriteLine($"Simulating circuit: {circuit}");
        }
    }
}
