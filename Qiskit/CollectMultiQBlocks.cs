using System.Collections.Generic;
using System.Linq;

public class CollectMultiQBlocks : AnalysisPass
{
    private Dictionary<int, int> parent;  // Parent dictionary for DSU
    private Dictionary<int, List<int>> bitGroups;  // Grouped bits in DSU
    private Dictionary<int, List<DAGOpNode>> gateGroups;  // Groups of gates for bits
    private int maxBlockSize;  // Maximum allowed block size

    public CollectMultiQBlocks(int maxBlockSize = 2)
    {
        parent = new Dictionary<int, int>();
        bitGroups = new Dictionary<int, List<int>>();
        gateGroups = new Dictionary<int, List<DAGOpNode>>();
        this.maxBlockSize = maxBlockSize;
    }

    private int FindSet(int index)
    {
        if (!parent.ContainsKey(index))
        {
            parent[index] = index;
            bitGroups[index] = new List<int> { index };
            gateGroups[index] = new List<DAGOpNode>();
        }

        if (parent[index] == index)
        {
            return index;
        }

        parent[index] = FindSet(parent[index]);
        return parent[index];
    }

    private void UnionSet(int set1, int set2)
    {
        set1 = FindSet(set1);
        set2 = FindSet(set2);

        if (set1 == set2)
        {
            return;
        }

        if (gateGroups[set1].Count < gateGroups[set2].Count)
        {
            var temp = set1;
            set1 = set2;
            set2 = temp;
        }

        parent[set2] = set1;
        gateGroups[set1].AddRange(gateGroups[set2]);
        bitGroups[set1].AddRange(bitGroups[set2]);
        gateGroups[set2].Clear();
        bitGroups[set2].Clear();
    }

    // Ensure the signature matches the base class AnalysisPass
    public override void Run(DAG dag) // Ensure the correct method signature here
    {
        parent.Clear();
        bitGroups.Clear();
        gateGroups.Clear();

        var blockList = new List<List<DAGOpNode>>();

        Func<DAGNode, string> collectKey = (x) =>
        {
            if (x is DAGInNode)
                return "a";
            if (!(x is DAGOpNode))
                return "d";
            if (x is DAGOpNode opNode)
            {
                if (opNode.Op.IsParameterized || opNode.Op.Condition != null)
                    return "c";
                return "b" + (char)('a' + opNode.Qargs.Count);
            }
            return "d";
        };

        var opNodes = dag.TopologicalOpNodes(key: collectKey);

        foreach (var node in opNodes)
        {
            bool canProcess = true;
            bool makesTooBig = false;

            if (node.Op.Condition != null || node.Op.IsParameterized || !(node.Op is Gate))
            {
                canProcess = false;
            }

            var curQubits = node.Qargs.Select(q => dag.FindBit(q).Index).ToHashSet();

            if (canProcess)
            {
                // Check if the node fits within the max block size
                var cTops = new HashSet<int>();
                foreach (var bit in curQubits)
                {
                    cTops.Add(FindSet(bit));
                }

                int totalSize = cTops.Sum(group => bitGroups[group].Count);
                if (totalSize > maxBlockSize)
                {
                    makesTooBig = true;
                }
            }

            if (!canProcess)
            {
                foreach (var bit in curQubits)
                {
                    int bitSet = FindSet(bit);
                    if (gateGroups[bitSet].Count > 0)
                    {
                        blockList.Add(new List<DAGOpNode>(gateGroups[bitSet]));
                    }
                    var curSet = new HashSet<int>(bitGroups[bitSet]);
                    foreach (var v in curSet)
                    {
                        parent[v] = v;
                        bitGroups[v] = new List<int> { v };
                        gateGroups[v] = new List<DAGOpNode>();
                    }
                }
            }

            if (makesTooBig)
            {
                var savings = new Dictionary<int, int>();
                int totalSize = 0;

                foreach (var bit in curQubits)
                {
                    int top = FindSet(bit);
                    if (savings.ContainsKey(top))
                    {
                        savings[top] -= 1;
                    }
                    else
                    {
                        savings[top] = bitGroups[top].Count - 1;
                        totalSize += bitGroups[top].Count;
                    }
                }

                var sortedSavings = savings
                    .Select(s => new KeyValuePair<int, int>(s.Value, s.Key))
                    .OrderByDescending(s => s.Key)
                    .ToList();

                int savingsNeed = totalSize - maxBlockSize;
                foreach (var item in sortedSavings)
                {
                    if (savingsNeed > 0)
                    {
                        savingsNeed -= item.Key;
                        if (gateGroups[item.Value].Count >= 1)
                        {
                            blockList.Add(new List<DAGOpNode>(gateGroups[item.Value]));
                        }
                        var curSet = new HashSet<int>(bitGroups[item.Value]);
                        foreach (var v in curSet)
                        {
                            parent[v] = v;
                            bitGroups[v] = new List<int> { v };
                            gateGroups[v] = new List<DAGOpNode>();
                        }
                    }
                }
            }

            if (canProcess)
            {
                if (curQubits.Count > maxBlockSize)
                    continue;

                int prev = -1;
                foreach (var bit in curQubits)
                {
                    if (prev != -1)
                    {
                        UnionSet(prev, bit);
                    }
                    prev = bit;
                }
                gateGroups[FindSet(prev)].Add(node);
            }
        }

        foreach (var index in parent.Keys)
        {
            if (parent[index] == index && gateGroups[index].Count > 0)
            {
                blockList.Add(new List<DAGOpNode>(gateGroups[index]));
            }
        }

        propertySet["block_list"] = blockList;
    }
}
