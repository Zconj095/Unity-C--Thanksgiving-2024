using System;
using System.Collections.Generic;
using UnityEngine;

public class CommutativeInverseCancellation : MonoBehaviour
{
    private bool matrixBased;
    private int maxQubits;
    private SCCCommChecker commChecker;  // Assuming SCCCommChecker is an existing utility class for commutativity check
    private Dictionary<string, float> phaseUpdateDict; // Store phase differences for each gate cancellation

    public CommutativeInverseCancellation(bool matrixBased = false, int maxQubits = 4)
    {
        this.matrixBased = matrixBased;
        this.maxQubits = maxQubits;
        this.commChecker = new SCCCommChecker();  // Initialize comm checker
        this.phaseUpdateDict = new Dictionary<string, float>();
    }

    // Check if node should be skipped based on conditions (e.g., non-gate nodes)
    private bool SkipNode(DAGOpNode node)
    {
        if (node == null)
            return true;

        if (node.IsDirective() || node.Name == "measure" || node.Name == "reset" || node.Name == "delay")
            return true;

        if (node.HasCondition())
            return true;

        if (node.IsParameterized())
            return true;

        return false;
    }

    // Check if two operations are inverse up to a phase
    private (bool, float) CheckInverse(DAGOpNode node1, DAGOpNode node2)
    {
        float phaseDifference = 0;
        bool isInverse = false;

        if (!matrixBased)
        {
            // Direct inverse comparison
            isInverse = node1.OpInverse().Equals(node2.Op());
        }
        else if (node2.Qargs.Count > maxQubits)
        {
            isInverse = false;
        }
        else
        {
            Matrix4x4 mat1 = node1.OpInverse().ToMatrix();
            Matrix4x4 mat2 = node2.Op().ToMatrix();
            isInverse = MatrixEquals(mat1, mat2, out phaseDifference);
        }

        return (isInverse, phaseDifference);
    }

    // Compare matrices for equality, ignoring phase
    private bool MatrixEquals(Matrix4x4 mat1, Matrix4x4 mat2, out float phaseDifference)
    {
        phaseDifference = 0;
        // Implement matrix comparison logic here (ignoring phase difference)
        return mat1 == mat2;  // Simplified, would need to actually check matrix equivalence
    }
}



