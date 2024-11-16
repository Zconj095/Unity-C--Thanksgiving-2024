using System;
using System.Collections.Generic;
using System.Linq;

public class Sampler
{
    private AerSimulator _backend;
    private Dictionary<int, QuantumCircuit> _circuits = new Dictionary<int, QuantumCircuit>();
    private Dictionary<int, List<Parameter>> _parameters = new Dictionary<int, List<Parameter>>();
    private Dictionary<(int, bool), QuantumCircuit> _transpiledCircuits = new Dictionary<(int, bool), QuantumCircuit>();
    private Dictionary<string, int> _circuitIds = new Dictionary<string, int>();
    private Dictionary<string, object> _transpileOptions;
    private bool _skipTranspilation;

    public Sampler(Dictionary<string, object> backendOptions = null, Dictionary<string, object> transpileOptions = null, Dictionary<string, object> runOptions = null, bool skipTranspilation = false)
    {
        _backend = new AerSimulator(backendOptions ?? new Dictionary<string, object>());
        _transpileOptions = transpileOptions ?? new Dictionary<string, object>();
        _skipTranspilation = skipTranspilation;
    }

    public SamplerResult Call(List<int> circuits, List<List<float>> parameterValues, Dictionary<string, object> runOptions)
    {
        bool isShotsNone = runOptions.ContainsKey("shots") && runOptions["shots"] == null;
        Transpile(circuits, isShotsNone);

        var experimentManager = new ExperimentManager();

        for (int i = 0; i < circuits.Count; i++)
        {
            int circuitIndex = circuits[i];
            var parameters = parameterValues[i];
            if (parameters.Count != _parameters[circuitIndex].Count)
                throw new Exception($"Parameter count mismatch for circuit {circuitIndex}.");

            experimentManager.Append(
                circuitIndex,
                _parameters[circuitIndex].Zip(parameters, (p, v) => (p, v)).ToDictionary(pv => pv.p, pv => pv.v),
                _transpiledCircuits[(circuitIndex, isShotsNone)]
            );
        }

        var results = _backend.Run(
            experimentManager.ExperimentCircuits,
            experimentManager.ParameterBinds,
            runOptions
        );

        return ProcessResults(results, experimentManager, isShotsNone);
    }

    private void Transpile(List<int> circuitIndices, bool isShotsNone)
    {
        var toHandle = circuitIndices.Where(i => !_transpiledCircuits.ContainsKey((i, isShotsNone))).Distinct();
        foreach (var circuitIndex in toHandle)
        {
            var circuit = _circuits[circuitIndex];
            if (isShotsNone)
                circuit = PreprocessCircuit(circuit);

            if (!_skipTranspilation)
                circuit = TranspileCircuit(circuit);

            _transpiledCircuits[(circuitIndex, isShotsNone)] = circuit;
        }
    }

    private QuantumCircuit PreprocessCircuit(QuantumCircuit circuit)
    {
        circuit = InitCircuit(circuit);
        var qcMapping = FinalMeasurementMapping(circuit);

        if (qcMapping.Values.Distinct().Count() != circuit.NumClBits)
            throw new Exception("Mismatch in classical bit mappings.");

        var qargs = qcMapping.OrderBy(x => x.Value).Select(x => x.Key).ToList();
        circuit.RemoveFinalMeasurements();
        circuit.SaveProbabilitiesDict(qargs);

        return circuit;
    }

    private QuantumCircuit TranspileCircuit(QuantumCircuit circuit)
    {
        _backend.SetMaxQubits(circuit.NumQubits);
        return _backend.Transpile(circuit, _transpileOptions);
    }

    private SamplerResult ProcessResults(List<Result> results, ExperimentManager experimentManager, bool isShotsNone)
    {
        var quasis = new List<QuasiDistribution>();
        var metadata = new List<Dictionary<string, object>>();

        foreach (var i in experimentManager.ExperimentIndices)
        {
            if (isShotsNone)
            {
                var probabilities = results[i].GetProbabilities();
                var numQubits = results[i].Metadata["num_qubits"];
                var quasi = new QuasiDistribution(
                    probabilities.ToDictionary(p => Convert.ToString(p.Key, 2).PadLeft(numQubits, '0'), p => p.Value)
                );
                quasis.Add(quasi);
                metadata.Add(new Dictionary<string, object> { { "shots", null }, { "simulator_metadata", results[i].Metadata } });
            }
            else
            {
                var counts = results[i].GetCounts();
                int shots = counts.Values.Sum();
                var quasi = new QuasiDistribution(
                    counts.ToDictionary(c => c.Key.Replace(" ", ""), c => (float)c.Value / shots),
                    shots
                );
                quasis.Add(quasi);
                metadata.Add(new Dictionary<string, object> { { "shots", shots }, { "simulator_metadata", results[i].Metadata } });
            }
        }

        return new SamplerResult(quasis, metadata);
    }
}
