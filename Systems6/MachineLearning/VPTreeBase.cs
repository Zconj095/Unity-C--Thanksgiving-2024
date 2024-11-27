using System;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace EdgeLoreMachineLearning
{
    public class VPTreeBase<TPoint, TNode> where TNode : VPTreeNodeBase<TPoint, TNode>, new()
    {
        private double tau;
        private Func<TPoint, TPoint, double> distanceFunction;

        public Func<TPoint, TPoint, double> DistanceFunction
        {
            get => distanceFunction;
            set => distanceFunction = value ?? throw new ArgumentNullException(nameof(value));
        }

        public double Radius
        {
            get => tau;
            set => tau = value;
        }

        public TNode Root { get; private set; }

        public VPTreeBase(Func<TPoint, TPoint, double> distanceFunction)
        {
            DistanceFunction = distanceFunction ?? throw new ArgumentNullException(nameof(distanceFunction));
        }

        public List<NodeDistance<TNode>> Nearest(TPoint position, int neighbors)
        {
            return Nearest(position, neighbors, new List<NodeDistance<TNode>>());
        }

        public List<NodeDistance<TNode>> Nearest(TPoint position, int neighbors, List<NodeDistance<TNode>> results)
        {
            var heap = new PriorityQueue<NodeDistance<TNode>>(neighbors, Comparer<NodeDistance<TNode>>.Create((a, b) => b.Distance.CompareTo(a.Distance)));
            tau = double.MaxValue;

            Search(Root, position, neighbors, heap);

            while (heap.Count > 0)
            {
                results.Add(heap.Dequeue());
            }

            results.Reverse(); // Results in ascending order of distance
            return results;
        }

        internal TNode BuildFromPoints(TPoint[] items, int lower, int upper, bool inPlace)
        {
            if (!inPlace)
                items = (TPoint[])items.Clone();

            if (upper == lower)
                return null;

            int median = (upper + lower) / 2;
            var node = new TNode { Position = items[lower] };

            if (upper - lower > 1)
            {
                var rand = new System.Random();
                int randomIndex = rand.Next(lower, upper);
                Swap(items, lower, randomIndex);

                Array.Sort(items, lower + 1, upper - lower - 1, Comparer<TPoint>.Create((a, b) =>
                    DistanceFunction(items[lower], a).CompareTo(DistanceFunction(items[lower], b))));

                node.Threshold = DistanceFunction(items[lower], items[median]);
                node.Left = BuildFromPoints(items, lower + 1, median, true);
                node.Right = BuildFromPoints(items, median, upper, true);
            }

            return node;
        }

        private void Search(TNode node, TPoint target, int k, PriorityQueue<NodeDistance<TNode>> heap)
        {
            if (node == null)
                return;

            double dist = DistanceFunction(node.Position, target);

            if (dist < tau)
            {
                if (heap.Count == k)
                    heap.Dequeue();

                heap.Enqueue(new NodeDistance<TNode>(node, dist));

                if (heap.Count == k)
                    tau = heap.Min.Distance; // Corrected: Use Min instead of Peek
            }

            if (node.Left == null && node.Right == null)
                return;

            double thresholdDiff = node.Threshold - dist;

            if (thresholdDiff <= 0)
            {
                Search(node.Right, target, k, heap);
                if (Math.Abs(thresholdDiff) <= tau)
                    Search(node.Left, target, k, heap);
            }
            else
            {
                Search(node.Left, target, k, heap);
                if (Math.Abs(thresholdDiff) <= tau)
                    Search(node.Right, target, k, heap);
            }
        }


        private void Swap(TPoint[] array, int i, int j)
        {
            TPoint temp = array[i];
            array[i] = array[j];
            array[j] = temp;
        }
    }

    public abstract class VPTreeNodeBase<TPoint, TNode> where TNode : VPTreeNodeBase<TPoint, TNode>
    {
        public TPoint Position { get; set; }
        public double Threshold { get; set; }
        public TNode Left { get; set; }
        public TNode Right { get; set; }
    }

    public class NodeDistance<TNode>
    {
        public TNode Node { get; }
        public double Distance { get; }

        public NodeDistance(TNode node, double distance)
        {
            Node = node;
            Distance = distance;
        }
    }

    public class PriorityQueue<T> : SortedSet<T>
    {
        private readonly int maxSize;

        public PriorityQueue(int maxSize, IComparer<T> comparer) : base(comparer)
        {
            this.maxSize = maxSize;
        }

        public new void Enqueue(T item)
        {
            Add(item);
            if (Count > maxSize)
                Remove(Max);
        }

        public T Dequeue()
        {
            var first = Min;
            Remove(first);
            return first;
        }
    }
}
