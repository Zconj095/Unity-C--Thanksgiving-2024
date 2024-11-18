using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

/// <summary>
/// Aer implementation of Estimator.
///
/// Run Options:
/// - shots (null or int):
///     The number of shots. If null and approximation is true, it calculates the exact expectation values.
///     Otherwise, it calculates expectation values with sampling.
/// - seed (int):
///     Set a fixed seed for the sampling.
///
/// Note:
/// Precedence of seeding for 'seed_simulator' is as follows:
/// 1. 'seed_simulator' in runtime (i.e. in run method)
/// 2. 'seed' in runtime (i.e. in run method)
/// 3. 'seed_simulator' of 'backend_options'.
/// 4. default.
///
/// 'seed' is also used for sampling from a normal distribution when approximation is true.
///
/// When combined with the approximation option, we get the expectation values as follows:
/// - shots is null and approximation=false: Return an expectation value with sampling-noise with warning.
/// - shots is int and approximation=false: Return an expectation value with sampling-noise.
/// - shots is null and approximation=true: Return an exact expectation value.
/// - shots is int and approximation=true: Return expectation value with sampling-noise using a normal distribution approximation.
/// </summary>
public class Estimator : BaseEstimator
{
    private List<QuantumCircuit> _circuits;
    private List<List<ParameterExpression>> _parameters;
    private List<BaseOperator> _observables;

    private AerSimulator _backend;
    private Options _transpile_options;
    private bool _approximation;
    private bool _skip_transpilation;
    private Dictionary<Tuple<List<int>, List<int>, bool>, Tuple<Dictionary<int, List<QuantumCircuit>>, Dictionary<int, Dictionary<int, List<int>>>>> _cache;
    private Dictionary<int, QuantumCircuit> _transpiled_circuits;
    private Dictionary<int, List<int>> _layouts;
    private Dictionary<object, int> _circuit_ids;
    private Dictionary<object, int> _observable_ids;
    private bool _abelian_grouping;

    public Estimator(
        Dictionary<string, object> backend_options = null,
        Dictionary<string, object> transpile_options = null,
        Dictionary<string, object> run_options = null,
        bool approximation = false,
        bool skip_transpilation = false,
        bool abelian_grouping = true
    ) : base(run_options)
    {
        Console.WriteLine("Estimator has been deprecated as of Aer 0.15, please use EstimatorV2 instead.");

        _circuits = new List<QuantumCircuit>();
        _parameters = new List<List<ParameterExpression>>();
        _observables = new List<BaseOperator>();

        backend_options = backend_options ?? new Dictionary<string, object>();
        string method = (approximation && backend_options.ContainsKey("noise_model")) ? "density_matrix" : "automatic";
        _backend = new AerSimulator(method);
        _backend.SetOptions(backend_options);

        _transpile_options = new Options();
        if (transpile_options != null)
        {
            _transpile_options.UpdateOptions(transpile_options);
        }

        if (!approximation)
        {
            Console.WriteLine("Option approximation=False is deprecated as of qiskit-aer 0.13. It will be removed no earlier than 3 months after the release date. Instead, use BackendEstimator from qiskit.primitives.");
        }

        _approximation = approximation;
        _skip_transpilation = skip_transpilation;
        _cache = new Dictionary<Tuple<List<int>, List<int>, bool>, Tuple<Dictionary<int, List<QuantumCircuit>>, Dictionary<int, Dictionary<int, List<int>>>>>();
        _transpiled_circuits = new Dictionary<int, QuantumCircuit>();
        _layouts = new Dictionary<int, List<int>>();
        _circuit_ids = new Dictionary<object, int>();
        _observable_ids = new Dictionary<object, int>();
        _abelian_grouping = abelian_grouping;
    }

    public bool Approximation
    {
        get { return _approximation; }
        set
        {
            if (!value)
            {
                Console.WriteLine("Option approximation=False is deprecated as of qiskit-aer 0.13. It will be removed no earlier than 3 months after the release date. Instead, use BackendEstimator from qiskit.primitives.");
            }
            _approximation = value;
        }
    }

