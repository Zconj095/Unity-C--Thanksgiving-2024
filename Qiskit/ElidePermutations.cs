using System;
using System.Collections.Generic;
using UnityEngine;

public class ElidePermutations : TransformationPass
{
    public override object Run(object dag)
    {
        // Check if layout is already set
        if (propertySet.ContainsKey("layout") && propertySet["layout"] != null)
        {
            Debug.LogWarning("ElidePermutations is not valid after a layout has been set. This indicates an invalid pass manager construction.");
            return dag;
        }

        // Run the ElidePermutationsRS logic
        var result = ElidePermutationsRS.Run(dag);

        // If no result was generated, return the original DAG
        if (result == null)
        {
            return dag;
        }

        // Otherwise, process the result (new DAG and qubit mapping)
        var newDag = result.Item1;
        var qubitMapping = result.Item2;

        var inputQubitMapping = new Dictionary<QuantumQubit, int>();
        for (int i = 0; i < dag.Qubits.Count; i++)
        {
            inputQubitMapping[dag.Qubits[i]] = i;
        }

        propertySet["original_layout"] = new Layout(inputQubitMapping);

        if (!propertySet.ContainsKey("original_qubit_indices"))
        {
            propertySet["original_qubit_indices"] = inputQubitMapping;
        }

        var newLayout = new Layout(new Dictionary<QuantumQubit, int>());
        for (int idx = 0; idx < qubitMapping.Count; idx++)
        {
            newLayout.Add(dag.Qubits[qubitMapping[idx]], idx);
        }

        if (propertySet.ContainsKey("virtual_permutation_layout"))
        {
            var currentLayout = propertySet["virtual_permutation_layout"] as Layout;
            propertySet["virtual_permutation_layout"] = newLayout.Compose(currentLayout.Inverse(dag.Qubits, dag.Qubits), dag.Qubits);
        }
        else
        {
            propertySet["virtual_permutation_layout"] = newLayout;
        }

        return newDag;
    }
}
