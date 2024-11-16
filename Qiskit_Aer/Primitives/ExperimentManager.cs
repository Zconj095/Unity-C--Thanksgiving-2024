using System.Collections.Generic;

public class ExperimentManager
{
    public List<int> Keys { get; private set; } = new List<int>();
    public List<QuantumCircuit> ExperimentCircuits { get; private set; } = new List<QuantumCircuit>();
    public List<Dictionary<Parameter, float>> ParameterBinds { get; private set; } = new List<Dictionary<Parameter, float>>();
    public List<List<int>> InputIndices { get; private set; } = new List<List<int>>();

    public void Append(int key, Dictionary<Parameter, float> parameterBind, QuantumCircuit experimentCircuit)
    {
        if (Keys.Contains(key))
        {
            int keyIndex = Keys.IndexOf(key);
            foreach (var bind in parameterBind)
                ParameterBinds[keyIndex][bind.Key] = bind.Value;

            InputIndices[keyIndex].Add(ExperimentCircuits.Count);
        }
        else
        {
            ExperimentCircuits.Add(experimentCircuit);
            Keys.Add(key);
            ParameterBinds.Add(parameterBind);
            InputIndices.Add(new List<int> { ExperimentCircuits.Count });
        }
    }
}

// Additional Classes: QuantumCircuit, AerSimulator, QuasiDistribution, etc., need to be implemented as per the use case.
