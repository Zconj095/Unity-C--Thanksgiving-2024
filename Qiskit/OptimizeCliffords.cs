using System;
using System.Collections.Generic;

public class OptimizeCliffords : TransformationPass
{
    public override DAGCircuit Run(DAGCircuit dag)
    {
        // List to hold blocks of Clifford gates
        var blocks = new List<List<DAGOpNode>>();
        DAGOpNode prevNode = null;
        List<DAGOpNode> currentBlock = new List<DAGOpNode>();

        // Iterate over all nodes in topological order
        foreach (var node in dag.TopologicalOpNodes())
        {
            if (node.Op is Clifford)
            {
                if (prevNode == null)
                {
                    // Start a new block
                    blocks.Add(currentBlock);
                    currentBlock = new List<DAGOpNode> { node };
                }
                else
                {
                    // If the qubits are the same, add to the current block
                    if (AreQubitsEqual(prevNode, node))
                    {
                        currentBlock.Add(node);
                    }
                    else
                    {
                        // Start a new block
                        blocks.Add(currentBlock);
                        currentBlock = new List<DAGOpNode> { node };
                    }
                }
                prevNode = node;
            }
            else
            {
                // If the current node is not a Clifford, finalize the current block
                if (currentBlock.Count > 0)
                {
                    blocks.Add(currentBlock);
                }
                prevNode = null;
                currentBlock.Clear();
            }
        }

        // Add the last block if it exists
        if (currentBlock.Count > 0)
        {
            blocks.Add(currentBlock);
        }

        // Replace each block of Clifford gates with a single composed Clifford
        foreach (var block in blocks)
        {
            if (block.Count <= 1) continue;

            var wirePosMap = new Dictionary<QuantumRegister, int>();
            for (int i = 0; i < block[0].Qargs.Length; i++)
            {
                wirePosMap[block[0].Qargs[i]] = i;
            }

            // Compose the Cliffords in the block
            Clifford clifford = block[0].Op as Clifford;
            for (int i = 1; i < block.Count; i++)
            {
                clifford = Clifford.Compose(block[i].Op as Clifford, clifford, front: true);
            }

            // Replace the block with the composed Clifford
            dag.ReplaceBlockWithOp(block, clifford, wirePosMap, cycleCheck: false);
        }

        return dag;
    }

    private bool AreQubitsEqual(DAGOpNode node1, DAGOpNode node2)
    {
        // Check if the qubits involved in the two nodes are the same
        if (node1.Qargs.Length != node2.Qargs.Length)
        {
            return false;
        }

        for (int i = 0; i < node1.Qargs.Length; i++)
        {
            if (node1.Qargs[i] != node2.Qargs[i])
            {
                return false;
            }
        }

        return true;
    }
}
