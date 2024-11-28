using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Decision Rule.
    /// </summary>
    [Serializable]
    public class DecisionRule : ICloneable, IEnumerable<Antecedent>, IEquatable<DecisionRule>, IComparable<DecisionRule>
    {
        private List<Antecedent> antecedents;
        private double output;

        /// <summary>
        ///   Gets the antecedents (conditions) that must be fulfilled for this rule to apply.
        /// </summary>
        public IList<Antecedent> Antecedents => antecedents;

        /// <summary>
        ///   Gets or sets the output of this decision rule, given when all antecedents are met.
        /// </summary>
        public double Output
        {
            get => output;
            set => output = value;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="DecisionRule"/> class.
        /// </summary>
        /// <param name="output">The output value, given after all antecedents are met.</param>
        /// <param name="antecedents">The conditions that lead to the output.</param>
        public DecisionRule(double output, params Antecedent[] antecedents)
        {
            this.antecedents = new List<Antecedent>(antecedents);
            this.output = output;
        }

        /// <summary>
        ///   Creates a decision rule from a decision node.
        /// </summary>
        /// <param name="node">The decision node to construct the rule from.</param>
        /// <returns>A new <see cref="DecisionRule"/>.</returns>
        public static DecisionRule FromNode(DecisionNode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node), "Node cannot be null.");

            if (!node.IsLeaf)
                throw new InvalidOperationException("Cannot create a rule from a non-leaf node.");

            var antecedents = new List<Antecedent>();

            // Traverse from the node to the root to extract antecedents
            while (node != null && !node.IsRoot)
            {
                antecedents.Insert(0, new Antecedent(node.AttributeIndex, node.Comparison, node.Value ?? double.NaN));
                node = node.Parent;
            }

            return new DecisionRule(node.Output.Value, antecedents.ToArray());
        }

        /// <summary>
        ///   Checks whether this rule matches a given input.
        /// </summary>
        /// <param name="input">The input vector.</param>
        /// <returns>True if all antecedents are matched; otherwise, false.</returns>
        public bool Match(double[] input)
        {
            return antecedents.All(a => a.Match(input));
        }

        /// <summary>
        ///   Clones this decision rule.
        /// </summary>
        /// <returns>A new instance of this rule.</returns>
        public object Clone()
        {
            return new DecisionRule(output, antecedents.ToArray());
        }

        /// <summary>
        ///   Returns a string representation of this rule.
        /// </summary>
        public override string ToString()
        {
            var sb = new StringBuilder();
            for (int i = 0; i < antecedents.Count; i++)
            {
                sb.Append($"({antecedents[i]})");
                if (i < antecedents.Count - 1)
                    sb.Append(" && ");
            }
            return $"{sb} => {Output}";
        }

        /// <summary>
        ///   Returns a culture-specific string representation of this rule.
        /// </summary>
        /// <param name="culture">The culture to use for formatting.</param>
        /// <returns>A string representation of this rule.</returns>
        public string ToString(CultureInfo culture)
        {
            var sb = new StringBuilder();
            for (int i = 0; i < antecedents.Count; i++)
            {
                // Use Antecedent.ToString() as Antecedent doesn't provide culture-specific formatting
                sb.Append($"({antecedents[i].ToString()})");
                if (i < antecedents.Count - 1)
                    sb.Append(" && ");
            }
            return $"{sb} => {Output.ToString(culture)}";
        }

        /// <summary>
        ///   Determines if this rule is equal to another rule.
        /// </summary>
        public bool Equals(DecisionRule other)
        {
            if (other == null) return false;
            return Output == other.Output && antecedents.SequenceEqual(other.antecedents);
        }

        /// <summary>
        ///   Returns a hash code for this rule.
        /// </summary>
        public override int GetHashCode()
        {
            int hash = Output.GetHashCode();
            foreach (var antecedent in antecedents)
            {
                hash = (hash * 397) ^ antecedent.GetHashCode();
            }
            return hash;
        }

        /// <summary>
        ///   Compares this rule to another rule.
        /// </summary>
        public int CompareTo(DecisionRule other)
        {
            if (other == null) return 1;
            int outputComparison = Output.CompareTo(other.Output);
            return outputComparison != 0 ? outputComparison : antecedents.Count.CompareTo(other.antecedents.Count);
        }

        /// <summary>
        ///   Returns an enumerator that iterates through the antecedents.
        /// </summary>
        public IEnumerator<Antecedent> GetEnumerator()
        {
            return antecedents.GetEnumerator();
        }

        /// <summary>
        ///   Returns an enumerator that iterates through the antecedents.
        /// </summary>
        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
