using System;
using System.Collections.Generic;
using System.Linq;

public class BackendAerProvider
{
    // Static variable to store backends
    private static List<AerProviderBackend> _BACKENDS = null;

    // Version of the provider
    public static int Version { get; } = 1;

    // Static method to get backends
    private static List<AerProviderBackend> GetBackends()
    {
        if (_BACKENDS == null)
        {
            // Populate the list of Aer simulator backends
            var simulator = new AerProviderSimulator();
            var methods = simulator.AvailableMethods();
            var devices = simulator.AvailableDevices();
            var backends = new List<AerProviderBackend>();

            foreach (var method in methods)
            {
                foreach (var device in devices)
                {
                    string name = "aer_simulator";

                    if (string.IsNullOrEmpty(method) || method == "automatic")
                    {
                        backends.Add(new AerProviderBackend(name, method, device));
                    }
                    else
                    {
                        name += $"_{method}";
                        if (method == "tensor_network" && device == "GPU")
                        {
                            name += $"_{device.ToLower()}";
                            backends.Add(new AerProviderBackend(name, method, device));
                        }
                        else
                        {
                            if (device == "CPU")
                            {
                                backends.Add(new AerProviderBackend(name, method, device));
                            }
                            else if (new[] { "statevector", "density_matrix", "unitary" }.Contains(method))
                            {
                                name += $"_{device.ToLower()}";
                                backends.Add(new AerProviderBackend(name, method, device));
                            }
                        }
                    }
                }
            }

            // Add legacy backends
            backends.AddRange(new[]
            {
                new AerProviderBackend("qasm_simulator"),
                new AerProviderBackend("statevector_simulator"),
                new AerProviderBackend("unitary_simulator")
            });

            _BACKENDS = backends;
        }

        return _BACKENDS;
    }

    // Method to get a single backend
    public AerProviderBackend GetBackend(string name = null, Dictionary<string, object> filters = null)
    {
        var backends = Backends(name, filters);
        if (backends.Count > 1)
        {
            throw new Exception("More than one backend matches the criteria");
        }
        if (backends.Count == 0)
        {
            throw new Exception("No backend matches the criteria");
        }

        return backends.First();
    }

    // Method to get all backends matching criteria
    public List<AerProviderBackend> Backends(string name = null, Dictionary<string, object> filters = null)
    {
        var backends = new List<AerProviderBackend>();
        foreach (var backend in GetBackends())
        {
            if (name == null || backend.Name == name)
            {
                backends.Add(backend);
            }
        }

        // Apply filters if provided
        if (filters != null)
        {
            backends = FilterBackends(backends, filters);
        }

        return backends;
    }

    // Filter backends using given filters
    private List<AerProviderBackend> FilterBackends(List<AerProviderBackend> backends, Dictionary<string, object> filters)
    {
        // Apply filters (implementation can vary based on requirements)
        return backends.Where(b => filters.All(f => b.MatchesFilter(f.Key, f.Value))).ToList();
    }

    // Override ToString
    public override string ToString()
    {
        return "BackendAerProvider";
    }
}

// Supporting Classes
public class AerProviderBackend
{
    public string Name { get; }
    public string Method { get; }
    public string Device { get; }

    public AerProviderBackend(string name, string method = null, string device = null)
    {
        Name = name;
        Method = method;
        Device = device;
    }

    // Check if the backend matches a specific filter
    public bool MatchesFilter(string key, object value)
    {
        return key switch
        {
            "method" => Method == (string)value,
            "device" => Device == (string)value,
            _ => false
        };
    }
}

public class AerProviderSimulator
{
    // Stub methods for available methods and devices
    public List<string> AvailableMethods()
    {
        return new List<string> { "automatic", "statevector", "density_matrix", "unitary", "tensor_network" };
    }

    public List<string> AvailableDevices()
    {
        return new List<string> { "CPU", "GPU" };
    }
}
