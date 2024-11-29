using System;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class LocationProcessor : MonoBehaviour
{
    [Tooltip("Specify the tag to filter location-related objects.")]
    public string locationTag = "Location";

    private void Start()
    {
        if (string.IsNullOrEmpty(locationTag))
        {
            Debug.LogError("Location tag is not specified.");
            return;
        }

        Debug.Log($"Processing objects with tag: {locationTag}");
        ProcessLocations(locationTag);
    }

    private void ProcessLocations(string tag)
    {
        // Find all GameObjects with the specified tag
        var locationObjects = GameObject.FindGameObjectsWithTag(tag);

        if (locationObjects.Length == 0)
        {
            Debug.LogWarning($"No objects found with tag: {tag}");
            return;
        }

        foreach (var obj in locationObjects)
        {
            Debug.Log($"Processing object: {obj.name}");
            ProcessObject(obj);
        }
    }

    private void ProcessObject(GameObject obj)
    {
        // Get all components attached to the GameObject
        var components = obj.GetComponents<Component>();

        foreach (var component in components)
        {
            Debug.Log($"Processing component: {component.GetType().Name}");
            ProcessComponent(component);
        }
    }

    private void ProcessComponent(Component component)
    {
        var type = component.GetType();

        // Get properties related to position and rotation
        var transform = component as Transform;
        if (transform != null)
        {
            Debug.Log($"Transform Position: {transform.position}");
            Debug.Log($"Transform Rotation: {transform.rotation}");
            return;
        }

        // Reflection: Analyze properties and methods of other components
        var properties = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
        foreach (var property in properties)
        {
            if (property.CanRead)
            {
                try
                {
                    var value = property.GetValue(component);
                    Debug.Log($"Property: {property.Name}, Value: {value}");
                }
                catch (Exception e)
                {
                    Debug.LogWarning($"Failed to read property {property.Name}: {e.Message}");
                }
            }
        }

        var methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(m => m.GetParameters().Length == 0); // Filter for parameterless methods
        foreach (var method in methods)
        {
            try
            {
                var result = method.Invoke(component, null);
                Debug.Log($"Method: {method.Name}, Result: {result}");
            }
            catch (Exception e)
            {
                Debug.LogWarning($"Failed to invoke method {method.Name}: {e.Message}");
            }
        }
    }
}
