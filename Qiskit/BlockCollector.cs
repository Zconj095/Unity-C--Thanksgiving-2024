using System;
using System.Collections.Generic;
using System.Linq;

public class BlockCollector
{
    private DAGCircuit dag;
    private List<DAGOpNode> _pendingNodes;
    private Dictionary<DAGOpNode, int> _inDegree;
    private bool _collectFromBack;
    private bool isDAGDependency;

    public BlockCollector(DAGCircuit dag)
    {
        this.dag = dag;
        this._pendingNodes = new List<DAGOpNode>();
        this._inDegree = new Dictionary<DAGOpNode, int>();
        this._collectFromBack = false;
        this.isDAGDependency = false;
    }

    public BlockCollector(DAGDependency dag)
    {
        this.dag = dag;
        this._pendingNodes = new List<DAGOpNode>();
        this._inDegree = new Dictionary<DAGOpNode, int>();
        this._collectFromBack = false;
        this.isDAGDependency = true;
    }

    private void _SetupInDegrees()
    {
        _pendingNodes.Clear();
        _inDegree.Clear();
        foreach (var node in _OpNodes())
        {
            int degree = _DirectPreds(node).Count();
            _inDegree[node] = degree;
            if (degree == 0)
            {
                _pendingNodes.Add(node);
            }
        }
    }

    private IEnumerable<DAGOpNode> _OpNodes()
    {
        if (!isDAGDependency)
            return dag.OpNodes();
        else
            return dag.GetNodes();
    }

    private IEnumerable<DAGOpNode> _DirectPreds(DAGOpNode node)
    {
        if (!isDAGDependency)
        {
            if (_collectFromBack)
                return dag.Successors(node).Where(succ => succ is DAGOpNode);
            else
                return dag.Predecessors(node).Where(pred => pred is DAGOpNode);
        }
        else
        {
            if (_collectFromBack)
                return dag.GetNode(dag.DirectSuccessors(node.NodeId)).Select(id => dag.GetNode(id));
            else
                return dag.GetNode(dag.DirectPredecessors(node.NodeId)).Select(id => dag.GetNode(id));
        }
    }

    private IEnumerable<DAGOpNode> _DirectSuccs(DAGOpNode node)
    {
        if (!isDAGDependency)
        {
            if (_collectFromBack)
                return dag.Predecessors(node).Where(succ => succ is DAGOpNode);
            else
                return dag.Successors(node).Where(pred => pred is DAGOpNode);
        }
        else
        {
            if (_collectFromBack)
                return dag.GetNode(dag.DirectPredecessors(node.NodeId)).Select(id => dag.GetNode(id));
            else
                return dag.GetNode(dag.DirectSuccessors(node.NodeId)).Select(id => dag.GetNode(id));
        }
    }

    private bool _HaveUncollectedNodes()
    {
        return _pendingNodes.Count > 0;
    }

    public List<DAGOpNode> CollectMatchingBlock(Func<DAGOpNode, bool> filterFn)
    {
        var currentBlock = new List<DAGOpNode>();
        var unprocessedPendingNodes = new List<DAGOpNode>(_pendingNodes);
        _pendingNodes.Clear();

        while (unprocessedPendingNodes.Any())
        {
            var newPendingNodes = new List<DAGOpNode>();

            foreach (var node in unprocessedPendingNodes)
            {
                if (filterFn(node))
                {
                    currentBlock.Add(node);

                    foreach (var succ in _DirectSuccs(node))
                    {
                        _inDegree[succ]--;
                        if (_inDegree[succ] == 0)
                        {
                            newPendingNodes.Add(succ);
                        }
                    }
                }
                else
                {
                    _pendingNodes.Add(node);
                }
            }

            unprocessedPendingNodes = newPendingNodes;
        }

        return currentBlock;
    }

    public List<List<DAGOpNode>> CollectAllMatchingBlocks(
        Func<DAGOpNode, bool> filterFn,
        bool splitBlocks = true,
        int minBlockSize = 2,
        bool splitLayers = false,
        bool collectFromBack = false)
    {
        Func<DAGOpNode, bool> notFilterFn = node => !filterFn(node);

        _collectFromBack = collectFromBack;
        _SetupInDegrees();

        var matchingBlocks = new List<List<DAGOpNode>>();

        while (_HaveUncollectedNodes())
        {
            CollectMatchingBlock(notFilterFn);
            var matchingBlock = CollectMatchingBlock(filterFn);
            if (matchingBlock.Any())
            {
                matchingBlocks.Add(matchingBlock);
            }
        }

        if (splitLayers)
        {
            var tmpBlocks = new List<List<DAGOpNode>>();
            foreach (var block in matchingBlocks)
            {
                tmpBlocks.AddRange(SplitBlockIntoLayers(block));
            }
            matchingBlocks = tmpBlocks;
        }

        if (splitBlocks)
        {
            var tmpBlocks = new List<List<DAGOpNode>>();
            foreach (var block in matchingBlocks)
            {
                tmpBlocks.AddRange(new BlockSplitter().Run(block));
            }
            matchingBlocks = tmpBlocks;
        }

        if (_collectFromBack)
        {
            matchingBlocks = matchingBlocks.Select(block => block.AsEnumerable().Reverse().ToList()).Reverse().ToList();
        }

        matchingBlocks = matchingBlocks.Where(block => block.Count >= minBlockSize).ToList();

        return matchingBlocks;
    }
}
