using System;
using System.Collections.Generic;
using System.Linq;

public class EquivalenceLibrary
{
    // A dictionary to store equivalences: key is a gate, value is a list of equivalent gates
    private Dictionary<string, List<string>> _equivalences;

    public EquivalenceLibrary()
    {
        _equivalences = new Dictionary<string, List<string>>();
    }

    // Add an equivalence between a gate and its equivalent gates
    public void AddEquivalence(string gate, List<string> equivalentGates)
    {
        if (_equivalences.ContainsKey(gate))
        {
            _equivalences[gate].AddRange(equivalentGates.Except(_equivalences[gate]));
        }
        else
        {
            _equivalences[gate] = equivalentGates;
        }
    }

    // Check if there is an equivalence for the given gate
    public bool HasEquivalence(string gate)
    {
        return _equivalences.ContainsKey(gate);
    }

    // Retrieve equivalent gates for a given gate
    public List<string> GetEquivalents(string gate)
    {
        if (_equivalences.ContainsKey(gate))
        {
            return _equivalences[gate];
        }
        else
        {
            throw new Exception($"No equivalence found for gate: {gate}");
        }
    }

    // Retrieve all stored equivalences
    public Dictionary<string, List<string>> GetAllEquivalences()
    {
        return _equivalences;
    }

    // Print out all equivalences for debugging
    public void PrintEquivalences()
    {
        foreach (var equivalence in _equivalences)
        {
            Console.WriteLine($"Gate: {equivalence.Key} => Equivalents: {string.Join(", ", equivalence.Value)}");
        }
    }

    // Check if two gates are equivalent
    public bool AreEquivalent(string gate1, string gate2)
    {
        if (_equivalences.ContainsKey(gate1))
        {
            return _equivalences[gate1].Contains(gate2);
        }
        return false;
    }
}
