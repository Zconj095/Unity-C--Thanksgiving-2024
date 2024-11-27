using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public class KDTreeBase<TNode> where TNode : KDTreeNodeBase<TNode>, new()
    {
        private int count;
        private int dimensions;
        private int leaves;

        private Func<double[], double[], double> distanceFunction = EuclideanDistance;

        public int Dimensions => dimensions;

        public Func<double[], double[], double> DistanceFunction
        {
            get => distanceFunction;
            set => distanceFunction = value ?? throw new ArgumentNullException(nameof(value));
        }

        public int Count => count;

        public int Leaves => leaves;

        public TNode Root { get; private set; }

        public KDTreeBase(int dimensions)
        {
            this.dimensions = dimensions;
        }

        public KDTreeBase(int dimensions, TNode root) : this(dimensions)
        {
            Root = root;
            InitializeCounts();
        }

        private void InitializeCounts()
        {
            count = 0;
            leaves = 0;

            if (Root != null)
            {
                foreach (var node in TraverseTree(Root))
                {
                    count++;
                    if (node.IsLeaf) leaves++;
                }
            }
        }

        private IEnumerable<TNode> TraverseTree(TNode node)
        {
            if (node == null) yield break;

            yield return node;

            if (node.Left != null)
            {
                foreach (var left in TraverseTree(node.Left)) yield return left;
            }

            if (node.Right != null)
            {
                foreach (var right in TraverseTree(node.Right)) yield return right;
            }
        }

        public TNode Nearest(double[] position, out double distance)
        {
            TNode result = Root;
            distance = DistanceFunction(Root.Position, position);
            Nearest(Root, position, ref result, ref distance);
            return result;
        }

        private void Nearest(TNode current, double[] position, ref TNode match, ref double minDistance)
        {
            if (current == null) return;

            double d = DistanceFunction(position, current.Position);
            if (d < minDistance)
            {
                minDistance = d;
                match = current;
            }

            int axis = current.Axis;
            double diff = position[axis] - current.Position[axis];

            TNode first = diff <= 0 ? current.Left : current.Right;
            TNode second = diff <= 0 ? current.Right : current.Left;

            Nearest(first, position, ref match, ref minDistance);
            if (Math.Abs(diff) < minDistance) Nearest(second, position, ref match, ref minDistance);
        }

        public void AddNode(double[] position)
        {
            count++;
            Root = Insert(Root, position, 0);
        }

        private TNode Insert(TNode node, double[] position, int depth)
        {
            if (node == null)
            {
                return new TNode
                {
                    Axis = depth % dimensions,
                    Position = position
                };
            }

            if (position[node.Axis] < node.Position[node.Axis])
            {
                node.Left = Insert(node.Left, position, depth + 1);
            }
            else
            {
                node.Right = Insert(node.Right, position, depth + 1);
            }

            return node;
        }

        public void Clear()
        {
            Root = null;
            count = 0;
            leaves = 0;
        }

        private static double EuclideanDistance(double[] a, double[] b)
        {
            double sum = 0;
            for (int i = 0; i < a.Length; i++)
            {
                double diff = a[i] - b[i];
                sum += diff * diff;
            }
            return Math.Sqrt(sum);
        }
    }

    public abstract class KDTreeNodeBase<TNode> where TNode : KDTreeNodeBase<TNode>
    {
        public int Axis { get; set; }
        public double[] Position { get; set; }
        public TNode Left { get; set; }
        public TNode Right { get; set; }
        public bool IsLeaf => Left == null && Right == null;
    }
}
