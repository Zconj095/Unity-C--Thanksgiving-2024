using System;
using System.Collections.Generic;
using UnityEngine;

public class CollectAndCollapse : MonoBehaviour
{
    private Func<DAGCircuit, List<List<DAGOpNode>>> collectFunction;
    private Action<DAGCircuit, List<List<DAGOpNode>>> collapseFunction;
    private bool doCommutativeAnalysis;

    public CollectAndCollapse(
        Func<DAGCircuit, List<List<DAGOpNode>>> collectFunction,
        Action<DAGCircuit, List<List<DAGOpNode>>> collapseFunction,
        bool doCommutativeAnalysis = false)
    {
        this.collectFunction = collectFunction;
        this.collapseFunction = collapseFunction;
        this.doCommutativeAnalysis = doCommutativeAnalysis;
    }
}