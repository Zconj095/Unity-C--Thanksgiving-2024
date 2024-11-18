using System;
using System.Collections.Generic;

public class BitLocations
{
    // Dictionary to hold bit names and their respective locations
    private Dictionary<string, int> _bitLocationMap;

    public BitLocations()
    {
        _bitLocationMap = new Dictionary<string, int>();
    }

    // Add a bit and its location to the map
    public void AddBitLocation(string bitName, int location)
    {
        if (!_bitLocationMap.ContainsKey(bitName))
        {
            _bitLocationMap.Add(bitName, location);
        }
        else
        {
            // Handle case where bit already exists (optional)
            throw new ArgumentException($"Bit {bitName} already exists in the map.");
        }
    }

    // Get the location of a bit by its name
    public int GetBitLocation(string bitName)
    {
        if (_bitLocationMap.ContainsKey(bitName))
        {
            return _bitLocationMap[bitName];
        }
        else
        {
            throw new KeyNotFoundException($"Bit {bitName} not found in the map.");
        }
    }

    // Check if a bit exists in the map
    public bool ContainsBit(string bitName)
    {
        return _bitLocationMap.ContainsKey(bitName);
    }

    // Remove a bit from the map
    public void RemoveBit(string bitName)
    {
        if (_bitLocationMap.ContainsKey(bitName))
        {
            _bitLocationMap.Remove(bitName);
        }
        else
        {
            throw new KeyNotFoundException($"Bit {bitName} not found in the map.");
        }
    }

    // Get all the bits stored in the map
    public IEnumerable<string> GetAllBits()
    {
        return _bitLocationMap.Keys;
    }

    // Get all the locations stored in the map
    public IEnumerable<int> GetAllLocations()
    {
        return _bitLocationMap.Values;
    }
    
    // Clear all bit locations
    public void Clear()
    {
        _bitLocationMap.Clear();
    }
}
