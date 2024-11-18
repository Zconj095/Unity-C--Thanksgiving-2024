using System;
using System.Collections.Generic;
using System.Linq;

public class DeviceProperties
{
    // Dictionary to store qubit error rates, key is the qubit index, value is the error rate
    public Dictionary<int, double> QubitErrorRates { get; set; }
    
    // Dictionary to store gate error rates, key is the gate name (e.g., "X", "CX"), value is the error rate
    public Dictionary<string, double> GateErrorRates { get; set; }
    
    // Dictionary to store qubit connectivity (which qubits are connected to which), key is qubit index
    public Dictionary<int, List<int>> QubitConnectivity { get; set; }
    
    // Optionally, other device-related properties like readout error rates, etc.
    public Dictionary<int, double> ReadoutErrorRates { get; set; }

    // Constructor
    public DeviceProperties()
    {
        QubitErrorRates = new Dictionary<int, double>();
        GateErrorRates = new Dictionary<string, double>();
        QubitConnectivity = new Dictionary<int, List<int>>();
        ReadoutErrorRates = new Dictionary<int, double>();
    }

    // Method to add a qubit error rate
    public void AddQubitErrorRate(int qubitIndex, double errorRate)
    {
        if (QubitErrorRates.ContainsKey(qubitIndex))
        {
            QubitErrorRates[qubitIndex] = errorRate;
        }
        else
        {
            QubitErrorRates.Add(qubitIndex, errorRate);
        }
    }

    // Method to add a gate error rate
    public void AddGateErrorRate(string gateName, double errorRate)
    {
        if (GateErrorRates.ContainsKey(gateName))
        {
            GateErrorRates[gateName] = errorRate;
        }
        else
        {
            GateErrorRates.Add(gateName, errorRate);
        }
    }

    // Method to add qubit connectivity
    public void AddQubitConnectivity(int qubitIndex, List<int> connectedQubits)
    {
        if (QubitConnectivity.ContainsKey(qubitIndex))
        {
            QubitConnectivity[qubitIndex] = connectedQubits;
        }
        else
        {
            QubitConnectivity.Add(qubitIndex, connectedQubits);
        }
    }

    // Method to add a readout error rate
    public void AddReadoutErrorRate(int qubitIndex, double errorRate)
    {
        if (ReadoutErrorRates.ContainsKey(qubitIndex))
        {
            ReadoutErrorRates[qubitIndex] = errorRate;
        }
        else
        {
            ReadoutErrorRates.Add(qubitIndex, errorRate);
        }
    }

    // Method to get all device properties as a string (for debugging purposes)
    public override string ToString()
    {
        string result = "Device Properties:\n";
        result += "Qubit Error Rates: " + string.Join(", ", QubitErrorRates.Select(kvp => $"Qubit {kvp.Key}: {kvp.Value}")) + "\n";
        result += "Gate Error Rates: " + string.Join(", ", GateErrorRates.Select(kvp => $"{kvp.Key}: {kvp.Value}")) + "\n";
        result += "Qubit Connectivity: " + string.Join(", ", QubitConnectivity.Select(kvp => $"Qubit {kvp.Key}: [{string.Join(", ", kvp.Value)}]")) + "\n";
        result += "Readout Error Rates: " + string.Join(", ", ReadoutErrorRates.Select(kvp => $"Qubit {kvp.Key}: {kvp.Value}")) + "\n";
        return result;
    }
}
