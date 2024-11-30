using System;
using System.Collections.Generic;
using UnityEngine;

namespace EdgeLoreMachineLearning.Rules
{
    /// <summary>
    /// Association rule matcher for classification and transformation in Unity.
    /// </summary>
    /// <typeparam name="T">The type of items in the rules.</typeparam>
    [Serializable]
    public class AssociationRule2Matcher<T>
    {
        private int items;
        private AssociationRule2<T>[] rules;
        private double threshold;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssociationRule2Matcher{T}"/> class.
        /// </summary>
        /// <param name="items">The number of distinct items in the dataset.</param>
        /// <param name="rules">The association rules for classification.</param>
        public AssociationRule2Matcher(int items, AssociationRule2<T>[] rules)
        {
            this.items = items;
            this.rules = rules;
        }

        /// <summary>
        /// Gets the number of items seen by the model during training.
        /// </summary>
        public int NumberOfInputs => items;

        /// <summary>
        /// Gets the number of rules in the model.
        /// </summary>
        public int NumberOfOutputs => rules.Length;

        /// <summary>
        /// Gets or sets the association rules in this model.
        /// </summary>
        public AssociationRule2<T>[] Rules
        {
            get => rules;
            set => rules = value;
        }

        /// <summary>
        /// Gets or sets the confidence threshold for rule application.
        /// </summary>
        public double Threshold
        {
            get => threshold;
            set => threshold = value;
        }

        /// <summary>
        /// Computes scores and corresponding decisions for a given input set.
        /// </summary>
        /// <param name="input">The input set to evaluate.</param>
        /// <param name="decision">The decisions based on the scores.</param>
        /// <returns>An array of scores representing the association strength.</returns>
        public double[] Scores(HashSet<T> input, ref List<HashSet<T>> decision)
        {
            var matches = new Dictionary<HashSet<T>, double>(new SetComparer<T>());
            foreach (var rule in rules)
            {
                if (rule.Matches(input) && !rule.Y.IsSubsetOf(input) && rule.Confidence > threshold)
                {
                    if (!matches.ContainsKey(rule.Y))
                        matches[rule.Y] = rule.Confidence;
                    else
                        matches[rule.Y] += rule.Confidence;
                }
            }

            decision = new List<HashSet<T>>(matches.Keys);
            var scores = new List<double>(matches.Values);

            // Sort by score descending
            var sortedScores = new double[scores.Count];
            scores.CopyTo(sortedScores);
            Array.Sort(sortedScores, decision.ToArray());
            Array.Reverse(sortedScores);

            return sortedScores;
        }

        /// <summary>
        /// Predicts a class decision for a given input set.
        /// </summary>
        /// <param name="input">The input set to classify.</param>
        /// <returns>A list of class labels representing the decision.</returns>
        public List<HashSet<T>> Decide(HashSet<T> input)
        {
            List<HashSet<T>> decision = null;
            Scores(input, ref decision);
            return decision;
        }

        /// <summary>
        /// Computes decisions for multiple input sets.
        /// </summary>
        /// <param name="inputs">An array of input sets to classify.</param>
        /// <returns>A list of decisions for each input set.</returns>
        public List<HashSet<T>>[] Decide(HashSet<T>[] inputs)
        {
            var results = new List<HashSet<T>>[inputs.Length];
            for (int i = 0; i < inputs.Length; i++)
            {
                results[i] = Decide(inputs[i]);
            }
            return results;
        }

        /// <summary>
        /// Applies the transformation to an input, producing a list of decisions.
        /// </summary>
        /// <param name="input">The input set to transform.</param>
        /// <returns>The transformed decision output.</returns>
        public List<HashSet<T>> Transform(HashSet<T> input)
        {
            return Decide(input);
        }

        /// <summary>
        /// Applies the transformation to multiple inputs, producing a list of decisions.
        /// </summary>
        /// <param name="inputs">The input sets to transform.</param>
        /// <returns>The transformed decisions for each input set.</returns>
        public List<HashSet<T>>[] Transform(HashSet<T>[] inputs)
        {
            return Decide(inputs);
        }
    }

    /// <summary>
    /// Represents an association rule.
    /// </summary>
    /// <typeparam name="T">The type of items in the rule.</typeparam>
    [Serializable]
    public class AssociationRule2<T>
    {
        public HashSet<T> X { get; set; }
        public HashSet<T> Y { get; set; }
        public double Confidence { get; set; }
        public double Support { get; set; }

        /// <summary>
        /// Checks if the rule matches the given input set.
        /// </summary>
        /// <param name="input">The input set to evaluate.</param>
        /// <returns>True if the rule matches; otherwise, false.</returns>
        public bool Matches(HashSet<T> input)
        {
            return X.IsSubsetOf(input);
        }
    }

    /// <summary>
    /// Custom comparer for HashSet to enable dictionary usage.
    /// </summary>
    /// <typeparam name="T">The type of items in the set.</typeparam>
    public class SetComparer<T> : IEqualityComparer<HashSet<T>>
    {
        public bool Equals(HashSet<T> x, HashSet<T> y)
        {
            return x.SetEquals(y);
        }

        public int GetHashCode(HashSet<T> obj)
        {
            int hash = 0;
            foreach (var item in obj)
                hash ^= item.GetHashCode();
            return hash;
        }
    }
}
