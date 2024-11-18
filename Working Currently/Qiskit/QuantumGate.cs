using System;
using UnityEngine;

public class QuantumGate : MonoBehaviour
{
    public string Name { get; private set; }
    public int[] Qubits { get; private set; }
    public Func<Complex[], Complex[]> Operation { get; private set; }

    public QuantumGate(string name, int[] qubits, Func<Complex[], Complex[]> operation = null)
    {
        Name = name;
        Qubits = qubits;
        Operation = operation;
    }

    public static QuantumGate Hadamard(int qubit)
    {
        return new QuantumGate("H", new[] { qubit }, state =>
        {
            Complex[] newState = new Complex[state.Length];
            int halfSize = state.Length / 2;
            for (int i = 0; i < halfSize; i++)
            {
                newState[i] = new Complex(
                    (state[i].Real + state[i + halfSize].Real) / Math.Sqrt(2),
                    (state[i].Imaginary + state[i + halfSize].Imaginary) / Math.Sqrt(2)
                );
                newState[i + halfSize] = new Complex(
                    (state[i].Real - state[i + halfSize].Real) / Math.Sqrt(2),
                    (state[i].Imaginary - state[i + halfSize].Imaginary) / Math.Sqrt(2)
                );
            }
            return newState;
        });
    }

    public static QuantumGate CNOT(int control, int target)
    {
        return new QuantumGate("CNOT", new[] { control, target }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int controlBit = (i >> control) & 1;
                if (controlBit == 1)
                {
                    int flippedIndex = i ^ (1 << target);
                    newState[flippedIndex] = state[i];
                    newState[i] = state[flippedIndex];
                }
            }
            return newState;
        });
    }

    public static QuantumGate PauliX(int qubit)
    {
        return new QuantumGate("X", new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int flippedIndex = i ^ (1 << qubit);
                newState[flippedIndex] = state[i];
            }
            return newState;
        });
    }

    public static QuantumGate PauliY(int qubit)
    {
        return new QuantumGate("Y", new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int flippedIndex = i ^ (1 << qubit);
                newState[flippedIndex] = new Complex(
                    -state[i].Imaginary,
                    state[i].Real
                );
            }
            return newState;
        });
    }

    public static QuantumGate PauliZ(int qubit)
    {
        return new QuantumGate("Z", new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                if (((i >> qubit) & 1) == 1)
                {
                    newState[i] = new Complex(-state[i].Real, -state[i].Imaginary);
                }
            }
            return newState;
        });
    }


    public static QuantumGate RX(int qubit, double angle)
    {
        return new QuantumGate($"RX({angle})", new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int targetBit = (i >> qubit) & 1; // Extract the qubit bit
                int flippedIndex = i ^ (1 << qubit); // Flip the target qubit

                double cos = Math.Cos(angle / 2);
                double sin = Math.Sin(angle / 2);

                // RX matrix transformation
                newState[i] = new Complex(
                    cos * state[i].Real - sin * state[flippedIndex].Imaginary,
                    cos * state[i].Imaginary + sin * state[flippedIndex].Real
                );
                newState[flippedIndex] = new Complex(
                    cos * state[flippedIndex].Real + sin * state[i].Imaginary,
                    cos * state[flippedIndex].Imaginary - sin * state[i].Real
                );
            }
            return newState;
        });
    }

    public static QuantumGate RY(int qubit, double angle)
    {
        return new QuantumGate($"RY({angle})", new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int targetBit = (i >> qubit) & 1; // Extract the qubit bit
                int flippedIndex = i ^ (1 << qubit); // Flip the target qubit

                double cos = Math.Cos(angle / 2);
                double sin = Math.Sin(angle / 2);

                // RY matrix transformation
                newState[i] = targetBit == 0
                    ? new Complex(
                        cos * state[i].Real + sin * state[flippedIndex].Real,
                        cos * state[i].Imaginary + sin * state[flippedIndex].Imaginary
                    )
                    : new Complex(
                        -sin * state[i].Real + cos * state[flippedIndex].Real,
                        -sin * state[i].Imaginary + cos * state[flippedIndex].Imaginary
                    );
            }
            return newState;
        });
    }

    public static QuantumGate RZ(int qubit, double angle)
    {
        return new QuantumGate($"RZ({angle})", new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int targetBit = (i >> qubit) & 1; // Extract the qubit bit

                // Compute the phase factor
                double phaseReal = Math.Cos(angle / 2);
                double phaseImaginary = Math.Sin(angle / 2);
                Complex phaseFactor = targetBit == 0 ? new Complex(1, 0) : new Complex(phaseReal, -phaseImaginary);

                // RZ matrix transformation
                newState[i] = state[i] * phaseFactor;
            }
            return newState;
        });
    }

    public static QuantumGate Phase(int qubit, double angle)
    {
        return new QuantumGate($"Phase({angle})", new[] { qubit }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int targetBit = (i >> qubit) & 1; // Extract the qubit bit

                if (targetBit == 1)
                {
                    // Compute the phase factor
                    double phaseReal = Math.Cos(angle);
                    double phaseImaginary = Math.Sin(angle);
                    Complex phaseFactor = new Complex(phaseReal, phaseImaginary);

                    // Apply phase shift
                    newState[i] = state[i] * phaseFactor;
                }
            }
            return newState;
        });
    }




    public static QuantumGate SWAP(int qubit1, int qubit2)
    {
        return new QuantumGate("SWAP", new[] { qubit1, qubit2 }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int swappedIndex = i ^ ((1 << qubit1) | (1 << qubit2));
                newState[swappedIndex] = state[i];
            }
            return newState;
        });
    }

    public static QuantumGate Toffoli(int control1, int control2, int target)
    {
        return new QuantumGate("Toffoli", new[] { control1, control2, target }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                int controlBits = ((i >> control1) & 1) & ((i >> control2) & 1);
                if (controlBits == 1)
                {
                    int flippedIndex = i ^ (1 << target);
                    newState[flippedIndex] = state[i];
                    newState[i] = state[flippedIndex];
                }
            }
            return newState;
        });
    }

    public static QuantumGate Fredkin(int control, int swap1, int swap2)
    {
        return new QuantumGate("Fredkin", new[] { control, swap1, swap2 }, state =>
        {
            Complex[] newState = (Complex[])state.Clone();
            for (int i = 0; i < state.Length; i++)
            {
                if (((i >> control) & 1) == 1)
                {
                    int swappedIndex = i ^ ((1 << swap1) | (1 << swap2));
                    newState[swappedIndex] = state[i];
                    newState[i] = state[swappedIndex];
                }
            }
            return newState;
        });
    }

    public override string ToString()
    {
        return $"{Name} Gate on Qubits: {string.Join(", ", Qubits)}";
    }
}
