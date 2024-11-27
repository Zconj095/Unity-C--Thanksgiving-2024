using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Represents a rulebase, a collection of fuzzy rules for a Fuzzy Inference System.
    /// </summary>
    public class Rulebase
    {
        private readonly Dictionary<string, object> rules;

        /// <summary>
        /// Initializes a new instance of the <see cref="Rulebase"/> class.
        /// </summary>
        public Rulebase()
        {
            rules = new Dictionary<string, object>(20);
        }

        /// <summary>
        /// Adds a fuzzy rule to the rulebase.
        /// </summary>
        /// <param name="rule">A fuzzy <see cref="Rule"/> to add to the rulebase.</param>
        /// <exception cref="ArgumentException">If the rule name already exists in the rulebase.</exception>
        public void AddRule(object rule)
        {
            PropertyInfo nameProperty = rule.GetType().GetProperty("Name");
            if (nameProperty == null)
                throw new InvalidOperationException("The rule object is missing a 'Name' property.");

            string ruleName = (string)nameProperty.GetValue(rule);

            if (rules.ContainsKey(ruleName))
                throw new ArgumentException("The fuzzy rule name already exists in the rulebase.");

            rules.Add(ruleName, rule);
        }

        /// <summary>
        /// Removes all fuzzy rules from the rulebase.
        /// </summary>
        public void ClearRules()
        {
            rules.Clear();
        }

        /// <summary>
        /// Returns an existing fuzzy rule from the rulebase.
        /// </summary>
        /// <param name="ruleName">Name of the fuzzy <see cref="Rule"/> to retrieve.</param>
        /// <returns>Reference to the specified <see cref="Rule"/>.</returns>
        /// <exception cref="KeyNotFoundException">If the rule is not found in the rulebase.</exception>
        public object GetRule(string ruleName)
        {
            if (!rules.TryGetValue(ruleName, out var rule))
                throw new KeyNotFoundException($"Rule '{ruleName}' not found in the rulebase.");

            return rule;
        }

        /// <summary>
        /// Gets all the rules in the rulebase.
        /// </summary>
        /// <returns>An array containing all rules in the rulebase.</returns>
        public object[] GetRules()
        {
            return new List<object>(rules.Values).ToArray();
        }
    }
}
