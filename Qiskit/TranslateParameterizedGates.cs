using System;
using System.Collections.Generic;
using UnityEngine;

public class TranslateParameterizedGates : TransformationPass
{
    private List<string> supportedGates;
    private Target target;
    private BasisTranslator translator;

    /// <summary>
    /// Initializes the TranslateParameterizedGates pass.
    /// </summary>
    /// <param name="supportedGates">List of supported gates specified as strings. If null, a target must be provided.</param>
    /// <param name="equivalenceLibrary">Equivalence library to translate gates. Defaults to the standard library.</param>
    /// <param name="target">Target containing supported operations. If null, supportedGates must be set.</param>
    public TranslateParameterizedGates(
        List<string> supportedGates = null,
        EquivalenceLibrary equivalenceLibrary = null,
        Target target = null)
    {
        if (equivalenceLibrary == null)
        {
            equivalenceLibrary = EquivalenceLibrary.GetDefaultLibrary(); // Replace with your default equivalence library
        }

        if (target != null)
        {
            this.supportedGates = new List<string>(target.OperationNames);
        }
        else if (supportedGates == null)
        {
            throw new ArgumentException("One of `supportedGates` or `target` must be specified.");
        }
        else
        {
            this.supportedGates = supportedGates;
        }

        this.target = target;
        this.translator = new BasisTranslator(equivalenceLibrary, this.supportedGates, target);
    }

    /// <summary>
    /// Runs the pass on the provided DAG circuit.
    /// </summary>
    /// <param name="dag">The input DAG circuit.</param>
    /// <returns>A DAG where parameterized gates have been unrolled.</returns>
    /// <exception cref="Exception">If the circuit cannot be unrolled.</exception>
    public override DAGCircuit Run(DAGCircuit dag)
    {
        foreach (var node in dag.OpNodes())
        {
            if (IsParameterized(node.Op) && !IsSupported(node, supportedGates, target))
            {
                DAGCircuit unrolled;
                if (node.Op.Definition != null)
                {
                    unrolled = Run(CircuitToDag(node.Op.Definition)); // Recursive unrolling
                }
                else
                {
                    try
                    {
                        unrolled = translator.Run(InstructionToDag(node.Op));
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Failed to translate final block.", ex);
                    }
                }

                dag.SubstituteNodeWithDag(node, unrolled);
            }
        }

        return dag;
    }

    private static bool IsParameterized(Instruction op)
    {
        foreach (var param in op.Params)
        {
            if (param is ParameterExpression paramExpr && paramExpr.HasParameters())
            {
                return true;
            }
        }
        return false;
    }

    private static bool IsSupported(DAGOpNode node, List<string> supportedGates, Target target)
    {
        if (target != null)
        {
            return target.IsInstructionSupported(node.Op.Name);
        }

        return supportedGates.Contains(node.Op.Name);
    }

    private static DAGCircuit InstructionToDag(Instruction op)
    {
        var dag = new DAGCircuit();
        dag.AddQubits(op.NumQubits);
        dag.AddClbits(op.NumClbits);
        dag.ApplyOperationBack(op, dag.Qubits, dag.Clbits, false);

        return dag;
    }
}