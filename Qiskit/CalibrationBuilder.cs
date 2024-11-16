using System;
using System.Collections.Generic;

public abstract class CalibrationBuilder : TransformationPass
{
    /// <summary>
    /// Determines if a given node supports the calibration.
    /// </summary>
    /// <param name="nodeOp">Target instruction object.</param>
    /// <param name="qubits">List of integer qubit indices to check.</param>
    /// <returns>True if calibration can be provided for this node and qubits.</returns>
    public abstract bool Supported(CircuitInst nodeOp, List<int> qubits);

    /// <summary>
    /// Gets the calibrated schedule for the given instruction and qubits.
    /// </summary>
    /// <param name="nodeOp">Target instruction object.</param>
    /// <param name="qubits">List of integer qubit indices to check.</param>
    /// <returns>A calibrated schedule for the given instruction and qubits.</returns>
    public abstract object GetCalibration(CircuitInst nodeOp, List<int> qubits);

    /// <summary>
    /// Runs the calibration adder pass on the provided DAG circuit.
    /// </summary>
    /// <param name="dag">The DAG circuit to schedule.</param>
    /// <returns>A DAG with calibrations added to it.</returns>
    public override DAGCircuit Run(DAGCircuit dag)
    {
        foreach (var node in dag.GateNodes())
        {
            List<int> qubits = new List<int>();
            foreach (var q in node.Qargs)
            {
                qubits.Add(dag.FindBit(q).Index);  // Get the index of each qubit
            }

            if (Supported(node.Op, qubits) && !dag.HasCalibrationFor(node))
            {
                // Calibration can be provided and no user-defined calibration is already provided
                try
                {
                    var schedule = GetCalibration(node.Op, qubits);
                    var publisher = schedule.GetMetadata("publisher") ?? CalibrationPublisher.QISKIT;

                    // Add calibration if it's not from the backend provider
                    if (publisher != CalibrationPublisher.BACKEND_PROVIDER)
                    {
                        dag.AddCalibration(node.Op, qubits, schedule);
                    }
                }
                catch (CalibrationNotAvailable ex)
                {
                    // Fail in schedule generation, ignore this node
                    continue;
                }
            }
        }

        return dag;
    }
}