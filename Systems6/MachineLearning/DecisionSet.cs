using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Decision rule set for decision trees.
    /// </summary>
    public class DecisionSet : IEnumerable<DecisionRule>
    {
        private HashSet<DecisionRule> rules;

        public int NumberOfClasses { get; set; }
        public int NumberOfOutputs { get; set; }

        /// <summary>
        ///   Initializes a new instance of the <see cref="DecisionSet"/> class.
        /// </summary>
        public DecisionSet()
        {
            rules = new HashSet<DecisionRule>();
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="DecisionSet"/> class with a set of rules.
        /// </summary>
        /// <param name="rules">A collection of decision rules.</param>
        public DecisionSet(IEnumerable<DecisionRule> rules)
        {
            this.rules = new HashSet<DecisionRule>(rules);
        }

        /// <summary>
        ///   Creates a <see cref="DecisionSet"/> from a decision tree.
        /// </summary>
        /// <param name="tree">The decision tree.</param>
        /// <returns>A <see cref="DecisionSet"/> equivalent to the decision tree.</returns>
        public static DecisionSet FromDecisionTree(DecisionTree tree)
        {
            var ruleSet = new List<DecisionRule>();
            foreach (var node in tree)
            {
                if (node.IsLeaf && !node.IsRoot && node.Output.HasValue)
                {
                    ruleSet.Add(DecisionRule.FromNode(node));
                }
            }

            return new DecisionSet(ruleSet)
            {
                NumberOfClasses = tree.NumberOfClasses,
                NumberOfOutputs = tree.NumberOfOutputs
            };
        }

        /// <summary>
        ///   Adds a new decision rule to the set.
        /// </summary>
        /// <param name="rule">The rule to be added.</param>
        public void Add(DecisionRule rule)
        {
            rules.Add(rule);
        }

        /// <summary>
        ///   Adds multiple decision rules to the set.
        /// </summary>
        /// <param name="rulesToAdd">The rules to add.</param>
        public void AddRange(IEnumerable<DecisionRule> rulesToAdd)
        {
            foreach (var rule in rulesToAdd)
            {
                rules.Add(rule);
            }
        }

        /// <summary>
        ///   Removes a decision rule from the set.
        /// </summary>
        /// <param name="rule">The rule to remove.</param>
        /// <returns>True if the rule was successfully removed, otherwise false.</returns>
        public bool Remove(DecisionRule rule)
        {
            return rules.Remove(rule);
        }

        /// <summary>
        ///   Clears all rules from the set.
        /// </summary>
        public void Clear()
        {
            rules.Clear();
        }

        /// <summary>
        ///   Gets the number of rules in the set.
        /// </summary>
        public int Count => rules.Count;

        /// <summary>
        ///   Computes the output for a given input vector.
        /// </summary>
        /// <param name="input">The input vector.</param>
        /// <returns>The computed output class.</returns>
        public int Decide(double[] input)
        {
            if (input.Any(double.IsNaN))
            {
                var outputCounts = new int[NumberOfClasses];
                foreach (var rule in rules)
                {
                    if (rule.Match(input))
                    {
                        outputCounts[(int)rule.Output]++;
                    }
                }
                return outputCounts.ToList().IndexOf(outputCounts.Max());
            }

            foreach (var rule in rules)
            {
                if (rule.Match(input))
                {
                    return (int)rule.Output;
                }
            }

            return -1;
        }

        /// <summary>
        ///   Converts the decision set to a string representation.
        /// </summary>
        /// <returns>A string representation of the decision set.</returns>
        public override string ToString()
        {
            return ToString(CultureInfo.CurrentCulture);
        }

        /// <summary>
        ///   Converts the decision set to a string representation with a specific culture.
        /// </summary>
        /// <param name="culture">The culture info to use for formatting.</param>
        /// <returns>A string representation of the decision set.</returns>
        public string ToString(CultureInfo culture)
        {
            var rulesArray = rules.ToArray();
            Array.Sort(rulesArray);

            var builder = new StringBuilder();
            foreach (var rule in rulesArray)
            {
                builder.AppendLine(rule.ToString(culture));
            }

            return builder.ToString();
        }

        /// <summary>
        ///   Gets an enumerator to iterate through the decision rules.
        /// </summary>
        public IEnumerator<DecisionRule> GetEnumerator()
        {
            return rules.GetEnumerator();
        }

        /// <summary>
        ///   Gets an enumerator to iterate through the decision rules.
        /// </summary>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return rules.GetEnumerator();
        }
    }
}
