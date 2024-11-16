using System;
using System.Collections.Generic;
using UnityEngine;

public class QuantumAlgorithm
{
    private QuantumInstance _quantumInstance;

    public QuantumAlgorithm(QuantumInstance quantumInstance = null)
    {
        if (quantumInstance != null)
        {
            QuantumInstance = quantumInstance;
        }
    }

    // Random generator property
    public System.Random RandomGenerator { get; private set; } = new System.Random();

    // QuantumInstance property
    public QuantumInstance QuantumInstance
    {
        get => _quantumInstance;
        set
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value), "QuantumInstance cannot be null.");
            }
            _quantumInstance = value;
        }
    }

    // Backend property
    public object Backend
    {
        get => QuantumInstance?.Backend;
        set => SetBackend(value);
    }

    // Run method for executing the algorithm
    public Dictionary<string, object> Run(QuantumInstance quantumInstance = null, Dictionary<string, object> config = null)
    {
        if (quantumInstance == null && QuantumInstance == null)
        {
            throw new InvalidOperationException("A QuantumInstance or Backend must be supplied to run the quantum algorithm.");
        }

        if (quantumInstance != null)
        {
            QuantumInstance = quantumInstance;
        }

        if (config != null)
        {
            QuantumInstance.SetConfig(config);
        }

        return ExecuteAlgorithm();
    }

    // Method for subclasses to override to define algorithm behavior
    public virtual Dictionary<string, object> ExecuteAlgorithm()
    {
        Debug.Log("Default quantum algorithm executed. Override this method in subclasses.");
        return new Dictionary<string, object> { { "result", "Default execution" } };
    }

    // Set backend with optional configuration
    public void SetBackend(object backend, Dictionary<string, object> config = null)
    {
        if (backend == null)
        {
            throw new ArgumentNullException(nameof(backend), "Backend cannot be null.");
        }

        QuantumInstance = new QuantumInstance(backend);

        if (config != null)
        {
            QuantumInstance.SetConfig(config);
        }
    }
}

// Supporting QuantumInstance class
public class QuantumInstance
{
    private readonly object _backend;
    private readonly Dictionary<string, object> _config;

    public QuantumInstance(object backend)
    {
        _backend = backend ?? throw new ArgumentNullException(nameof(backend), "Backend cannot be null.");
        _config = new Dictionary<string, object>();
    }

    public object Backend => _backend;

    public void SetConfig(Dictionary<string, object> config)
    {
        foreach (var kvp in config)
        {
            _config[kvp.Key] = kvp.Value;
        }
    }
}
