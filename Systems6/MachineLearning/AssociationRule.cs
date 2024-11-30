using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EdgeLoreMachineLearning.Rules
{
    /// <summary>
    /// Association rule for itemset mining in Unity.
    /// </summary>
    /// <typeparam name="T">The type of items in the rule.</typeparam>
    [Serializable]
    public class AssociationRule3<T>
    {
        /// <summary>
        /// Gets or sets the antecedent of the rule (the items that trigger this rule).
        /// </summary>
        public HashSet<T> X { get; set; }

        /// <summary>
        /// Gets or sets the consequent of the rule (the items likely to appear if X is present).
        /// </summary>
        public HashSet<T> Y { get; set; }

        /// <summary>
        /// Gets or sets the support value of this rule (frequency of occurrence in the dataset).
        /// </summary>
        public double Support { get; set; }

        /// <summary>
        /// Gets or sets the confidence of this rule (likelihood that Y is present when X is present).
        /// </summary>
        public double Confidence { get; set; }

        /// <summary>
        /// Determines whether this rule applies to a given input set.
        /// </summary>
        /// <param name="input">The input set to evaluate.</param>
        /// <returns>True if the rule applies; otherwise, false.</returns>
        public bool Matches(HashSet<T> input)
        {
            return X.IsSubsetOf(input);
        }

        /// <summary>
        /// Determines whether this rule applies to a given input array.
        /// </summary>
        /// <param name="input">The input array to evaluate.</param>
        /// <returns>True if the rule applies; otherwise, false.</returns>
        public bool Matches(T[] input)
        {
            return Matches(new HashSet<T>(input));
        }

        /// <summary>
        /// Returns a string representation of this rule.
        /// </summary>
        /// <returns>A string that describes this rule.</returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append(string.Join(", ", X));
            sb.Append(" -> ");
            sb.Append(string.Join(", ", Y));
            sb.AppendFormat("; support: {0:0.##}, confidence: {1:0.##}", Support, Confidence);
            return sb.ToString();
        }
    }
}
