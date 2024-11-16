using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AlgorithmResult
{
    // Internal storage for data
    private readonly Dictionary<string, object> _data;

    // Constructor
    public AlgorithmResult(Dictionary<string, object> initialData = null)
    {
        _data = initialData != null ? new Dictionary<string, object>(initialData) : new Dictionary<string, object>();
    }

    // Accessor to retrieve the internal data dictionary
    public IReadOnlyDictionary<string, object> Data => _data;

    // Prevent setting items directly
    public void Add(string key, object value)
    {
        throw new InvalidOperationException("'Add' is not allowed on this object.");
    }

    // Prevent clearing the data
    public void Clear()
    {
        throw new InvalidOperationException("'Clear' is not allowed on this object.");
    }

    // Prevent removing items directly
    public bool Remove(string key)
    {
        throw new InvalidOperationException("'Remove' is not allowed on this object.");
    }

    // Prevent updating items directly
    public void Update(Dictionary<string, object> newValues)
    {
        throw new InvalidOperationException("'Update' is not allowed on this object.");
    }

    // Combine results from another AlgorithmResult
    public void Combine(AlgorithmResult result)
    {
        if (result == null)
        {
            throw new ArgumentNullException(nameof(result), "Argument result expected.");
        }

        if (ReferenceEquals(this, result))
        {
            return; // If the same object, no action is needed
        }

        // Copy properties from the result to this instance
        foreach (var property in result.Data)
        {
            if (property.Value == null)
            {
                // Remove the key if the value is null
                _data.Remove(property.Key);
            }
            else
            {
                // Update the value
                _data[property.Key] = property.Value;
            }
        }
    }

    // Check if a key exists
    public bool ContainsKey(string key)
    {
        return _data.ContainsKey(key);
    }

    // Get a value by key
    public object Get(string key)
    {
        if (!_data.ContainsKey(key))
        {
            throw new KeyNotFoundException($"Key '{key}' not found in the AlgorithmResult.");
        }

        return _data[key];
    }

    // Prevent setting a value directly
    public void Set(string key, object value)
    {
        throw new InvalidOperationException("'Set' is not allowed on this object.");
    }
}