    private EstimatorResult _call(
        List<int> circuits,
        List<int> observables,
        List<List<double>> parameter_values,
        Dictionary<string, object> run_options
    )
    {
        object seedObj;
        run_options.TryGetValue("seed", out seedObj);
        int? seed = seedObj as int?;
        if (seed.HasValue)
        {
            if (!run_options.ContainsKey("seed_simulator"))
            {
                run_options["seed_simulator"] = seed.Value;
            }
        }

        if (_approximation)
        {
            return _compute_with_approximation(circuits, observables, parameter_values, run_options, seed);
        }
        else
        {
            return _compute(circuits, observables, parameter_values, run_options);
        }
    }

    public PrimitiveJob Run(
        List<QuantumCircuit> circuits,
        List<BaseOperator> observables,
        List<List<double>> parameter_values,
        Dictionary<string, object> run_options = null
    )
    {
        List<int> circuit_indices = new List<int>();
        foreach (var circuit in circuits)
        {
            object key = CircuitKey(circuit);
            int index;
            if (_circuit_ids.TryGetValue(key, out index))
            {
                circuit_indices.Add(index);
            }
            else
            {
                index = _circuits.Count;
                circuit_indices.Add(index);
                _circuit_ids[key] = index;
                _circuits.Add(circuit);
                _parameters.Add(circuit.Parameters);
            }
        }

        List<int> observable_indices = new List<int>();
        foreach (var observable in observables)
        {
            BaseOperator obs = InitObservable(observable);
            object key = ObservableKey(obs);
            int index;
            if (_observable_ids.TryGetValue(key, out index))
            {
                observable_indices.Add(index);
            }
            else
            {
                index = _observables.Count;
                observable_indices.Add(index);
                _observable_ids[key] = index;
                _observables.Add(obs);
            }
        }

        PrimitiveJob job = new PrimitiveJob(() =>
        {
            return _call(circuit_indices, observable_indices, parameter_values, run_options);
        });

        job.Submit();
        return job;
    }

