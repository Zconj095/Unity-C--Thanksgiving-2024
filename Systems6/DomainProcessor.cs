using System;
using System.Reflection;
using UnityEngine;

public class DomainProcessor : MonoBehaviour
{
    [Tooltip("Specify the namespace or domain to process.")]
    public string domainNamespace = "MyGame.Domain"; // Example namespace

    private void Start()
    {
        if (string.IsNullOrEmpty(domainNamespace))
        {
            Debug.LogError("Domain namespace is not specified.");
            return;
        }

        Debug.Log($"Processing domain: {domainNamespace}");
        ProcessDomain(domainNamespace);
    }

    private void ProcessDomain(string domainNamespace)
    {
        // Get all types within the specified namespace
        var domainTypes = GetTypesInNamespace(Assembly.GetExecutingAssembly(), domainNamespace);
        
        foreach (var type in domainTypes)
        {
            Debug.Log($"Processing Type: {type.Name}");
            ProcessType(type);
        }
    }

    private void ProcessType(Type type)
    {
        // Instantiate object if it has a parameterless constructor
        object instance = null;
        try
        {
            if (type.GetConstructor(Type.EmptyTypes) != null)
            {
                instance = Activator.CreateInstance(type);
                Debug.Log($"Created instance of type: {type.Name}");
            }
        }
        catch (Exception ex)
        {
            Debug.LogWarning($"Could not create instance of {type.Name}: {ex.Message}");
        }

        // List all properties
        foreach (var property in type.GetProperties())
        {
            Debug.Log($"Property: {property.Name}, Type: {property.PropertyType.Name}");
            if (instance != null && property.CanRead)
            {
                try
                {
                    var value = property.GetValue(instance);
                    Debug.Log($"Property Value: {value}");
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Could not read property {property.Name}: {e.Message}");
                }
            }
        }

        // List all methods
        foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
        {
            Debug.Log($"Method: {method.Name}");
            if (instance != null && method.GetParameters().Length == 0)
            {
                try
                {
                    var result = method.Invoke(instance, null);
                    Debug.Log($"Invoked Method {method.Name}, Result: {result}");
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Could not invoke method {method.Name}: {e.Message}");
                }
            }
        }
    }

    private Type[] GetTypesInNamespace(Assembly assembly, string namespaceName)
    {
        return Array.FindAll(assembly.GetTypes(), t => t.Namespace == namespaceName);
    }
}
