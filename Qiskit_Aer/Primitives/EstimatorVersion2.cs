using System;
using System.Collections.Generic;
using System.Linq;

public class EstimatorV2
{
    private Options _options;
    private AerSimulator _backend;

    public EstimatorV2(Dictionary<string, object> options = null)
    {
        _options = options != null ? ParseOptions(options) : new Options();
        _backend = new AerSimulator(_options.BackendOptions);
    }

    private Options ParseOptions(Dictionary<string, object> options)
    {
        var parsedOptions = new Options();
        if (options.ContainsKey("default_precision"))
            parsedOptions.DefaultPrecision = Convert.ToDouble(options["default_precision"]);
        if (options.ContainsKey("backend_options"))
            parsedOptions.BackendOptions = (Dictionary<string, object>)options["backend_options"];
        if (options.ContainsKey("run_options"))
            parsedOptions.RunOptions = (Dictionary<string, object>)options["run_options"];
        return parsedOptions;
    }

    public static EstimatorV2 FromBackend(AerSimulator backend, Dictionary<string, object> options = null)
    {
        var estimator = new EstimatorV2(options);
        estimator._backend = backend ?? new AerSimulator();
        return estimator;
    }

    public Options GetOptions()
    {
        return _options;
    }

    public PrimitiveJob Run(IEnumerable<EstimatorPubLike> pubs, double? precision = null)
    {
        double targetPrecision = precision ?? _options.DefaultPrecision;
        var coercedPubs = pubs.Select(pub => EstimatorPub.Coerce(pub, targetPrecision)).ToList();
        ValidatePubs(coercedPubs);
        var job = new PrimitiveJob(() => RunPubs(coercedPubs));
        job.Submit();
        return job;
    }

    private void ValidatePubs(List<EstimatorPub> pubs)
    {
        for (int i = 0; i < pubs.Count; i++)
        {
            if (pubs[i].Precision < 0.0)
            {
                throw new ArgumentException($"The {i}-th pub has precision less than 0 ({pubs[i].Precision}). Precision should be greater than or equal to 0.");
            }
        }
    }

    private PrimitiveResult RunPubs(List<EstimatorPub> pubs)
    {
        var results = pubs.Select(pub => RunPub(pub)).ToList();
        return new PrimitiveResult(results, new Dictionary<string, object> { { "version", 2 } });
    }

    private PubResult RunPub(EstimatorPub pub)
    {
        var circuit = pub.Circuit.Clone();
        var observables = pub.Observables;
        var parameterValues = pub.ParameterValues;
        var precision = pub.Precision;

        var paramShape = parameterValues.GetShape();
        var bcParamIndices = new int[paramShape[0], paramShape[1]]; // Simulating broadcasting

        var paulis = observables.SelectMany(obs => obs.Keys).ToHashSet();
        foreach (var pauli in paulis)
        {
            circuit.SaveExpectationValue(pauli, Enumerable.Range(0, circuit.NumQubits), pauli);
        }

        var result = _backend.Run(circuit, _options.RunOptions);
        var evs = new double[paramShape[0], paramShape[1]];
        var stds = new double[paramShape[0], paramShape[1]];

        foreach (var index in Enumerable.Range(0, paramShape[0] * paramShape[1]))
        {
            // Compute expectation values and apply noise
            // Add implementation for each coefficient and pauli
        }

        return new PubResult(new DataBin(evs, stds, evs.GetLength(0), evs.GetLength(1)), new Dictionary<string, object>
        {
            { "target_precision", precision },
            { "circuit_metadata", pub.Circuit.Metadata },
            { "simulator_metadata", result.Metadata }
        });
    }
}