    private EstimatorResult _compute(List<int> circuits, List<int> observables, List<List<double>> parameter_values, Dictionary<string, object> run_options)
    {
        if (run_options.ContainsKey("shots") && run_options["shots"] == null)
        {
            Console.WriteLine($"If `shots` is None and `approximation` is False, the number of shots is automatically set to backend options' shots={_backend.Options["shots"]}.");
        }

        var key = Tuple.Create(circuits, observables, _approximation);

        Dictionary<int, List<QuantumCircuit>> experiments_dict;
        Dictionary<int, Dictionary<int, List<int>>> obs_maps;

        if (_cache.TryGetValue(key, out var cacheEntry))
        {
            experiments_dict = cacheEntry.Item1;
            obs_maps = cacheEntry.Item2;

            var exp_map = _pre_process_params(circuits, observables, parameter_values, obs_maps);
            var flattenResult = _flatten(experiments_dict, exp_map);
            var experiments = flattenResult.Item1;
            var parameter_binds = flattenResult.Item2;

            var post_processings = _create_post_processing(circuits, observables, parameter_values, obs_maps, exp_map);

            // Run experiments
            var results = _backend.Run(experiments, parameter_binds.Any() ? parameter_binds : null, run_options).Result().Results;

            // Post processing
            var expectation_values = new List<double>();
            var metadata = new List<Dictionary<string, object>>();

            foreach (var post_processing in post_processings)
            {
                var result = post_processing.Run(results);
                expectation_values.Add(result.Item1);
                metadata.Add(result.Item2);
            }

            return new EstimatorResult(expectation_values.ToArray(), metadata);
        }
        else
        {
            _transpile_circuits(circuits);
            var circ_obs_map = new Dictionary<int, List<int>>();

            for (int idx = 0; idx < circuits.Count; idx++)
            {
                int circ_ind = circuits[idx];
                int obs_ind = observables[idx];
                if (!circ_obs_map.ContainsKey(circ_ind))
                {
                    circ_obs_map[circ_ind] = new List<int>();
                }
                circ_obs_map[circ_ind].Add(obs_ind);
            }

            experiments_dict = new Dictionary<int, List<QuantumCircuit>>();
            obs_maps = new Dictionary<int, Dictionary<int, List<int>>>();

            foreach (var kvp in circ_obs_map)
            {
                int circ_ind = kvp.Key;
                List<int> obs_indices = kvp.Value;

                PauliList pauli_list = new PauliList();
                foreach (int obs_ind in obs_indices)
                {
                    pauli_list = pauli_list.Add(_observables[obs_ind].Paulis);
                }
                pauli_list = pauli_list.Unique();

                List<PauliList> pauli_lists;
                if (_abelian_grouping)
                {
                    pauli_lists = pauli_list.GroupCommuting(true);
                }
                else
                {
                    pauli_lists = pauli_list.Paulis.Select(p => new PauliList(p)).ToList();
                }

                var obs_map = new Dictionary<int, List<int>>();

                foreach (int obs_ind in obs_indices)
                {
                    foreach (var pauli in _observables[obs_ind].Paulis.Paulis)
                    {
                        for (int basis_ind = 0; basis_ind < pauli_lists.Count; basis_ind++)
                        {
                            if (pauli_lists[basis_ind].Contains(pauli))
                            {
                                if (!obs_map.ContainsKey(obs_ind))
                                {
                                    obs_map[obs_ind] = new List<int>();
                                }
                                obs_map[obs_ind].Add(basis_ind);
                                break;
                            }
                        }
                    }
                }

                obs_maps[circ_ind] = obs_map;
                var bases = pauli_lists.Select(pl => _paulis2basis(pl)).ToList();

                if (bases.Count == 1 && !bases[0].X.Any() && !bases[0].Z.Any())
                {
                    break;
                }

                var meas_circuits = bases.Select(b => _create_meas_circuit(b, circ_ind)).ToList();

                var circuit = _skip_transpilation ? _circuits[circ_ind] : _transpiled_circuits[circ_ind];
                experiments_dict[circ_ind] = _combine_circs(circuit, meas_circuits);
            }

            _cache[key] = Tuple.Create(experiments_dict, obs_maps);

            var exp_map = _pre_process_params(circuits, observables, parameter_values, obs_maps);
            var flattenResult = _flatten(experiments_dict, exp_map);
            var experiments = flattenResult.Item1;
            var parameter_binds = flattenResult.Item2;

            var post_processings = _create_post_processing(circuits, observables, parameter_values, obs_maps, exp_map);

            // Run experiments
            var results = _backend.Run(experiments, parameter_binds.Any() ? parameter_binds : null, run_options).Result().Results;

            // Post processing
            var expectation_values = new List<double>();
            var metadata = new List<Dictionary<string, object>>();

            foreach (var post_processing in post_processings)
            {
                var result = post_processing.Run(results);
                expectation_values.Add(result.Item1);
                metadata.Add(result.Item2);
            }

            return new EstimatorResult(expectation_values.ToArray(), metadata);
        }
    }

    private Dictionary<int, Dictionary<int, Tuple<List<ParameterExpression>, List<List<double>>>>> _pre_process_params(
        List<int> circuits,
        List<int> observables,
        List<List<double>> parameter_values,
        Dictionary<int, Dictionary<int, List<int>>> obs_maps
    )
    {
        var exp_map = new Dictionary<int, Dictionary<int, Tuple<List<ParameterExpression>, List<List<double>>>>>();

        for (int idx = 0; idx < circuits.Count; idx++)
        {
            int circ_ind = circuits[idx];
            int obs_ind = observables[idx];
            List<double> param_val = parameter_values[idx];

            _validate_parameter_length(param_val, circ_ind);
            var parameter = _parameters[circ_ind];

            foreach (int basis_ind in obs_maps[circ_ind][obs_ind])
            {
                if (exp_map.ContainsKey(circ_ind) && exp_map[circ_ind].ContainsKey(basis_ind) && parameter.Any())
                {
                    var param_vals = exp_map[circ_ind][basis_ind].Item2;
                    if (!param_vals.Contains(param_val))
                    {
                        param_vals.Add(param_val);
                    }
                }
                else
                {
                    if (!exp_map.ContainsKey(circ_ind))
                    {
                        exp_map[circ_ind] = new Dictionary<int, Tuple<List<ParameterExpression>, List<List<double>>>>();
                    }
                    exp_map[circ_ind][basis_ind] = Tuple.Create(parameter, new List<List<double>> { param_val });
                }
            }
        }

        return exp_map;
    }

