using System;
using System.Collections.Generic;
using System.Linq;

public class AerProvider
{
    private static List<(string Name, Func<Dictionary<string, object>, AerSimulator> BackendConstructor, string Method, string Device)> _backends;
    public static int Version => 1;

    private static void PopulateBackends()
    {
        if (_backends == null)
        {
            _backends = new List<(string, Func<Dictionary<string, object>, AerSimulator>, string, string)>();

            var simulator = new AerSimulator();
            var methods = simulator.AvailableMethods();
            var devices = simulator.AvailableDevices();

            foreach (var method in methods)
            {
                foreach (var device in devices)
                {
                    string name = "aer_simulator";

                    if (string.IsNullOrEmpty(method) || method == "automatic")
                    {
                        _backends.Add((name, opts => new AerSimulator(opts), method, device));
                    }
                    else
                    {
                        name += $"_{method}";
                        if (method == "tensor_network" && device == "GPU")
                        {
                            name += $"_{device.ToLower()}";
                            _backends.Add((name, opts => new AerSimulator(opts), method, device));
                        }
                        else if (device == "CPU" || new[] { "statevector", "density_matrix", "unitary" }.Contains(method))
                        {
                            if (device != "CPU")
                            {
                                name += $"_{device.ToLower()}";
                            }
                            _backends.Add((name, opts => new AerSimulator(opts), method, device));
                        }
                    }
                }
            }

            // Adding legacy backends
            _backends.AddRange(new[]
            {
                ("qasm_simulator", opts => new QasmSimulator(opts), null, null),
                ("statevector_simulator", opts => new StatevectorSimulator(opts), null, null),
                ("unitary_simulator", opts => new UnitarySimulator(opts), null, null)
            });
        }
    }

    public AerSimulator GetBackend(string name = null, Dictionary<string, object> filters = null)
    {
        var backends = Backends(name, filters);
        if (backends.Count > 1)
        {
            throw new Exception("More than one backend matches the criteria.");
        }
        if (backends.Count == 0)
        {
            throw new Exception("No backend matches the criteria.");
        }

        return backends[0];
    }

    public List<AerSimulator> Backends(string name = null, Dictionary<string, object> filters = null)
    {
        PopulateBackends();

        var filteredBackends = new List<AerSimulator>();

        foreach (var (backendName, backendConstructor, method, device) in _backends)
        {
            if (name == null || backendName == name)
            {
                var options = new Dictionary<string, object>();
                if (method != null) options["method"] = method;
                if (device != null) options["device"] = device;

                var backend = backendConstructor(options);

                // Apply filters if provided
                if (filters == null || filters.All(filter => filter.Value.Equals(backend.GetOption(filter.Key))))
                {
                    filteredBackends.Add(backend);
                }
            }
        }

        return filteredBackends;
    }

    public override string ToString()
    {
        return "AerProvider";
    }
}