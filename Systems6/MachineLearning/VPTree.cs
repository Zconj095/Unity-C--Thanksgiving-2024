using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Vantage-Point Tree for Unity.
    /// </summary>
    public class VPTree<TPoint>
    {
        private Func<TPoint, TPoint, double> distanceFunction;
        private VPTreeNode<TPoint> root;

        public VPTree(Func<TPoint, TPoint, double> distanceFunction)
        {
            this.distanceFunction = distanceFunction ?? throw new ArgumentNullException(nameof(distanceFunction));
        }

        public VPTreeNode<TPoint> Root => root;

        public static VPTree<TPoint> FromData(TPoint[] points, Func<TPoint, TPoint, double> distanceFunction, bool inPlace = false)
        {
            var tree = new VPTree<TPoint>(distanceFunction);
            tree.root = tree.BuildFromPoints(points, 0, points.Length, inPlace);
            return tree;
        }

        private VPTreeNode<TPoint> BuildFromPoints(TPoint[] points, int lower, int upper, bool inPlace)
        {
            if (upper == lower)
                return null;

            if (!inPlace)
                points = (TPoint[])points.Clone();

            // Choose a vantage point and move it to the start
            var random = new System.Random();
            int index = random.Next(lower, upper);
            Swap(points, lower, index);

            // Create a new node
            var node = new VPTreeNode<TPoint>
            {
                Position = points[lower]
            };

            // If there is more than one point, partition the space
            if (upper - lower > 1)
            {
                int median = (lower + upper) / 2;

                Array.Sort(points, lower + 1, upper - lower - 1, Comparer<TPoint>.Create((a, b) =>
                    distanceFunction(node.Position, a).CompareTo(distanceFunction(node.Position, b))));

                // Set threshold and build subtrees
                node.Threshold = distanceFunction(node.Position, points[median]);
                node.Left = BuildFromPoints(points, lower + 1, median, true);
                node.Right = BuildFromPoints(points, median, upper, true);
            }

            return node;
        }

        public List<TPoint> Nearest(TPoint target, int count)
        {
            var result = new List<TPoint>();
            var heap = new PriorityQueueV2<NodeDistanceV4<TPoint>>(count, Comparer<NodeDistanceV4<TPoint>>.Create((a, b) =>
                b.Distance.CompareTo(a.Distance)));

            double tau = double.MaxValue;

            Search(root, target, count, heap, ref tau);

            while (heap.Count > 0)
            {
                result.Add(heap.Dequeue().Node);
            }

            result.Reverse();
            return result;
        }

        private void Search(VPTreeNode<TPoint> node, TPoint target, int k, PriorityQueueV2<NodeDistanceV4<TPoint>> heap, ref double tau)
        {
            if (node == null)
                return;

            double dist = distanceFunction(node.Position, target);

            if (dist < tau)
            {
                if (heap.Count == k)
                    heap.Dequeue();

                heap.Enqueue(new NodeDistanceV4<TPoint>(node.Position, dist));

                if (heap.Count == k)
                    tau = heap.Peek().Distance;
            }

            if (node.Left == null && node.Right == null)
                return;

            if (dist < node.Threshold)
            {
                if (dist - tau <= node.Threshold)
                    Search(node.Left, target, k, heap, ref tau);

                if (dist + tau >= node.Threshold)
                    Search(node.Right, target, k, heap, ref tau);
            }
            else
            {
                if (dist + tau >= node.Threshold)
                    Search(node.Right, target, k, heap, ref tau);

                if (dist - tau <= node.Threshold)
                    Search(node.Left, target, k, heap, ref tau);
            }
        }

        private void Swap(TPoint[] array, int i, int j)
        {
            TPoint temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    public class VPTreeNode<TPoint>
    {
        public TPoint Position { get; set; }
        public double Threshold { get; set; }
        public VPTreeNode<TPoint> Left { get; set; }
        public VPTreeNode<TPoint> Right { get; set; }
    }

    public class NodeDistanceV4<TNode>
    {
        public TNode Node { get; }
        public double Distance { get; }

        public NodeDistanceV4(TNode node, double distance)
        {
            Node = node;
            Distance = distance;
        }
    }

    public class PriorityQueueV2<T> : SortedSet<T>
    {
        private readonly int maxSize;

        public PriorityQueueV2(int maxSize, IComparer<T> comparer) : base(comparer)
        {
            this.maxSize = maxSize;
        }

        public void Enqueue(T item)
        {
            Add(item);
            if (Count > maxSize)
                Remove(Max);
        }

        public T Dequeue()
        {
            T item = Min;
            Remove(item);
            return item;
        }

        public T Peek()
        {
            return Min;
        }
    }
}