    private Tuple<List<QuantumCircuit>, List<Dictionary<ParameterExpression, List<double>>>> _flatten(
        Dictionary<int, List<QuantumCircuit>> experiments_dict,
        Dictionary<int, Dictionary<int, Tuple<List<ParameterExpression>, List<List<double>>>>> exp_map
    )
    {
        var experiments_list = new List<QuantumCircuit>();
        var parameter_binds = new List<Dictionary<ParameterExpression, List<double>>>();

        foreach (var circ_ind in experiments_dict.Keys)
        {
            experiments_list.AddRange(experiments_dict[circ_ind]);
            foreach (var basis_map in exp_map[circ_ind].Values)
            {
                var parameter = basis_map.Item1;
                var param_vals = basis_map.Item2;

                var binds = new Dictionary<ParameterExpression, List<double>>();
                for (int i = 0; i < parameter.Count; i++)
                {
                    var param = parameter[i];
                    binds[param] = param_vals.Select(pv => pv[i]).ToList();
                }

                parameter_binds.Add(binds);
            }
        }

        return Tuple.Create(experiments_list, parameter_binds);
    }

    private QuantumCircuit _create_meas_circuit(Pauli basis, int circuit_index)
    {
        var qargs = Enumerable.Range(0, basis.NumQubits).Where(i => basis.Z[i] || basis.X[i]).ToList();
        var meas_circuit = new QuantumCircuit(basis.NumQubits, qargs.Count);

        for (int clbit = 0; clbit < qargs.Count; clbit++)
        {
            int qarg = qargs[clbit];
            if (basis.X[qarg])
            {
                if (basis.Z[qarg])
                {
                    meas_circuit.Sdg(qarg);
                }
                meas_circuit.H(qarg);
            }
            meas_circuit.Measure(qarg, clbit);
        }

        meas_circuit.Metadata = new Dictionary<string, object> { { "basis", basis } };

        if (_skip_transpilation)
        {
            return meas_circuit;
        }

        var layout = _layouts[circuit_index];
        var passmanager = new PassManager(new List<TransformationPass> { new SetLayout(layout) });
        var opt1q = new Optimize1qGatesDecomposition(_backend.Target);
        passmanager.Append(opt1q);
        if (_backend.CouplingMap != null)
        {
            var coupling_map = _backend.CouplingMap;
            passmanager.Append(new FullAncillaAllocation(coupling_map));
            passmanager.Append(new EnlargeWithAncilla());
        }
        passmanager.Append(new ApplyLayout());

        return passmanager.Run(meas_circuit);
    }

    private List<QuantumCircuit> _combine_circs(QuantumCircuit circuit, List<QuantumCircuit> meas_circuits)
    {
        var circs = new List<QuantumCircuit>();
        foreach (var meas_circuit in meas_circuits)
        {
            var new_circ = circuit.Copy();
            foreach (var creg in meas_circuit.Cregs)
            {
                new_circ.AddRegister(creg);
            }
            new_circ.Compose(meas_circuit, inplace: true);
            _update_metadata(new_circ, meas_circuit.Metadata);
            circs.Add(new_circ);
        }
        return circs;
    }

    private int _calculate_result_index(int circ_ind, int obs_ind, int term_ind, List<double> param_val, Dictionary<int, Dictionary<int, List<int>>> obs_maps, Dictionary<int, Dictionary<int, Tuple<List<ParameterExpression>, List<List<double>>>>> exp_map)
    {
        int basis_ind = obs_maps[circ_ind][obs_ind][term_ind];
        int result_index = 0;

        foreach (var _circ_ind in exp_map.Keys)
        {
            foreach (var _basis_ind in exp_map[_circ_ind].Keys)
            {
                var param_vals = exp_map[_circ_ind][_basis_ind].Item2;
                if (circ_ind == _circ_ind && basis_ind == _basis_ind)
                {
                    result_index += param_vals.IndexOf(param_val);
                    return result_index;
                }
                result_index += param_vals.Count;
            }
        }

        throw new AerError("Bug. Please report from issue: https://github.com/Qiskit/qiskit-aer/issues");
    }

