using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SamplerV2
{
    private int _defaultShots;
    private int? _seed;
    private AerSimulator _backend;
    private Options _options;

    public SamplerV2(int defaultShots = 1024, int? seed = null, Dictionary<string, object> options = null)
    {
        _defaultShots = defaultShots;
        _seed = seed;
        _options = options != null ? new Options(options) : new Options();
        _backend = new AerSimulator(_options.BackendOptions);
    }

    public static SamplerV2 FromBackend(AerSimulator backend, Dictionary<string, object> options = null)
    {
        var sampler = new SamplerV2(options: options);
        sampler._backend = backend is AerSimulator ? backend : AerSimulator.FromBackend(backend);
        return sampler;
    }

    public int DefaultShots => _defaultShots;
    public int? Seed => _seed;
    public Options Options => _options;

    public PrimitiveJob<PrimitiveResult<SamplerPubResult>> Run(IEnumerable<SamplerPub> pubs, int? shots = null)
    {
        shots ??= _defaultShots;
        var coercedPubs = pubs.Select(pub => SamplerPub.Coerce(pub, shots.Value)).ToList();
        ValidatePubs(coercedPubs);

        var job = new PrimitiveJob<PrimitiveResult<SamplerPubResult>>(() => RunPubs(coercedPubs));
        job.Submit();
        return job;
    }

    private void ValidatePubs(List<SamplerPub> pubs)
    {
        for (int i = 0; i < pubs.Count; i++)
        {
            var pub = pubs[i];
            if (pub.Circuit.Cregs.Count == 0)
            {
                Debug.LogWarning($"The {i}-th pub's circuit has no output classical registers, so the result will be empty. Did you mean to add measurement instructions?");
            }
        }
    }

    private PrimitiveResult<SamplerPubResult> RunPubs(List<SamplerPub> pubs)
    {
        var pubDict = pubs.GroupBy(pub => pub.Shots).ToDictionary(g => g.Key, g => g.ToList());
        var results = new SamplerPubResult[pubs.Count];

        foreach (var kvp in pubDict)
        {
            var shots = kvp.Key;
            var pubsWithSameShots = kvp.Value;
            var pubResults = ExecutePubs(pubsWithSameShots, shots);

            for (int i = 0; i < pubsWithSameShots.Count; i++)
            {
                var pubIndex = pubs.IndexOf(pubsWithSameShots[i]);
                results[pubIndex] = pubResults[i];
            }
        }

        return new PrimitiveResult<SamplerPubResult>(results, new Dictionary<string, object> { { "version", 2 } });
    }

    private List<SamplerPubResult> ExecutePubs(List<SamplerPub> pubs, int shots)
    {
        var circuits = pubs.Select(pub => pub.Circuit).ToList();
        var parameterBinds = pubs.Select(ConvertParameterBindings).ToList();

        var runOptions = new Dictionary<string, object>(_options.RunOptions);
        runOptions.Remove("shots");
        runOptions.Remove("parameter_binds");
        runOptions.Remove("memory");

        if (_seed.HasValue && runOptions.ContainsKey("seed_simulator"))
        {
            runOptions.Remove("seed_simulator");
        }

        var result = _backend.Run(circuits, shots, _seed, parameterBinds, true, runOptions);
        var resultMemory = PrepareMemory(result);

        var results = new List<SamplerPubResult>();
        int start = 0;

        foreach (var pub in pubs)
        {
            var (measInfo, maxNumBytes) = AnalyzeCircuit(pub.Circuit);
            var parameterValues = pub.ParameterValues;
            int end = start + parameterValues.Size;

            results.Add(PostprocessPub(
                resultMemory.GetRange(start, end - start),
                shots,
                parameterValues.Shape,
                measInfo,
                maxNumBytes,
                pub.Circuit.Metadata,
                result.Metadata
            ));
            start = end;
        }

        return results;
    }

    private SamplerPubResult PostprocessPub(
        List<List<string>> resultMemory,
        int shots,
        int[] shape,
        List<MeasureInfo> measInfo,
        int maxNumBytes,
        Dictionary<string, object> circuitMetadata,
        Dictionary<string, object> simulatorMetadata)
    {
        var arrays = measInfo.ToDictionary(
            item => item.CregName,
            item => new uint[shape[0], shots, item.NumBytes]
        );

        var memoryArray = ConvertToMemoryArray(resultMemory, maxNumBytes);

        for (int i = 0; i < memoryArray.Length; i++)
        {
            var sample = memoryArray[i];
            foreach (var item in measInfo)
            {
                var ary = ConvertSamplesToPackedArray(sample, item.NumBits, item.Start);
                arrays[item.CregName][i] = ary;
            }
        }

        var measurements = measInfo.ToDictionary(
            item => item.CregName,
            item => new BitArray(arrays[item.CregName], item.NumBits)
        );

        return new SamplerPubResult(
            new DataBin(measurements, shape),
            new Dictionary<string, object>
            {
                { "shots", shots },
                { "circuit_metadata", circuitMetadata },
                { "simulator_metadata", simulatorMetadata }
            }
        );
    }

    private Dictionary<string, float[]> ConvertParameterBindings(SamplerPub pub)
    {
        var circuit = pub.Circuit;
        var parameterValues = pub.ParameterValues;
        return circuit.Parameters.ToDictionary(
            param => param.Name,
            param => parameterValues.AsArray(param)
        );
    }

    private List<List<string>> PrepareMemory(Result result)
    {
        return result.Results
            .Select(exp => exp.Data.ContainsKey("memory") ? exp.Data["memory"] : Enumerable.Repeat("0x0", exp.Shots).ToList())
            .ToList();
    }

    private List<uint[]> ConvertToMemoryArray(List<List<string>> memory, int maxNumBytes)
    {
        // Implement memory array conversion logic here.
        throw new NotImplementedException();
    }

    private uint[] ConvertSamplesToPackedArray(string sample, int numBits, int start)
    {
        // Implement packed array conversion logic here.
        throw new NotImplementedException();
    }

    private (List<MeasureInfo>, int) AnalyzeCircuit(QuantumCircuit circuit)
    {
        // Analyze circuit and return measure info and max bytes.
        throw new NotImplementedException();
    }
}