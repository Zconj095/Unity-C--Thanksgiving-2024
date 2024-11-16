using System;
using System.Collections.Generic;
using UnityEngine;

public class AerBackend : MonoBehaviour
{
    // Configuration and Properties
    protected AerBackendConfiguration Configuration { get; private set; }
    protected AerBackendProperties Properties { get; private set; }
    protected Dictionary<string, object> Options { get; private set; }
    public string BackendName { get; private set; }
    public string Description { get; private set; }
    public string BackendVersion { get; private set; }

    public AerBackend(AerBackendConfiguration configuration, AerBackendProperties properties = null)
    {
        Configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        Properties = properties;
        Options = new Dictionary<string, object>();
        BackendName = configuration.BackendName;
        Description = configuration.Description;
        BackendVersion = configuration.BackendVersion;
    }

    // Method to run simulation (default implementation)
    public virtual string Run(List<QuantumCircuit> circuits, Dictionary<string, List<double>> parameterBinds = null)
    {
        if (circuits == null || circuits.Count == 0)
        {
            throw new ArgumentException("No circuits provided to simulate.");
        }

        // Default behavior: Just display the circuits
        foreach (var circuit in circuits)
        {
            Console.WriteLine($"Default Simulation: {circuit}");
        }

        return "Default simulation completed.";
    }

    public void SetOption(string key, object value)
    {
        if (Options.ContainsKey(key))
        {
            Options[key] = value;
        }
        else
        {
            Options.Add(key, value);
        }
    }

    public object GetOption(string key)
    {
        return Options.ContainsKey(key) ? Options[key] : null;
    }

    public void SetOptions(Dictionary<string, object> newOptions)
    {
        foreach (var option in newOptions)
        {
            SetOption(option.Key, option.Value);
        }
    }

    public override string ToString()
    {
        return $"{GetType().Name}('{BackendName}')";
    }
}
