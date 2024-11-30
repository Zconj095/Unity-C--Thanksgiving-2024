using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    public abstract class BaseKNearestNeighbors<TModel, TInput, TDistance>
        where TModel : BaseKNearestNeighbors<TModel, TInput, TDistance>
    {
        private int k = 5;
        private TInput[] inputs;
        private int[] outputs;
        private TDistance distance;

        private object cancellationToken;

        protected BaseKNearestNeighbors()
        {
            // Initialize cancellation token using Unity's Reflection (dynamic type)
            var type = typeof(System.Threading.CancellationToken);
            cancellationToken = Activator.CreateInstance(type);
        }

        // Distance Property
        public TDistance Distance
        {
            get => distance;
            set => distance = value;
        }

        // K Property
        public int K
        {
            get => k;
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        "The value for k should be greater than zero and less than the total number of input points.");
                }
                k = value;
            }
        }

        // Token Property (Reflection)
        public object Token
        {
            get => cancellationToken;
            set
            {
                var tokenType = value?.GetType();
                if (tokenType?.Name == "CancellationToken")
                {
                    cancellationToken = value;
                }
                else
                {
                    throw new InvalidOperationException("Invalid token type.");
                }
            }
        }

        public TInput[] Inputs
        {
            get => inputs;
            protected set => inputs = value;
        }

        public int[] Outputs
        {
            get => outputs;
            protected set => outputs = value;
        }

        public abstract TInput[] GetNearestNeighbors(TInput input, out int[] labels);

        public abstract TModel Learn(TInput[] x, int[] y, double[] weights = null);

        // Reflective Argument Checker
        internal static void CheckArgs(int k, TInput[] inputs, int[] outputs, object distance, double[] weights)
        {
            if (weights != null)
            {
                Debug.LogError("Weights are not supported yet. Please request this feature if needed.");
                throw new NotSupportedException("Weights are not supported yet.");
            }

            if (inputs == null) throw new ArgumentNullException(nameof(inputs));
            if (outputs == null) throw new ArgumentNullException(nameof(outputs));
            if (inputs.Length != outputs.Length)
                throw new InvalidOperationException("Input and output length mismatch.");
            if (k <= 0 || k > inputs.Length)
                throw new ArgumentOutOfRangeException(nameof(k), "K value is out of range.");
            if (distance == null) throw new ArgumentNullException(nameof(distance));
        }

        internal int GetNumberOfInputs(TInput[] x)
        {
            if (x == null || x.Length == 0) return 0;

            var firstElement = x[0] as IList;
            if (firstElement == null) return 0;

            int length = firstElement.Count;
            for (int i = 0; i < x.Length; i++)
            {
                if (!(x[i] is IList list) || list.Count != length)
                {
                    return 0;
                }
            }

            return length;
        }

        public virtual double Score(TInput input, int classIndex)
        {
            var scores = Scores(input);
            return scores[classIndex];
        }

        public virtual double[] Scores(TInput input)
        {
            // Example: Score logic could use reflection to dynamically invoke methods.
            var scoreMethod = GetType().GetMethod("ComputeScore", BindingFlags.NonPublic | BindingFlags.Instance);
            return (double[])scoreMethod?.Invoke(this, new object[] { input }) ?? Array.Empty<double>();
        }
    }
}
