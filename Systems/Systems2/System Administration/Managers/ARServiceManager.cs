using UnityEngine;
using System.Collections.Generic;

public class ARServiceManager : MonoBehaviour
{
    // Simulated service storage
    private Dictionary<string, bool> services = new Dictionary<string, bool>();

    public void StartService(string serviceName)
    {
        if (services.TryGetValue(serviceName, out bool isRunning))
        {
            if (isRunning)
            {
                Debug.Log($"Service '{serviceName}' is already running.");
            }
            else
            {
                services[serviceName] = true;
                Debug.Log($"Service '{serviceName}' started.");
            }
        }
        else
        {
            Debug.LogError($"Service '{serviceName}' does not exist.");
        }
    }

    public void StopService(string serviceName)
    {
        if (services.TryGetValue(serviceName, out bool isRunning))
        {
            if (!isRunning)
            {
                Debug.Log($"Service '{serviceName}' is already stopped.");
            }
            else
            {
                services[serviceName] = false;
                Debug.Log($"Service '{serviceName}' stopped.");
            }
        }
        else
        {
            Debug.LogError($"Service '{serviceName}' does not exist.");
        }
    }

    public void AddService(string serviceName)
    {
        if (!services.ContainsKey(serviceName))
        {
            services[serviceName] = false; // Initially stopped
            Debug.Log($"Service '{serviceName}' added successfully.");
        }
        else
        {
            Debug.LogError($"Service '{serviceName}' already exists.");
        }
    }

    public void RemoveService(string serviceName)
    {
        if (services.Remove(serviceName))
        {
            Debug.Log($"Service '{serviceName}' removed successfully.");
        }
        else
        {
            Debug.LogError($"Service '{serviceName}' does not exist.");
        }
    }
}
