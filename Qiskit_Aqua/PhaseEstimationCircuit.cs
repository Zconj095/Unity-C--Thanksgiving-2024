using System;
using System.Collections.Generic;

public class PhaseEstimationCircuit
{
    private QuantumOperator operatorObj;
    private QuantumCircuit stateIn;
    private QuantumCircuit iqft;
    private int numTimeSlices;
    private int numAncillae;
    private string expansionMode;
    private int expansionOrder;
    private double evoTime;
    private QuantumCircuit unitaryCircuitFactory;
    private QuantumCircuit constructedCircuit;

    public PhaseEstimationCircuit(
        QuantumOperator operatorObj = null,
        QuantumCircuit stateIn = null,
        QuantumCircuit iqft = null,
        int numTimeSlices = 1,
        int numAncillae = 1,
        string expansionMode = "trotter",
        int expansionOrder = 1,
        double evoTime = 2 * Math.PI,
        QuantumCircuit unitaryCircuitFactory = null)
    {
        if ((operatorObj != null && unitaryCircuitFactory != null) ||
            (operatorObj == null && unitaryCircuitFactory == null))
        {
            throw new ArgumentException("Provide either an operator or a unitary circuit factory, not both.");
        }

        this.operatorObj = operatorObj;
        this.stateIn = stateIn;
        this.iqft = iqft ?? QuantumCircuit.CreateQFT(numAncillae, inverse: true, doSwaps: false);
        this.numTimeSlices = numTimeSlices;
        this.numAncillae = numAncillae;
        this.expansionMode = expansionMode;
        this.expansionOrder = expansionOrder;
        this.evoTime = evoTime;
        this.unitaryCircuitFactory = unitaryCircuitFactory;
    }

    public QuantumCircuit ConstructCircuit(QuantumRegister stateRegister = null, QuantumRegister ancillaryRegister = null, bool measurement = false)
    {
        if (constructedCircuit != null)
        {
            return constructedCircuit;
        }

        ancillaryRegister ??= new QuantumRegister(numAncillae, "ancilla");
        stateRegister ??= operatorObj != null
            ? new QuantumRegister(operatorObj.NumQubits, "state")
            : new QuantumRegister(unitaryCircuitFactory.NumTargetQubits, "state");

        QuantumCircuit qc = new QuantumCircuit(stateRegister, ancillaryRegister);

        // Initialize the state
        if (stateIn != null)
        {
            qc.Append(stateIn);
        }

        // Apply Hadamard gates to the ancillae
        qc.H(ancillaryRegister);

        // Phase kickback logic
        if (operatorObj != null)
        {
            for (int i = 0; i < numAncillae; i++)
            {
                double power = Math.Pow(2, i);
                var evolutionGate = operatorObj.GetEvolutionGate(-evoTime, numTimeSlices, controlled: true, power: power, expansionMode: expansionMode, expansionOrder: expansionOrder);
                qc.Append(evolutionGate, stateRegister, ancillaryRegister[i]);
            }
        }
        else if (unitaryCircuitFactory != null)
        {
            for (int i = 0; i < numAncillae; i++)
            {
                unitaryCircuitFactory.BuildControlledPower(qc, stateRegister, ancillaryRegister[i], Math.Pow(2, i));
            }
        }

        // Apply Inverse QFT to the ancillae
        if (iqft.NumQubits != numAncillae)
        {
            throw new ArgumentException($"IQFT must have {numAncillae} qubits.");
        }

        qc.Append(iqft, ancillaryRegister);

        // Measurement
        if (measurement)
        {
            ClassicalRegister classicalRegister = new ClassicalRegister(numAncillae, "measure");
            qc.AddRegister(classicalRegister);
            qc.Measure(ancillaryRegister, classicalRegister);
        }

        constructedCircuit = qc;
        return qc;
    }
}
