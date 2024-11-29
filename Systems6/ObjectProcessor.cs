using System;
using System.Reflection;
using UnityEngine;

public class ObjectProcessor : MonoBehaviour
{
    [Tooltip("Assign the GameObject to process.")]
    public GameObject targetObject;

    private void Start()
    {
        if (targetObject == null)
        {
            Debug.LogError("Target object is not assigned.");
            return;
        }

        // Example: Process the components of the target object
        ProcessObject(targetObject);
    }

    private void ProcessObject(GameObject obj)
    {
        Debug.Log($"Processing object: {obj.name}");

        // Get all components attached to the GameObject
        Component[] components = obj.GetComponents<Component>();
        foreach (var component in components)
        {
            ProcessComponent(component);
        }
    }

    private void ProcessComponent(Component component)
    {
        Debug.Log($"Processing component: {component.GetType().Name}");

        // Get all properties of the component using reflection
        PropertyInfo[] properties = component.GetType().GetProperties();
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
                    Debug.LogWarning($"Could not read property {property.Name}: {e.Message}");
                }
            }
        }

        // Get all methods of the component using reflection
        MethodInfo[] methods = component.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance);
        foreach (var method in methods)
        {
            Debug.Log($"Method: {method.Name}");
        }
    }
}
