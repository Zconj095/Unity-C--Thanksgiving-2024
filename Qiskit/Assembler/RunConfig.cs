using System;
using System.Collections.Generic;

public class RunConfig
{
    private Dictionary<string, object> properties;

    public RunConfig(Dictionary<string, object> initialProperties)
    {
        properties = initialProperties;
    }

    public bool HasProperty(string propertyName)
    {
        return properties.ContainsKey(propertyName);
    }

    public object GetProperty(string propertyName)
    {
        return properties.ContainsKey(propertyName) ? properties[propertyName] : null;
    }

    public Dictionary<string, object> ToDict()
    {
        return new Dictionary<string, object>(properties);
    }
    /// <summary>
    /// Number of shots for the execution.
    /// </summary>
    public int? Shots { get; set; }

    /// <summary>
    /// Seed for the simulator.
    /// </summary>
    public int? SeedSimulator { get; set; }

    /// <summary>
    /// Flag indicating whether to request memory from the backend (per-shot readouts).
    /// </summary>
    public bool? Memory { get; set; }

    /// <summary>
    /// List of parameter bindings.
    /// </summary>
    public List<Dictionary<string, object>> ParameterBinds { get; set; }

    /// <summary>
    /// Additional optional fields.
    /// </summary>
    public Dictionary<string, object> AdditionalFields { get; set; } = new Dictionary<string, object>();

    /// <summary>
    /// Initializes a new instance of the <see cref="RunConfig"/> class.
    /// </summary>
    /// <param name="shots">Number of shots.</param>
    /// <param name="seedSimulator">Seed for the simulator.</param>
    /// <param name="memory">Whether to request memory from the backend.</param>
    /// <param name="parameterBinds">List of parameter bindings.</param>
    /// <param name="additionalFields">Additional optional fields.</param>
    public RunConfig(
        int? shots = null,
        int? seedSimulator = null,
        bool? memory = null,
        List<Dictionary<string, object>> parameterBinds = null,
        Dictionary<string, object> additionalFields = null)
    {
        if (shots.HasValue)
        {
            Shots = shots;
        }

        if (seedSimulator.HasValue)
        {
            SeedSimulator = seedSimulator;
        }

        if (memory.HasValue)
        {
            Memory = memory;
        }

        if (parameterBinds != null)
        {
            ParameterBinds = parameterBinds;
        }

        if (additionalFields != null)
        {
            AdditionalFields = additionalFields;
        }
    }

    /// <summary>
    /// Creates a new <see cref="RunConfig"/> object from a dictionary.
    /// </summary>
    /// <param name="data">Dictionary containing RunConfig fields.</param>
    /// <returns>A new instance of <see cref="RunConfig"/>.</returns>
    public static RunConfig FromDictionary(Dictionary<string, object> data)
    {
        var shots = data.ContainsKey("shots") ? (int?)data["shots"] : null;
        var seedSimulator = data.ContainsKey("seed_simulator") ? (int?)data["seed_simulator"] : null;
        var memory = data.ContainsKey("memory") ? (bool?)data["memory"] : null;

        var parameterBinds = data.ContainsKey("parameter_binds") 
            ? (List<Dictionary<string, object>>)data["parameter_binds"] 
            : null;

        var additionalFields = new Dictionary<string, object>(data);
        additionalFields.Remove("shots");
        additionalFields.Remove("seed_simulator");
        additionalFields.Remove("memory");
        additionalFields.Remove("parameter_binds");

        return new RunConfig(shots, seedSimulator, memory, parameterBinds, additionalFields);
    }

    /// <summary>
    /// Converts the <see cref="RunConfig"/> object to a dictionary.
    /// </summary>
    /// <returns>A dictionary representation of the <see cref="RunConfig"/> object.</returns>
    public Dictionary<string, object> ToDictionary()
    {
        var dictionary = new Dictionary<string, object>();

        if (Shots.HasValue)
        {
            dictionary["shots"] = Shots.Value;
        }

        if (SeedSimulator.HasValue)
        {
            dictionary["seed_simulator"] = SeedSimulator.Value;
        }

        if (Memory.HasValue)
        {
            dictionary["memory"] = Memory.Value;
        }

        if (ParameterBinds != null)
        {
            dictionary["parameter_binds"] = ParameterBinds;
        }

        foreach (var field in AdditionalFields)
        {
            dictionary[field.Key] = field.Value;
        }

        return dictionary;
    }
}
