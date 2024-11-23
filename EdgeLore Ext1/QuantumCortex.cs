using UnityEngine;
using System.Collections.Generic;
using System;
namespace CognitiveCortex
{
    public class QuantumCircuit
    {
        private Qubit[] qubits; // Array to hold qubits
        private QuantumGate[] gates; // Array to hold quantum gates

        // Constructor: Initializes the number of qubits and creates the qubit array.
        public QuantumCircuit(int numQubits)
        {
            qubits = new Qubit[numQubits];
            for (int i = 0; i < numQubits; i++)
            {
                qubits[i] = new Qubit(); // Initialize each qubit
            }
            gates = new QuantumGate[0]; // Initialize empty gate array.  We'll dynamically resize this.

        }

        // Method to add a Hadamard gate to a specific qubit.
        public void ApplyHadamard(int qubitIndex)
        {
            if (qubitIndex < 0 || qubitIndex >= qubits.Length)
            {
                throw new IndexOutOfRangeException("Qubit index out of range.");
            }
            Array.Resize(ref gates, gates.Length + 1);
            gates[gates.Length -1] = new HadamardGate(qubits[qubitIndex]);
        }


        // Method to add a CNOT gate (controlled-NOT).
        public void ApplyCNOT(int controlQubitIndex, int targetQubitIndex)
        {
            if (controlQubitIndex < 0 || controlQubitIndex >= qubits.Length ||
                targetQubitIndex < 0 || targetQubitIndex >= qubits.Length)
            {
                throw new IndexOutOfRangeException("Qubit index out of range.");
            }
            Array.Resize(ref gates, gates.Length + 1);
            gates[gates.Length-1] = new CNOTGate(qubits[controlQubitIndex], qubits[targetQubitIndex]);
        }


        // Method to add a measurement to a specific qubit.  Returns the measurement result.
        public MeasurementResult Measure(int qubitIndex)
        {
            if (qubitIndex < 0 || qubitIndex >= qubits.Length)
            {
                throw new IndexOutOfRangeException("Qubit index out of range.");
            }
            // Simulate measurement (replace with actual quantum simulation logic later)
            System.Random random = new System.Random();
            return random.NextDouble() < qubits[qubitIndex].ProbabilityOfOne ? MeasurementResult.One : MeasurementResult.Zero;

        }

        //Method to run the simulation
        public void RunSimulation() {
            foreach(QuantumGate gate in gates){
                gate.ApplyGate();
            }
        }

        // Inner classes for Qubit and Quantum Gates (Simplified representations)
        public class Qubit
        {
            public double ProbabilityOfOne { get; set; } = 0.5; //Initially in superposition.  This will change after gates are applied.

            public void SetProbabilityOfOne(double prob) {
                if (prob < 0 || prob > 1) throw new ArgumentException("Probability must be between 0 and 1.");
                ProbabilityOfOne = prob;
            }

        }
        public enum MeasurementResult {Zero, One}

        //Base class for quantum gates
        public abstract class QuantumGate {
            public abstract void ApplyGate();
        }

        //Hadamard gate
        public class HadamardGate : QuantumGate {
            private Qubit qubit;
            public HadamardGate(Qubit q) { qubit = q;}

            public override void ApplyGate() {
                //Simplified Hadamard transformation - replace with accurate quantum computation if using a real simulator.
                qubit.SetProbabilityOfOne(0.5); //Simplified for demonstration
            }
        }

        //CNOT gate
        public class CNOTGate : QuantumGate {
            private Qubit controlQubit;
            private Qubit targetQubit;

            public CNOTGate(Qubit c, Qubit t) {
                controlQubit = c;
                targetQubit = t;
            }

            public override void ApplyGate() {
                //Simplified CNOT transformation - replace with accurate quantum computation if using a real simulator
                if (controlQubit.ProbabilityOfOne > 0.5) {
                    targetQubit.SetProbabilityOfOne(1 - targetQubit.ProbabilityOfOne);
                }
                //More sophisticated implementation would involve a better representation of qubit states than just a probability.
            }
        }


    }
}