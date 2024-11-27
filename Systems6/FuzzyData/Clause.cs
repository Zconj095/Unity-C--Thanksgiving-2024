using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Represents a fuzzy clause, a linguistic expression of the type "Variable IS Value".
    /// </summary>
    public class Clause
    {
        // The linguistic variable of the clause
        private readonly LinguisticVariable variable;

        // The label of the clause
        private readonly FuzzySet label;

        /// <summary>
        /// Gets the <see cref="LinguisticVariable"/> of the <see cref="Clause"/>.
        /// </summary>
        public LinguisticVariable Variable
        {
            get { return variable; }
        }

        /// <summary>
        /// Gets the <see cref="FuzzySet"/> of the <see cref="Clause"/>.
        /// </summary>
        public FuzzySet Label
        {
            get { return label; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Clause"/> class.
        /// </summary>
        /// <param name="variable">The linguistic variable of the clause.</param>
        /// <param name="label">The label of the linguistic variable, represented as a fuzzy set.</param>
        /// <exception cref="KeyNotFoundException">Thrown if the label is not found in the linguistic variable.</exception>
        public Clause(LinguisticVariable variable, FuzzySet label)
        {
            // Use reflection to ensure the label exists in the linguistic variable
            MethodInfo getLabelMethod = variable.GetType().GetMethod("GetLabel", BindingFlags.Instance | BindingFlags.Public);
            if (getLabelMethod == null)
                throw new InvalidOperationException("GetLabel method not found in LinguisticVariable.");

            getLabelMethod.Invoke(variable, new object[] { label.Name });

            // Initialize attributes
            this.label = label;
            this.variable = variable;
        }

        /// <summary>
        /// Evaluates the fuzzy clause.
        /// </summary>
        /// <returns>Degree of membership [0..1] of the clause.</returns>
        public float Evaluate()
        {
            // Use reflection to call the GetMembership method on the label
            MethodInfo getMembershipMethod = label.GetType().GetMethod("GetMembership", BindingFlags.Instance | BindingFlags.Public);
            if (getMembershipMethod == null)
                throw new InvalidOperationException("GetMembership method not found in FuzzySet.");

            return (float)getMembershipMethod.Invoke(label, new object[] { variable.NumericInput });
        }

        /// <summary>
        /// Returns the fuzzy clause in its linguistic representation.
        /// </summary>
        /// <returns>A string representing the fuzzy clause.</returns>
        public override string ToString()
        {
            return $"{variable.Name} IS {label.Name}";
        }
    }
}