    private List<_PostProcessing> _create_post_processing(
        List<int> circuits,
        List<int> observables,
        List<List<double>> parameter_values,
        Dictionary<int, Dictionary<int, List<int>>> obs_maps,
        Dictionary<int, Dictionary<int, Tuple<List<ParameterExpression>, List<List<double>>>>> exp_map
    )
    {
        var post_processings = new List<_PostProcessing>();

        for (int idx = 0; idx < circuits.Count; idx++)
        {
            int circ_ind = circuits[idx];
            int obs_ind = observables[idx];
            var param_val = parameter_values[idx];

            var result_indices = new List<int?>();
            var paulis = new List<PauliList>();
            var coeffs = new List<List<double>>();

            var observable = _observables[obs_ind];

            for (int term_ind = 0; term_ind < observable.Paulis.Count; term_ind++)
            {
                var pauli = observable.Paulis[term_ind];
                var coeff = observable.Coeffs[term_ind];

                if (!pauli.X.Any(b => b) && !pauli.Z.Any(b => b))
                {
                    result_indices.Add(null);
                    paulis.Add(new PauliList(pauli));
                    coeffs.Add(new List<double> { coeff });
                    continue;
                }

                int result_index = _calculate_result_index(circ_ind, obs_ind, term_ind, param_val, obs_maps, exp_map);
                if (result_indices.Contains(result_index))
                {
                    int i = result_indices.IndexOf(result_index);
                    paulis[i].Add(pauli);
                    coeffs[i].Add(coeff);
                }
                else
                {
                    result_indices.Add(result_index);
                    paulis.Add(new PauliList(pauli));
                    coeffs.Add(new List<double> { coeff });
                }
            }

            post_processings.Add(new _PostProcessing(result_indices, paulis, coeffs));
        }

        return post_processings;
    }

    private EstimatorResult _compute_with_approximation(
        List<int> circuits,
        List<int> observables,
        List<List<double>> parameter_values,
        Dictionary<string, object> run_options,
        int? seed
    )
    {
        var key = Tuple.Create(circuits, observables, _approximation);
        object shotsObj;
        run_options.TryGetValue("shots", out shotsObj);
        int? shots = shotsObj as int?;

        if (_cache.TryGetValue(key, out var cacheEntry))
        {
            var experiment_manager = cacheEntry as _ExperimentManager;
            var parameter_binds = new Dictionary<Tuple<int, int>, Dictionary<ParameterExpression, List<double>>>();

            for (int idx = 0; idx < circuits.Count; idx++)
            {
                int i = circuits[idx];
                int j = observables[idx];
                var value = parameter_values[idx];

                _validate_parameter_length(value, i);
                var parameters = _parameters[i];

                var keyPair = Tuple.Create(i, j);

                if (!parameter_binds.ContainsKey(keyPair))
                {
                    parameter_binds[keyPair] = new Dictionary<ParameterExpression, List<double>>();
                }

                for (int k = 0; k < parameters.Count; k++)
                {
                    var param = parameters[k];
                    if (!parameter_binds[keyPair].ContainsKey(param))
                    {
                        parameter_binds[keyPair][param] = new List<double>();
                    }
                    parameter_binds[keyPair][param].Add(value[k]);
                }
            }

            experiment_manager.ParameterBinds = parameter_binds.Values.ToList();
        }
        else
        {
            _transpile_circuits(circuits);
            var experiment_manager = new _ExperimentManager();

            for (int idx = 0; idx < circuits.Count; idx++)
            {
                int i = circuits[idx];
                int j = observables[idx];
                var value = parameter_values[idx];

                _validate_parameter_length(value, i);

                var keyPair = Tuple.Create(i, j);

                if (experiment_manager.Keys.Contains(keyPair))
                {
                    // Do nothing, already added
                }
                else
                {
                    var circuit = _skip_transpilation ? _circuits[i].Copy() : _transpiled_circuits[i].Copy();
                    var observable = _observables[j];

                    if (shots == null)
                    {
                        circuit.SaveExpectationValue(observable, _layouts[i]);
                    }
                    else
                    {
                        for (int term_ind = 0; term_ind < observable.Paulis.Count; term_ind++)
                        {
                            var pauli = observable.Paulis[term_ind];
                            circuit.SaveExpectationValue(pauli, _layouts[i], label: term_ind.ToString());
                        }
                    }

                    experiment_manager.Append(keyPair, _parameters[i].Zip(value, (k, v) => new KeyValuePair<ParameterExpression, double>(k, v)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value), circuit);
                }
            }

            _cache[key] = experiment_manager;
        }

        var result = _backend.Run(experiment_manager.ExperimentCircuits, experiment_manager.ParameterBinds, run_options).Result();

        var expectation_values = new List<double>();
        var metadata = new List<Dictionary<string, object>>();

        if (shots == null)
        {
            foreach (int idx in experiment_manager.ExperimentIndices)
            {
                var expval = result.Data(idx)["expectation_value"];
                expectation_values.Add(Convert.ToDouble(expval));
                metadata.Add(new Dictionary<string, object> { { "simulator_metadata", result.Results[idx].Metadata } });
            }
        }
        else
        {
            var rng = new Random(seed.HasValue ? seed.Value : Environment.TickCount);
            foreach (int idx in experiment_manager.ExperimentIndices)
            {
                double combined_expval = 0.0;
                double combined_var = 0.0;
                int result_index = idx;
                int observable_key = experiment_manager.GetObservableKey(idx);
                var coeffs = _observables[observable_key].Coeffs.Select(c => Convert.ToDouble(c)).ToList();

                foreach (var kvp in result.Data(result_index))
                {
                    int term_ind = int.Parse(kvp.Key);
                    double expval = Convert.ToDouble(kvp.Value);
                    double var = 1 - expval * expval;
                    double coeff = coeffs[term_ind];
                    combined_expval += expval * coeff;
                    combined_var += var * coeff * coeff;
                }

                double standard_error = Math.Sqrt(combined_var / shots.Value);
                expectation_values.Add(NormalSample(rng, combined_expval, standard_error));
                metadata.Add(new Dictionary<string, object>
                {
                    { "variance", combined_var },
                    { "shots", shots.Value },
                    { "simulator_metadata", result.Results[result_index].Metadata }
                });
            }
        }

        return new EstimatorResult(expectation_values.ToArray(), metadata);
    }

