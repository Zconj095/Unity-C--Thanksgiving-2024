using System;
using System.Collections.Generic;

public class GateConfig
{
    public string Name { get; set; }
    public List<string> Parameters { get; set; }
    public string QasmDefinition { get; set; }
    public List<List<int>> CouplingMap { get; set; }
    public List<List<int>> LatencyMap { get; set; }
    public bool? Conditional { get; set; }
    public string Description { get; set; }

    public GateConfig(
        string name,
        List<string> parameters,
        string qasmDefinition,
        List<List<int>> couplingMap = null,
        List<List<int>> latencyMap = null,
        bool? conditional = null,
        string description = null)
    {
        Name = name;
        Parameters = parameters;
        QasmDefinition = qasmDefinition;
        CouplingMap = couplingMap?.Count > 0 ? couplingMap : null;
        LatencyMap = latencyMap?.Count > 0 ? latencyMap : null;
        Conditional = conditional;
        Description = description;
    }

    public static GateConfig FromDictionary(Dictionary<string, object> data)
    {
        return new GateConfig(
            name: data["name"].ToString(),
            parameters: new List<string>((List<string>)data["parameters"]),
            qasmDefinition: data["qasm_def"].ToString(),
            couplingMap: data.ContainsKey("coupling_map") ? (List<List<int>>)data["coupling_map"] : null,
            latencyMap: data.ContainsKey("latency_map") ? (List<List<int>>)data["latency_map"] : null,
            conditional: data.ContainsKey("conditional") ? (bool?)data["conditional"] : null,
            description: data.ContainsKey("description") ? data["description"].ToString() : null
        );
    }

    public Dictionary<string, object> ToDictionary()
    {
        var dict = new Dictionary<string, object>
        {
            { "name", Name },
            { "parameters", Parameters },
            { "qasm_def", QasmDefinition }
        };

        if (CouplingMap != null) dict["coupling_map"] = CouplingMap;
        if (LatencyMap != null) dict["latency_map"] = LatencyMap;
        if (Conditional != null) dict["conditional"] = Conditional;
        if (Description != null) dict["description"] = Description;

        return dict;
    }

    public override bool Equals(object obj)
    {
        if (obj is GateConfig other)
        {
            return ToDictionary().Equals(other.ToDictionary());
        }
        return false;
    }

    public override string ToString()
    {
        return $"GateConfig({Name}, {string.Join(", ", Parameters)}, {QasmDefinition})";
    }
}
