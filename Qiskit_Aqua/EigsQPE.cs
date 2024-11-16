using System;
using System.Collections.Generic;

public class EigsQPE
{
    private QuantumCircuit iqft;
    private WeightedPauliOperator operatorHamiltonian;
    private int numTimeSlices;
    private int numAncillae;
    private string expansionMode;
    private int expansionOrder;
    private double evolutionTime;
    private bool negativeEigenvalues;
    private List<QuantumCircuit> negativeEigenvalueQFTs;
    private QuantumCircuit constructedCircuit;
    private QuantumRegister outputRegister;
    private QuantumRegister inputRegister;

    public EigsQPE(
        WeightedPauliOperator hamiltonian,
        QuantumCircuit iqftCircuit,
        int timeSlices = 1,
        int ancillae = 1,
        string mode = "trotter",
        int order = 1,
        double? evoTime = null,
        bool handleNegativeEvals = false,
        List<QuantumCircuit> negativeEvalsQFTs = null
    )
    {
        if (timeSlices < 1 || ancillae < 1 || order < 1)
        {
            throw new ArgumentException("Time slices, ancillae, and order must be at least 1.");
        }

        if (mode != "trotter" && mode != "suzuki")
        {
            throw new ArgumentException("Expansion mode must be 'trotter' or 'suzuki'.");
        }

        operatorHamiltonian = hamiltonian;
        iqft = iqftCircuit;
        numTimeSlices = timeSlices;
        numAncillae = ancillae;
        expansionMode = mode;
        expansionOrder = order;
        negativeEigenvalues = handleNegativeEvals;
        negativeEigenvalueQFTs = negativeEvalsQFTs ?? new List<QuantumCircuit> { null, null };

        InitializeEvolutionTime(evoTime);
    }

    private void InitializeEvolutionTime(double? evoTime)
    {
        if (evoTime.HasValue)
        {
            evolutionTime = evoTime.Value;
        }
        else
        {
            double lMax = operatorHamiltonian.GetMaximumPauliSum();
            if (!negativeEigenvalues)
            {
                evolutionTime = (1 - Math.Pow(2, -numAncillae)) * 2 * Math.PI / lMax;
            }
            else
            {
                evolutionTime = (0.5 - Math.Pow(2, -numAncillae)) * 2 * Math.PI / lMax;
            }
        }
    }

    public (int stateQubits, int ancillaQubits) GetRegisterSizes()
    {
        return (operatorHamiltonian.NumQubits, numAncillae);
    }

    public double GetScaling()
    {
        return evolutionTime;
    }

    public QuantumCircuit ConstructCircuit(QuantumRegister stateRegister)
    {
        if (stateRegister == null)
        {
            throw new ArgumentNullException(nameof(stateRegister), "State register cannot be null.");
        }

        var ancillaryRegister = new QuantumRegister(numAncillae, "ancilla");
        var phaseEstimation = new PhaseEstimationCircuit(
            operatorHamiltonian, iqft, numTimeSlices, numAncillae, expansionMode, expansionOrder, evolutionTime
        );

        QuantumCircuit circuit = phaseEstimation.ConstructCircuit(stateRegister, ancillaryRegister);

        if (negativeEigenvalues)
        {
            HandleNegativeEigenvalues(circuit, ancillaryRegister);
        }

        constructedCircuit = circuit;
        outputRegister = ancillaryRegister;
        inputRegister = stateRegister;

        return circuit;
    }

    private void HandleNegativeEigenvalues(QuantumCircuit circuit, QuantumRegister ancillaryRegister)
    {
        var signQubit = ancillaryRegister[0];
        var stateQubits = new List<QuantumQubit>();
        for (int i = 1; i < ancillaryRegister.Count; i++)
        {
            stateQubits.Add(ancillaryRegister[i]);
        }

        void ApplyQFT(QuantumCircuit qftCircuit)
        {
            if (qftCircuit == null) return;

            if (qftCircuit.NumQubits != stateQubits.Count)
            {
                throw new InvalidOperationException("QFT size does not match state qubit count.");
            }

            qftCircuit.DoSwaps = false;
            circuit.Append(qftCircuit);
        }

        // Flip the sign qubit onto each state qubit
        foreach (var qubit in stateQubits)
        {
            circuit.CX(signQubit, qubit);
        }

        ApplyQFT(negativeEigenvalueQFTs[0]);

        // Phase adjustments
        for (int i = 0; i < stateQubits.Count; i++)
        {
            double phase = 2 * Math.PI / Math.Pow(2, i + 1);
            circuit.CPhase(phase, signQubit, stateQubits[i]);
        }

        ApplyQFT(negativeEigenvalueQFTs[1]);
    }
}