    private void _validate_parameter_length(List<double> parameter, int circuit_index)
    {
        if (parameter.Count != _parameters[circuit_index].Count)
        {
            throw new ArgumentException($"The number of values ({parameter.Count}) does not match the number of parameters ({_parameters[circuit_index].Count}).");
        }
    }

    private void _transpile_circuits(List<int> circuits)
    {
        if (_skip_transpilation)
        {
            foreach (int i in circuits.Distinct())
            {
                int num_qubits = _circuits[i].NumQubits;
                _layouts[i] = Enumerable.Range(0, num_qubits).ToList();
            }
            return;
        }

        foreach (int i in circuits.Distinct())
        {
            if (!_transpiled_circuits.ContainsKey(i))
            {
                var circuit = _circuits[i].Copy();
                circuit.MeasureAll();
                int num_qubits = circuit.NumQubits;
                _backend.SetMaxQubits(num_qubits);
                circuit = _transpile(circuit);
                var bit_map = circuit.Qubits.Select((q, idx) => new { q, idx }).ToDictionary(a => a.q, a => a.idx);
                var layout = circuit.Data.Skip(circuit.Data.Count - num_qubits).Select(op => bit_map[op.Qubits[0]]).ToList();
                circuit.RemoveFinalMeasurements();
                _transpiled_circuits[i] = circuit;
                _layouts[i] = layout;
            }
        }
    }

    private QuantumCircuit _transpile(QuantumCircuit circuit)
    {
        if (_skip_transpilation)
        {
            return circuit;
        }

        return Transpiler.Transpile(circuit, _backend, _transpile_options.GetOptions());
    }

    // Helper methods and classes would continue here...
    // Including definitions for BaseEstimator, QuantumCircuit, BaseOperator, Pauli, PauliList, AerSimulator, PrimitiveJob, Options, etc.
    // As well as any methods or functions like CircuitKey, ObservableKey, InitObservable, NormalSample, etc.
}
