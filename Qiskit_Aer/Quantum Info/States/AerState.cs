using System;
using System.Collections.Generic;

public enum State
{
    INITIALIZING = 1,
    ALLOCATED = 2,
    MAPPED = 3,
    MOVED = 4,
    RELEASED = 5,
    CLOSED = 6
}

public class AerState
{
    private State _state;
    private AerStateWrapper _nativeState;
    private object _initData;
    private object _movedData;
    private int _lastQubit;

    public AerState()
    {
        _state = State.INITIALIZING;
        _nativeState = new AerStateWrapper();
        _initData = null;
        _movedData = null;
        _lastQubit = -1;
    }

    private void AssertAllocatedOrMapped()
    {
        if (_state == State.INITIALIZING)
            throw new Exception("AerState has not been initialized yet.");
    }

    private void AssertInAllocatedQubits(int qubit)
    {
        if (qubit < 0 || qubit > _lastQubit)
            throw new Exception($"Invalid qubit index: {qubit}");
    }

    public void ApplyCX(int controlQubit, int targetQubit)
    {
        AssertAllocatedOrMapped();
        AssertInAllocatedQubits(controlQubit);
        AssertInAllocatedQubits(targetQubit);
        _nativeState.ApplyCX(new int[] { controlQubit, targetQubit });
    }

    public void ApplyMCX(int[] controlQubits, int targetQubit)
    {
        AssertAllocatedOrMapped();
        foreach (var qubit in controlQubits)
            AssertInAllocatedQubits(qubit);
        AssertInAllocatedQubits(targetQubit);
        var allQubits = new List<int>(controlQubits) { targetQubit };
        _nativeState.ApplyMCX(allQubits.ToArray());
    }

    public void ApplyY(int targetQubit)
    {
        AssertAllocatedOrMapped();
        AssertInAllocatedQubits(targetQubit);
        _nativeState.ApplyY(targetQubit);
    }

    public void ApplyCY(int controlQubit, int targetQubit)
    {
        AssertAllocatedOrMapped();
        AssertInAllocatedQubits(controlQubit);
        AssertInAllocatedQubits(targetQubit);
        _nativeState.ApplyCY(new int[] { controlQubit, targetQubit });
    }

    public void ApplyMCY(int[] controlQubits, int targetQubit)
    {
        AssertAllocatedOrMapped();
        foreach (var qubit in controlQubits)
            AssertInAllocatedQubits(qubit);
        AssertInAllocatedQubits(targetQubit);
        var allQubits = new List<int>(controlQubits) { targetQubit };
        _nativeState.ApplyMCY(allQubits.ToArray());
    }

    public void ApplyZ(int targetQubit)
    {
        AssertAllocatedOrMapped();
        AssertInAllocatedQubits(targetQubit);
        _nativeState.ApplyZ(targetQubit);
    }

    public void ApplyCZ(int controlQubit, int targetQubit)
    {
        AssertAllocatedOrMapped();
        AssertInAllocatedQubits(controlQubit);
        AssertInAllocatedQubits(targetQubit);
        _nativeState.ApplyCZ(new int[] { controlQubit, targetQubit });
    }

    public void ApplyMCZ(int[] controlQubits, int targetQubit)
    {
        AssertAllocatedOrMapped();
        foreach (var qubit in controlQubits)
            AssertInAllocatedQubits(qubit);
        AssertInAllocatedQubits(targetQubit);
        var allQubits = new List<int>(controlQubits) { targetQubit };
        _nativeState.ApplyMCZ(allQubits.ToArray());
    }

    public void ApplyMCPhase(int[] controlQubits, int targetQubit, double phase)
    {
        AssertAllocatedOrMapped();
        foreach (var qubit in controlQubits)
            AssertInAllocatedQubits(qubit);
        AssertInAllocatedQubits(targetQubit);
        var allQubits = new List<int>(controlQubits) { targetQubit };
        _nativeState.ApplyMCPhase(allQubits.ToArray(), phase);
    }

    public void ApplyH(int targetQubit)
    {
        AssertAllocatedOrMapped();
        AssertInAllocatedQubits(targetQubit);
        _nativeState.ApplyH(targetQubit);
    }

    public void ApplyU(int targetQubit, double theta, double phi, double lambda)
    {
        AssertAllocatedOrMapped();
        AssertInAllocatedQubits(targetQubit);
        _nativeState.ApplyU(targetQubit, theta, phi, lambda);
    }

    public void ApplyCU(int controlQubit, int targetQubit, double theta, double phi, double lambda, double gamma)
    {
        AssertAllocatedOrMapped();
        AssertInAllocatedQubits(controlQubit);
        AssertInAllocatedQubits(targetQubit);
        _nativeState.ApplyCU(new int[] { controlQubit, targetQubit }, theta, phi, lambda, gamma);
    }

    public void ApplyMCU(int[] controlQubits, int targetQubit, double theta, double phi, double lambda, double gamma)
    {
        AssertAllocatedOrMapped();
        foreach (var qubit in controlQubits)
            AssertInAllocatedQubits(qubit);
        AssertInAllocatedQubits(targetQubit);
        var allQubits = new List<int>(controlQubits) { targetQubit };
        _nativeState.ApplyMCU(allQubits.ToArray(), theta, phi, lambda, gamma);
    }

    public void ApplyMCSwap(int[] controlQubits, int qubit0, int qubit1)
    {
        AssertAllocatedOrMapped();
        foreach (var qubit in controlQubits)
            AssertInAllocatedQubits(qubit);
        AssertInAllocatedQubits(qubit0);
        AssertInAllocatedQubits(qubit1);
        var allQubits = new List<int>(controlQubits) { qubit0, qubit1 };
        _nativeState.ApplyMCSwap(allQubits.ToArray());
    }

    public int[] ApplyMeasure(int[] qubits)
    {
        AssertAllocatedOrMapped();
        foreach (var qubit in qubits)
            AssertInAllocatedQubits(qubit);
        return _nativeState.ApplyMeasure(qubits);
    }

    public void ApplyInitialize(int[] qubits, double[] vector)
    {
        AssertAllocatedOrMapped();
        foreach (var qubit in qubits)
            AssertInAllocatedQubits(qubit);
        _nativeState.ApplyInitialize(qubits, vector);
    }

    public void ApplyReset(int[] qubits)
    {
        AssertAllocatedOrMapped();
        foreach (var qubit in qubits)
            AssertInAllocatedQubits(qubit);
        _nativeState.ApplyReset(qubits);
    }

    public void ApplyKraus(int[] qubits, double[][] krausOps)
    {
        AssertAllocatedOrMapped();
        foreach (var qubit in qubits)
            AssertInAllocatedQubits(qubit);
        _nativeState.ApplyKraus(qubits, krausOps);
    }

    public double Probability(int[] outcome)
    {
        AssertAllocatedOrMapped();
        return _nativeState.Probability(outcome);
    }

    public double[] Probabilities(int[] qubits = null)
    {
        AssertAllocatedOrMapped();
        qubits ??= new int[_lastQubit + 1];
        return _nativeState.Probabilities(qubits);
    }

    public Dictionary<string, int> SampleCounts(int[] qubits = null, int shots = 1024)
    {
        AssertAllocatedOrMapped();
        qubits ??= new int[_lastQubit + 1];
        return _nativeState.SampleCounts(qubits, shots);
    }

    public List<int[]> SampleMemory(int[] qubits = null, int shots = 1024)
    {
        AssertAllocatedOrMapped();
        qubits ??= new int[_lastQubit + 1];
        return _nativeState.SampleMemory(qubits, shots);
    }
}
