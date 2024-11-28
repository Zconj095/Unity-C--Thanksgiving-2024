using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Base class for tree-inducing (learning) algorithms in Unity.
    /// </summary>
    public class DecisionTreeLearningBase : IEnumerable<DecisionVariable>
    {
        private DecisionTree tree;
        private List<DecisionVariable> attributes;
        private int maxHeight = 0; // No limit by default
        private int maxVariables = 0; // No limit by default
        private int join = 1;

        private int[] attributeUsageCount;

        /// <summary>
        ///   Gets or sets the maximum allowed height of the decision tree. Default is 0 (no limit).
        /// </summary>
        public int MaxHeight
        {
            get => maxHeight;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Height must be non-negative.");
                maxHeight = value;
            }
        }

        /// <summary>
        ///   Gets or sets the maximum number of attributes that can be used in the tree. Default is 0 (no limit).
        /// </summary>
        public int MaxVariables
        {
            get => maxVariables;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "MaxVariables must be non-negative.");
                maxVariables = value;
            }
        }

        /// <summary>
        ///   Gets or sets the list of attributes for the decision tree.
        /// </summary>
        public IList<DecisionVariable> Attributes
        {
            get => attributes;
            set => attributes = value.ToList();
        }

        /// <summary>
        ///   Gets or sets how many times an attribute can be used in a decision path.
        ///   Default is 1 (an attribute can be used only once per path).
        /// </summary>
        public int Join
        {
            get => join;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Join must be non-negative.");
                join = value;
            }
        }

        /// <summary>
        ///   Gets or sets the decision tree being learned.
        /// </summary>
        public DecisionTree Model
        {
            get => tree;
            set
            {
                tree = value;
                InitializeAttributeUsageCount();
            }
        }

        /// <summary>
        ///   Initializes a new instance of the `DecisionTreeLearningBase` class.
        /// </summary>
        public DecisionTreeLearningBase()
        {
            attributes = new List<DecisionVariable>();
        }

        /// <summary>
        ///   Initializes a new instance of the `DecisionTreeLearningBase` class with a given set of attributes.
        /// </summary>
        /// <param name="attributes">The attributes to be used in the decision tree.</param>
        public DecisionTreeLearningBase(IEnumerable<DecisionVariable> attributes)
        {
            this.attributes = attributes.ToList();
        }

        /// <summary>
        ///   Adds an attribute to the decision tree.
        /// </summary>
        public void Add(DecisionVariable variable)
        {
            attributes.Add(variable);
        }

        /// <summary>
        ///   Initializes the usage count for attributes.
        /// </summary>
        private void InitializeAttributeUsageCount()
        {
            if (tree == null || tree.NumberOfInputs <= 0)
                throw new InvalidOperationException("Tree must be initialized with valid attributes.");

            attributeUsageCount = new int[tree.NumberOfInputs];
        }

        /// <summary>
        ///   Enumerates over the attributes of the decision tree.
        /// </summary>
        public IEnumerator<DecisionVariable> GetEnumerator()
        {
            return attributes.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        /// <summary>
        ///   Computes the split information measure.
        /// </summary>
        /// <param name="samples">The total number of samples.</param>
        /// <param name="partitions">The partitions of the dataset.</param>
        /// <param name="missing">An optional partition for missing values.</param>
        /// <returns>The split information for the partitions.</returns>
        public static double SplitInformation(int samples, IList<int>[] partitions, List<int> missing = null)
        {
            double info = 0;

            foreach (var partition in partitions)
            {
                if (partition == null)
                    continue;

                double p = (double)partition.Count / samples;
                if (p > 0)
                    info -= p * Math.Log(p, 2);
            }

            if (missing != null)
            {
                double p = (double)missing.Count / samples;
                if (p > 0)
                    info -= p * Math.Log(p, 2);
            }

            return info;
        }
    }
}
