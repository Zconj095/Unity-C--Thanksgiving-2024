using System;
using System.Collections.Generic;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Represents a linguistic variable in a Fuzzy Inference System.
    /// </summary>
    public class LinguisticVariable
    {
        private readonly string name;
        private readonly float start;
        private readonly float end;
        private readonly Dictionary<string, object> labels;
        private float numericInput;

        /// <summary>
        /// Numerical value of the input of this linguistic variable.
        /// </summary>
        public float NumericInput
        {
            get => numericInput;
            set => numericInput = value;
        }

        /// <summary>
        /// Name of the linguistic variable.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Left limit of the valid variable range.
        /// </summary>
        public float Start => start;

        /// <summary>
        /// Right limit of the valid variable range.
        /// </summary>
        public float End => end;

        /// <summary>
        /// Initializes a new instance of the <see cref="LinguisticVariable"/> class.
        /// </summary>
        /// <param name="name">Name of the linguistic variable.</param>
        /// <param name="start">Left limit of the valid variable range.</param>
        /// <param name="end">Right limit of the valid variable range.</param>
        public LinguisticVariable(string name, float start, float end)
        {
            this.name = name;
            this.start = start;
            this.end = end;
            this.labels = new Dictionary<string, object>(10);
        }

        /// <summary>
        /// Adds a linguistic label to the variable.
        /// </summary>
        /// <param name="label">A <see cref="FuzzySet"/> that will be a linguistic label of the linguistic variable.</param>
        /// <exception cref="ArgumentException">If the label name already exists or the label limits are outside the variable range.</exception>
        public void AddLabel(object label)
        {
            Type labelType = label.GetType();
            PropertyInfo nameProperty = labelType.GetProperty("Name");
            PropertyInfo leftLimitProperty = labelType.GetProperty("LeftLimit");
            PropertyInfo rightLimitProperty = labelType.GetProperty("RightLimit");

            if (nameProperty == null || leftLimitProperty == null || rightLimitProperty == null)
                throw new InvalidOperationException("The label object is missing required properties.");

            string labelName = (string)nameProperty.GetValue(label);
            float leftLimit = (float)leftLimitProperty.GetValue(label);
            float rightLimit = (float)rightLimitProperty.GetValue(label);

            if (labels.ContainsKey(labelName))
                throw new ArgumentException("The linguistic label name already exists in the linguistic variable.");
            if (leftLimit < this.start)
                throw new ArgumentException("The left limit of the fuzzy set cannot be lower than the linguistic variable's starting point.");
            if (rightLimit > this.end)
                throw new ArgumentException("The right limit of the fuzzy set cannot be greater than the linguistic variable's ending point.");

            labels.Add(labelName, label);
        }

        /// <summary>
        /// Removes all the linguistic labels of the linguistic variable.
        /// </summary>
        public void ClearLabels()
        {
            labels.Clear();
        }

        /// <summary>
        /// Returns an existing label from the linguistic variable.
        /// </summary>
        /// <param name="labelName">Name of the label to retrieve.</param>
        /// <returns>Reference to named label (<see cref="FuzzySet"/>).</returns>
        /// <exception cref="KeyNotFoundException">If the label is not found in the linguistic variable.</exception>
        public object GetLabel(string labelName)
        {
            if (!labels.TryGetValue(labelName, out var label))
                throw new KeyNotFoundException($"Label '{labelName}' not found in the linguistic variable.");

            return label;
        }

        /// <summary>
        /// Calculates the membership of a given value to a given label.
        /// </summary>
        /// <param name="labelName">Label (fuzzy set) to evaluate value's membership.</param>
        /// <param name="value">Value for which label's membership will be calculated.</param>
        /// <returns>Degree of membership [0..1] of the value to the label (fuzzy set).</returns>
        /// <exception cref="KeyNotFoundException">If the label is not found in the linguistic variable.</exception>
        public float GetLabelMembership(string labelName, float value)
        {
            object label = GetLabel(labelName);
            MethodInfo getMembershipMethod = label.GetType().GetMethod("GetMembership");

            if (getMembershipMethod == null)
                throw new InvalidOperationException("The label object is missing the 'GetMembership' method.");

            return (float)getMembershipMethod.Invoke(label, new object[] { value });
        }
    }
}
