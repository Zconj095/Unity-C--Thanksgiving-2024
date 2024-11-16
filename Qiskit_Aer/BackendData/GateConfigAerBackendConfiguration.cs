using System;
using System.Collections.Generic;

public class GateConfigAerBackendConfiguration
{
    public string BackendName { get; set; }
    public string BackendVersion { get; set; }
    public int NumberOfQubits { get; set; }
    public List<string> BasisGates { get; set; }
    public List<GateConfig> Gates { get; set; }
    public int MaxShots { get; set; }
    public List<List<int>> CouplingMap { get; set; }
    public List<string> SupportedInstructions { get; set; }
    public bool DynamicRepRateEnabled { get; set; }
    public List<double> RepDelayRange { get; set; }
    public double? DefaultRepDelay { get; set; }
    public int? MaxExperiments { get; set; }
    public Dictionary<string, object> AdditionalData { get; private set; } = new Dictionary<string, object>();

    public GateConfigAerBackendConfiguration(
        string backendName,
        string backendVersion,
        int numberOfQubits,
        List<string> basisGates,
        List<GateConfig> gates,
        int maxShots,
        List<List<int>> couplingMap,
        List<string> supportedInstructions = null,
        bool dynamicRepRateEnabled = false,
        List<double> repDelayRange = null,
        double? defaultRepDelay = null,
        int? maxExperiments = null,
        Dictionary<string, object> additionalData = null)
    {
        BackendName = backendName;
        BackendVersion = backendVersion;
        NumberOfQubits = numberOfQubits;
        BasisGates = basisGates;
        Gates = gates;
        MaxShots = maxShots;
        CouplingMap = couplingMap;
        SupportedInstructions = supportedInstructions;
        DynamicRepRateEnabled = dynamicRepRateEnabled;
        RepDelayRange = repDelayRange;
        DefaultRepDelay = defaultRepDelay;
        MaxExperiments = maxExperiments;
        if (additionalData != null)
        {
            AdditionalData = additionalData;
        }
    }

    public static GateConfigAerBackendConfiguration FromDictionary(Dictionary<string, object> data)
    {
        var gates = new List<GateConfig>();
        foreach (var gateData in (List<Dictionary<string, object>>)data["gates"])
        {
            gates.Add(GateConfig.FromDictionary(gateData));
        }

        return new GateConfigAerBackendConfiguration(
            backendName: data["backend_name"].ToString(),
            backendVersion: data["backend_version"].ToString(),
            numberOfQubits: Convert.ToInt32(data["n_qubits"]),
            basisGates: new List<string>((List<string>)data["basis_gates"]),
            gates: gates,
            maxShots: Convert.ToInt32(data["max_shots"]),
            couplingMap: (List<List<int>>)data["coupling_map"],
            supportedInstructions: data.ContainsKey("supported_instructions")
                ? new List<string>((List<string>)data["supported_instructions"])
                : null,
            dynamicRepRateEnabled: data.ContainsKey("dynamic_reprate_enabled")
                ? Convert.ToBoolean(data["dynamic_reprate_enabled"])
                : false,
            repDelayRange: data.ContainsKey("rep_delay_range")
                ? new List<double>((List<double>)data["rep_delay_range"])
                : null,
            defaultRepDelay: data.ContainsKey("default_rep_delay")
                ? (double?)Convert.ToDouble(data["default_rep_delay"])
                : null,
            maxExperiments: data.ContainsKey("max_experiments")
                ? (int?)Convert.ToInt32(data["max_experiments"])
                : null,
            additionalData: data
        );
    }

    public Dictionary<string, object> ToDictionary()
    {
        var dict = new Dictionary<string, object>
        {
            { "backend_name", BackendName },
            { "backend_version", BackendVersion },
            { "n_qubits", NumberOfQubits },
            { "basis_gates", BasisGates },
            { "gates", Gates.ConvertAll(g => g.ToDictionary()) },
            { "max_shots", MaxShots },
            { "coupling_map", CouplingMap },
            { "dynamic_reprate_enabled", DynamicRepRateEnabled }
        };

        if (SupportedInstructions != null) dict["supported_instructions"] = SupportedInstructions;
        if (RepDelayRange != null) dict["rep_delay_range"] = RepDelayRange;
        if (DefaultRepDelay != null) dict["default_rep_delay"] = DefaultRepDelay;
        if (MaxExperiments != null) dict["max_experiments"] = MaxExperiments;

        foreach (var kv in AdditionalData)
        {
            dict[kv.Key] = kv.Value;
        }

        return dict;
    }

    public override bool Equals(object obj)
    {
        if (obj is GateConfigAerBackendConfiguration other)
        {
            return ToDictionary().Equals(other.ToDictionary());
        }
        return false;
    }
}
