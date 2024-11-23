using UnityEngine;
using System.Collections.Generic;

public class ServiceManager : MonoBehaviour
{
    private Dictionary<string, bool> services = new Dictionary<string, bool>();

    public void AddService(string serviceName)
    {
        if (services.ContainsKey(serviceName))
        {
            Debug.LogError($"Service '{serviceName}' already exists.");
            return;
        }

        services[serviceName] = true; // Assume the service is active by default.
        Debug.Log($"Service '{serviceName}' added successfully.");
    }

    public void StartService(string serviceName)
    {
        if (services.TryGetValue(serviceName, out bool isActive))
        {
            if (isActive)
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
        if (services.TryGetValue(serviceName, out bool isActive))
        {
            if (!isActive)
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
