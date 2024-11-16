using System;
using System.Collections.Generic;
using System.Linq;

public class BlockSplitter
{
    private Dictionary<int, int> leader = new Dictionary<int, int>();
    private Dictionary<int, List<DAGOpNode>> group = new Dictionary<int, List<DAGOpNode>>();

    private int FindLeader(int index)
    {
        if (!leader.ContainsKey(index))
        {
            leader[index] = index;
            group[index] = new List<DAGOpNode>();
            return index;
        }
        if (leader[index] == index)
            return index;
        leader[index] = FindLeader(leader[index]);
        return leader[index];
    }

    private void UnionLeaders(int index1, int index2)
    {
        int leader1 = FindLeader(index1);
        int leader2 = FindLeader(index2);
        if (leader1 == leader2) return;
        if (group[leader1].Count < group[leader2].Count)
        {
            var temp = leader1;
            leader1 = leader2;
            leader2 = temp;
        }
        leader[leader2] = leader1;
        group[leader1].AddRange(group[leader2]);
        group[leader2].Clear();
    }

    public List<List<DAGOpNode>> Run(List<DAGOpNode> block)
    {
        foreach (var node in block)
        {
            var indices = node.Qargs;
            if (!indices.Any()) continue;
            var first = indices[0];
            foreach (var index in indices.Skip(1))
            {
                UnionLeaders(first, index);
            }
            group[FindLeader(first)].Add(node);
        }

        var blocks = new List<List<DAGOpNode>>();
        foreach (var index in leader.Keys.Where(index => leader[index] == index))
        {
            blocks.Add(group[index]);
        }

        return blocks;
    }
}

public static List<List<DAGOpNode>> SplitBlockIntoLayers(List<DAGOpNode> block)
{
    var bitDepths = new Dictionary<Bit, int>();
    var layers = new List<List<DAGOpNode>>();

    foreach (var node in block)
    {
        var curBits = new HashSet<Bit>(node.Qargs);
        curBits.UnionWith(node.Cargs);

        var cond = node.Op.GetCondition();
        if (cond != null)
        {
            curBits.UnionWith(ConditionResources(cond).Clbits);
        }

        var curDepth = curBits.Max(bit => bitDepths.GetValueOrDefault(bit, 0));

        while (layers.Count <= curDepth)
        {
            layers.Add(new List<DAGOpNode>());
        }

        foreach (var bit in curBits)
        {
            bitDepths[bit] = curDepth + 1;
        }
        layers[curDepth].Add(node);
    }

    return layers;
}
