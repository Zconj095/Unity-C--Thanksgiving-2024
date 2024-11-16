using System;
using System.Collections.Generic;
using UnityEngine;
public class GateProperties
{
    public List<int> Qubits { get; set; }
    public string Gate { get; set; }
    public List<Nduv> Parameters { get; set; }
    private Dictionary<string, object> AdditionalData { get; set; }

    public GateProperties(List<int> qubits, string gate, List<Nduv> parameters, Dictionary<string, object> additionalData = null)
    {
        Qubits = qubits;
        Gate = gate;
        Parameters = parameters;
        AdditionalData = additionalData ?? new Dictionary<string, object>();
    }

    public static GateProperties FromDictionary(Dictionary<string, object> data)
    {
        var parameters = new List<Nduv>();
        foreach (var param in (List<Dictionary<string, object>>)data["parameters"])
        {
            parameters.Add(Nduv.FromDictionary(param));
        }

        return new GateProperties(
            (List<int>)data["qubits"],
            data["gate"].ToString(),
            parameters,
            data
        );
    }

    public Dictionary<string, object> ToDictionary()
    {
        var dict = new Dictionary<string, object>
        {
            { "qubits", Qubits },
            { "gate", Gate },
            { "parameters", Parameters.ConvertAll(param => param.ToDictionary()) }
        };

        foreach (var kv in AdditionalData)
        {
            dict[kv.Key] = kv.Value;
        }

        return dict;
    }

    public override bool Equals(object obj)
    {
        return obj is GateProperties other && ToDictionary().Equals(other.ToDictionary());
    }
}
