using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Delegate for tree traversal methods.
    /// </summary>
    /// <returns>An enumerator traversing the tree.</returns>
    public delegate IEnumerator<DecisionNode> DecisionTreeTraversalMethod(DecisionNode node);

    /// <summary>
    ///   Common traversal methods for n-ary trees.
    /// </summary>
    public static class DecisionTreeTraversal
    {
        /// <summary>
        ///   Breadth-first traversal method.
        /// </summary>
        public static IEnumerator<DecisionNode> BreadthFirst(DecisionNode tree)
        {
            if (tree == null)
                yield break;

            Queue<DecisionNode> queue = new Queue<DecisionNode>();
            queue.Enqueue(tree);

            while (queue.Count > 0)
            {
                DecisionNode current = queue.Dequeue();
                yield return current;

                if (current.Branches != null)
                {
                    for (int i = 0; i < current.Branches.Count; i++)
                    {
                        queue.Enqueue(current.Branches[i]);
                    }
                }
            }
        }

        /// <summary>
        ///   Depth-first traversal method.
        /// </summary>
        public static IEnumerator<DecisionNode> DepthFirst(DecisionNode tree)
        {
            if (tree == null)
                yield break;

            Stack<DecisionNode> stack = new Stack<DecisionNode>();
            stack.Push(tree);

            while (stack.Count > 0)
            {
                DecisionNode current = stack.Pop();
                yield return current;

                if (current.Branches != null)
                {
                    for (int i = current.Branches.Count - 1; i >= 0; i--)
                    {
                        stack.Push(current.Branches[i]);
                    }
                }
            }
        }

        /// <summary>
        ///   Post-order traversal method.
        /// </summary>
        public static IEnumerator<DecisionNode> PostOrder(DecisionNode tree)
        {
            if (tree == null)
                yield break;

            Dictionary<DecisionNode, int> cursors = new Dictionary<DecisionNode, int>();
            DecisionNode currentNode = tree;

            while (currentNode != null)
            {
                // Move to the first child
                DecisionNode nextNode = null;
                if (currentNode.Branches != null && currentNode.Branches.Count > 0)
                {
                    nextNode = currentNode.Branches[0];
                }

                if (nextNode != null)
                {
                    currentNode = nextNode;
                    continue;
                }

                // No children, process the current node
                while (currentNode != null)
                {
                    yield return currentNode;

                    // Check for siblings
                    nextNode = GetNextSibling(cursors, currentNode);

                    if (nextNode != null)
                    {
                        currentNode = nextNode;
                        break;
                    }

                    // Move up to parent
                    currentNode = currentNode.Parent;
                }
            }
        }

        private static DecisionNode GetNextSibling(Dictionary<DecisionNode, int> cursors, DecisionNode node)
        {
            DecisionNode parent = node.Parent;
            if (parent == null || parent.Branches == null)
                return null;

            // Get the current node's index
            int currentIndex = -1;
            if (cursors.TryGetValue(node, out currentIndex))
            {
                cursors[node] = currentIndex + 1;
            }
            else
            {
                currentIndex = parent.Branches.IndexOf(node);
                cursors[node] = currentIndex + 1;
            }

            int nextIndex = currentIndex + 1;
            if (nextIndex < parent.Branches.Count)
            {
                DecisionNode sibling = parent.Branches[nextIndex];
                cursors[sibling] = nextIndex;
                return sibling;
            }

            return null;
        }
    }
}
