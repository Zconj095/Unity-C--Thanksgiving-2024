using System;
using System.Collections.Generic;
using UnityEngine;

public class CNFLogicCircuit
{
    private int numVariables;
    private List<List<int>> clauses;

    public CNFLogicCircuit(int numVariables, List<List<int>> clauses)
    {
        this.numVariables = numVariables;
        this.clauses = clauses;
    }

    public QuantumCircuit ConstructCircuit(QuantumRegister variableRegister, QuantumRegister outputRegister, QuantumRegister ancillaryRegister, string mctMode = "basic")
    {
        var circuit = new QuantumCircuit(variableRegister, outputRegister, ancillaryRegister);

        foreach (var clause in clauses)
        {
            var flags = ConvertLiteralsToFlags(clause);
            if (flags != null)
            {
                var orCircuit = BuildORCircuit(flags, mctMode);
                circuit.Append(orCircuit, variableRegister, ancillaryRegister);
            }
        }

        circuit.MCT(clauses, outputRegister, ancillaryRegister, mctMode);
        return circuit;
    }

    private List<int> ConvertLiteralsToFlags(List<int> literals)
    {
        var flags = new List<int>(new int[numVariables]);
        foreach (var lit in literals)
        {
            if (lit > 0) flags[lit - 1] = 1;
            else if (lit < 0) flags[-lit - 1] = -1;
        }
        return flags;
    }

    private QuantumCircuit BuildORCircuit(List<int> flags, string mctMode)
    {
        // Logic to build OR circuit using flags
        return new QuantumCircuit();
    }
}

public class DNFLogicCircuit
{
    private int numVariables;
    private List<List<int>> clauses;

    public DNFLogicCircuit(int numVariables, List<List<int>> clauses)
    {
        this.numVariables = numVariables;
        this.clauses = clauses;
    }

    public QuantumCircuit ConstructCircuit(QuantumRegister variableRegister, QuantumRegister outputRegister, QuantumRegister ancillaryRegister, string mctMode = "basic")
    {
        var circuit = new QuantumCircuit(variableRegister, outputRegister, ancillaryRegister);

        foreach (var clause in clauses)
        {
            var flags = ConvertLiteralsToFlags(clause);
            if (flags != null)
            {
                var andCircuit = BuildANDCircuit(flags, mctMode);
                circuit.Append(andCircuit, variableRegister, ancillaryRegister);
            }
        }

        circuit.MCT(clauses, outputRegister, ancillaryRegister, mctMode);
        return circuit;
    }

    private List<int> ConvertLiteralsToFlags(List<int> literals)
    {
        var flags = new List<int>(new int[numVariables]);
        foreach (var lit in literals)
        {
            if (lit > 0) flags[lit - 1] = 1;
            else if (lit < 0) flags[-lit - 1] = -1;
        }
        return flags;
    }

    private QuantumCircuit BuildANDCircuit(List<int> flags, string mctMode)
    {
        // Logic to build AND circuit using flags
        return new QuantumCircuit();
    }
}

public class ESOPLogicCircuit
{
    private int numVariables;
    private List<List<int>> clauses;

    public ESOPLogicCircuit(int numVariables, List<List<int>> clauses)
    {
        this.numVariables = numVariables;
        this.clauses = clauses;
    }

    public QuantumCircuit ConstructCircuit(QuantumRegister variableRegister, QuantumRegister outputRegister, QuantumRegister ancillaryRegister, string mctMode = "basic")
    {
        var circuit = new QuantumCircuit(variableRegister, outputRegister, ancillaryRegister);

        foreach (var clause in clauses)
        {
            var flags = ConvertLiteralsToFlags(clause);
            if (flags != null)
            {
                var xorCircuit = BuildXORCircuit(flags, mctMode);
                circuit.Append(xorCircuit, variableRegister, ancillaryRegister);
            }
        }

        return circuit;
    }

    private List<int> ConvertLiteralsToFlags(List<int> literals)
    {
        var flags = new List<int>(new int[numVariables]);
        foreach (var lit in literals)
        {
            if (lit > 0) flags[lit - 1] = 1;
            else if (lit < 0) flags[-lit - 1] = -1;
        }
        return flags;
    }

    private QuantumCircuit BuildXORCircuit(List<int> flags, string mctMode)
    {
        // Logic to build XOR circuit using flags
        return new QuantumCircuit();
    }
}
