using System;
using System.Collections.Generic;

public class QuantumGates
{
    // FLIP GATES
    // XGate (Pauli-X or NOT gate)
    public static void SetPostconditionsXGate(ref Qubit x1, ref Qubit y1)
    {
        y1 = !x1;  // Negation of x1, as in NOT(x1)
    }

    public static void SetPostconditionsCXGate(ref Qubit x1, ref Qubit y1)
    {
        y1 = !x1;  // CNOT gate, flip y1 if x1 is 1
    }

    public static void SetPostconditionsCCXGate(ref Qubit x1, ref Qubit y1)
    {
        y1 = !x1;  // Toffoli gate (CCX) flip y1 based on x1's state
    }

    // YGate (Pauli-Y gate)
    public static void SetPostconditionsYGate(ref Qubit x1, ref Qubit y1)
    {
        y1 = !x1;  // Negation of x1
    }

    public static void SetPostconditionsCYGate(ref Qubit x1, ref Qubit y1)
    {
        y1 = !x1;  // Controlled Y (CY) gate
    }

    // PHASE GATES
    // IdGate (Identity gate)
    public static void SetPostconditionsIGate(ref Qubit x1, ref Qubit y1)
    {
        y1 = x1;  // Identity gate, y1 equals x1
    }

    // ZGate (Pauli-Z gate)
    public static void SetPostconditionsZGate(ref Qubit x1, ref Qubit y1)
    {
        y1 = x1;  // ZGate postconditions, y1 equals x1
    }

    // SGate (Phase shift gate)
    public static void SetPostconditionsSGate(ref Qubit x1, ref Qubit y1)
    {
        y1 = x1;  // SGate postconditions, y1 equals x1
    }

    // TGate (π/4 phase shift)
    public static void SetPostconditionsTGate(ref Qubit x1, ref Qubit y1)
    {
        y1 = x1;  // TGate postconditions, y1 equals x1
    }

    // RzGate (Rotation gate around Z axis)
    public static void SetPostconditionsRZGate(ref Qubit x1, ref Qubit y1)
    {
        y1 = x1;  // RZGate postconditions, y1 equals x1
    }

    // U1Gate (General phase gate)
    public static void SetPostconditionsU1Gate(ref Qubit x1, ref Qubit y1)
    {
        y1 = x1;  // U1Gate postconditions, y1 equals x1
    }

    // MULTI-QUBIT GATES
    // SwapGate (Quantum swap gate)
    public static void SetPostconditionsSwapGate(ref Qubit x1, ref Qubit x2, ref Qubit y1, ref Qubit y2)
    {
        y1 = x2;
        y2 = x1;  // SwapGate postconditions
    }

    // Controlled Swap (CSwap) gate
    public static void SetPostconditionsCSwapGate(ref Qubit x1, ref Qubit x2, ref Qubit y1, ref Qubit y2)
    {
        y1 = x2;
        y2 = x1;  // CSwapGate postconditions
    }
}
