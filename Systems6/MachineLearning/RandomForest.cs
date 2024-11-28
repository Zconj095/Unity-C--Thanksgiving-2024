using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Random Forest implementation for Unity.
    /// </summary>
    public class RandomForest
    {
        private List<DecisionTree> trees;
        public int NumberOfOutputs { get; private set; }
        public int NumberOfInputs { get; private set; }
        public int NumberOfClasses { get; private set; }

        /// <summary>
        ///   Exposes the decision trees as a read-only property.
        /// </summary>
        public IEnumerable<DecisionTree> Trees => trees.AsReadOnly();

        /// <summary>
        ///   Initializes a new instance of the <see cref="RandomForest"/> class.
        /// </summary>
        public RandomForest()
        {
            trees = new List<DecisionTree>();
        }

        /// <summary>
        ///   Initializes a new random forest with a set of decision trees.
        /// </summary>
        /// <param name="trees">The decision trees to include in the forest.</param>
        public RandomForest(IEnumerable<DecisionTree> trees)
        {
            this.trees = new List<DecisionTree>(trees);
            if (this.trees.Count > 0)
            {
                InitializeParameters(this.trees[0]);
            }
        }

        /// <summary>
        ///   Initializes a new random forest with a specified number of trees and attributes.
        /// </summary>
        /// <param name="treeCount">The number of trees in the forest.</param>
        /// <param name="attributes">The attributes used by each tree.</param>
        /// <param name="classCount">The number of output classes.</param>
        public RandomForest(int treeCount, IList<DecisionVariable> attributes, int classCount)
        {
            trees = new List<DecisionTree>();
            for (int i = 0; i < treeCount; i++)
            {
                // Explicitly convert IList to List to resolve type mismatch
                trees.Add(new DecisionTree(attributes.ToList(), classCount));
            }
            InitializeParameters(trees[0]);
        }

        /// <summary>
        ///   Adds a decision tree to the forest.
        /// </summary>
        /// <param name="tree">The decision tree to add.</param>
        public void AddTree(DecisionTree tree)
        {
            if (trees.Count == 0)
            {
                InitializeParameters(tree);
            }
            ValidateTreeCompatibility(tree);
            trees.Add(tree);
        }

        /// <summary>
        ///   Computes a decision for the given input vector.
        /// </summary>
        /// <param name="input">The input vector.</param>
        /// <returns>The most common decision among all trees in the forest.</returns>
        public int Decide(double[] input)
        {
            var responses = new int[NumberOfClasses];

            foreach (var tree in trees)
            {
                int decision = tree.Decide(input);
                if (decision >= 0)
                {
                    responses[decision]++;
                }
            }

            return responses.ToList().IndexOf(responses.Max());
        }

        /// <summary>
        ///   Clears all trees in the forest.
        /// </summary>
        public void Clear()
        {
            trees.Clear();
        }

        /// <summary>
        ///   Gets the number of decision trees in the forest.
        /// </summary>
        public int TreeCount => trees.Count;

        private void InitializeParameters(DecisionTree sampleTree)
        {
            NumberOfInputs = sampleTree.NumberOfInputs;
            NumberOfOutputs = sampleTree.NumberOfOutputs;
            NumberOfClasses = sampleTree.NumberOfClasses;
        }

        private void ValidateTreeCompatibility(DecisionTree tree)
        {
            if (tree.NumberOfInputs != NumberOfInputs)
                throw new ArgumentException("Tree inputs do not match forest configuration.");
            if (tree.NumberOfOutputs != NumberOfOutputs)
                throw new ArgumentException("Tree outputs do not match forest configuration.");
            if (tree.NumberOfClasses != NumberOfClasses)
                throw new ArgumentException("Tree classes do not match forest configuration.");
        }
    }
}
