using System.Linq;
using System;
using System.Collections.Generic;

public class BackendProperties
{
    public string BackendName { get; set; }
    public string BackendVersion { get; set; }
    public DateTime LastUpdateDate { get; set; }
    public List<List<Nduv>> Qubits { get; set; }
    public List<GateProperties> Gates { get; set; }
    public List<Nduv> General { get; set; }
    private Dictionary<string, object> AdditionalData { get; set; }

    public BackendProperties(
        string backendName,
        string backendVersion,
        DateTime lastUpdateDate,
        List<List<Nduv>> qubits,
        List<GateProperties> gates,
        List<Nduv> general,
        Dictionary<string, object> additionalData = null)
    {
        BackendName = backendName;
        BackendVersion = backendVersion;
        LastUpdateDate = lastUpdateDate;
        Qubits = qubits;
        Gates = gates;
        General = general;
        AdditionalData = additionalData ?? new Dictionary<string, object>();
    }

    public static BackendProperties FromDictionary(Dictionary<string, object> data)
    {
        var qubits = new List<List<Nduv>>();
        foreach (var qubit in (List<List<Dictionary<string, object>>>)data["qubits"])
        {
            var nduvs = new List<Nduv>();
            foreach (var nduv in qubit)
            {
                nduvs.Add(Nduv.FromDictionary(nduv));
            }
            qubits.Add(nduvs);
        }

        var gates = ((List<Dictionary<string, object>>)data["gates"]).Select(GateProperties.FromDictionary).ToList();
        var general = ((List<Dictionary<string, object>>)data["general"]).Select(Nduv.FromDictionary).ToList();

        return new BackendProperties(
            data["backend_name"].ToString(),
            data["backend_version"].ToString(),
            Convert.ToDateTime(data["last_update_date"]),
            qubits,
            gates,
            general,
            data
        );
    }

    public Dictionary<string, object> ToDictionary()
    {
        var dict = new Dictionary<string, object>
        {
            { "backend_name", BackendName },
            { "backend_version", BackendVersion },
            { "last_update_date", LastUpdateDate },
            { "qubits", Qubits.Select(q => q.Select(nduv => nduv.ToDictionary()).ToList()).ToList() },
            { "gates", Gates.Select(gate => gate.ToDictionary()).ToList() },
            { "general", General.Select(nduv => nduv.ToDictionary()).ToList() }
        };

        foreach (var kv in AdditionalData)
        {
            dict[kv.Key] = kv.Value;
        }

        return dict;
    }

    public override bool Equals(object obj)
    {
        return obj is BackendProperties other && ToDictionary().Equals(other.ToDictionary());
    }
}
