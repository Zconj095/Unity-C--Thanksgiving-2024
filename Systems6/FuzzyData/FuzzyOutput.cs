using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Represents the output of a Fuzzy Inference System.
    /// </summary>
    public class FuzzyOutput
    {
        /// <summary>
        /// Inner class to store the pair fuzzy label / firing strength of a fuzzy output.
        /// </summary>
        public class OutputConstraint
        {
            private readonly string label;
            private readonly float firingStrength;

            /// <summary>
            /// Initializes a new instance of the <see cref="OutputConstraint"/> class.
            /// </summary>
            /// <param name="label">The output label of a rule.</param>
            /// <param name="firingStrength">The firing strength of the rule.</param>
            internal OutputConstraint(string label, float firingStrength)
            {
                this.label = label;
                this.firingStrength = firingStrength;
            }

            /// <summary>
            /// The output label of the rule.
            /// </summary>
            public string Label => label;

            /// <summary>
            /// The firing strength of the rule.
            /// </summary>
            public float FiringStrength => firingStrength;
        }

        private readonly List<OutputConstraint> outputList;
        private readonly object outputVar;

        /// <summary>
        /// A list of <see cref="OutputConstraint"/> representing the Fuzzy Inference System's output.
        /// </summary>
        public List<OutputConstraint> OutputList => outputList;

        /// <summary>
        /// Gets the output variable of the Fuzzy Inference System.
        /// </summary>
        public object OutputVariable => outputVar;

        /// <summary>
        /// Initializes a new instance of the <see cref="FuzzyOutput"/> class.
        /// </summary>
        /// <param name="outputVar">The output variable of the Fuzzy Inference System.</param>
        internal FuzzyOutput(object outputVar)
        {
            this.outputList = new List<OutputConstraint>(20);
            this.outputVar = outputVar;
        }

        /// <summary>
        /// Adds an output to the Fuzzy Output.
        /// </summary>
        /// <param name="labelName">The name of the label representing a rule's output.</param>
        /// <param name="firingStrength">The firing strength [0..1] of the rule.</param>
        internal void AddOutput(string labelName, float firingStrength)
        {
            // Use reflection to ensure the label exists in the linguistic variable
            MethodInfo getLabelMethod = outputVar.GetType().GetMethod("GetLabel", BindingFlags.Instance | BindingFlags.Public);
            if (getLabelMethod == null)
                throw new InvalidOperationException("GetLabel method not found in LinguisticVariable.");

            getLabelMethod.Invoke(outputVar, new object[] { labelName });

            // Add the label and its firing strength
            this.outputList.Add(new OutputConstraint(labelName, firingStrength));
        }

        /// <summary>
        /// Clears all outputs from the Fuzzy Output.
        /// </summary>
        internal void ClearOutput()
        {
            this.outputList.Clear();
        }
    }
}
