using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Attribute category.
    /// </summary>
    public enum DecisionVariableKind
    {
        /// <summary>
        ///   Attribute is discrete-valued.
        /// </summary>
        Discrete,

        /// <summary>
        ///   Attribute is continuous-valued.
        /// </summary>
        Continuous
    }

    /// <summary>
    ///   Decision attribute for use in machine learning models.
    /// </summary>
    [Serializable]
    public class DecisionVariable
    {
        /// <summary>
        ///   The name of the variable.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///   The type of the variable (discrete or continuous).
        /// </summary>
        public DecisionVariableKind Nature { get; set; }

        /// <summary>
        ///   The valid range of the variable's values.
        /// </summary>
        public DoubleRange2 Range { get; set; }

        /// <summary>
        ///   Creates a continuous DecisionVariable.
        /// </summary>
        public DecisionVariable(string name, DoubleRange2 range)
        {
            Name = name;
            Nature = DecisionVariableKind.Continuous;
            Range = range;
        }

        /// <summary>
        ///   Creates a discrete DecisionVariable.
        /// </summary>
        public DecisionVariable(string name, int symbols)
            : this(name, new IntRange2(0, symbols - 1))
        {
        }

        /// <summary>
        ///   Creates a discrete DecisionVariable with a specific range.
        /// </summary>
        public DecisionVariable(string name, IntRange2 range)
        {
            Name = name;
            Nature = DecisionVariableKind.Discrete;
            Range = new DoubleRange2(range.Min, range.Max);
        }

        /// <summary>
        ///   Creates a continuous DecisionVariable with default range [0, 1].
        /// </summary>
        public static DecisionVariable Continuous(string name)
        {
            return new DecisionVariable(name, new DoubleRange2(0, 1));
        }

        /// <summary>
        ///   Creates a discrete DecisionVariable from symbols.
        /// </summary>
        public static DecisionVariable Discrete(string name, int symbols)
        {
            return new DecisionVariable(name, symbols);
        }

        /// <summary>
        ///   Returns a string representation of the DecisionVariable.
        /// </summary>
        public override string ToString()
        {
            return $"{Name} : {Nature} ({Range})";
        }

        /// <summary>
        ///   Generates DecisionVariables from data.
        /// </summary>
        public static DecisionVariable[] FromData(double[][] inputs)
        {
            if (inputs == null || inputs.Length == 0)
                throw new ArgumentException("Input data cannot be null or empty.");

            int cols = inputs[0].Length;
            DecisionVariable[] variables = new DecisionVariable[cols];

            for (int i = 0; i < cols; i++)
            {
                var columnData = inputs.Select(row => row[i]).Where(value => !double.IsNaN(value)).ToArray();
                DoubleRange2 range = columnData.GetRange();
                variables[i] = new DecisionVariable($"Variable_{i}", range);
            }

            Debug.Log($"Created {variables.Length} decision variables from data.");
            return variables;
        }
    }

    /// <summary>
    ///   Represents a range of double values.
    /// </summary>
    public class DoubleRange2
    {
        public double Min { get; set; }
        public double Max { get; set; }

        public DoubleRange2(double min, double max)
        {
            Min = min;
            Max = max;
        }

        public override string ToString()
        {
            return $"[{Min}, {Max}]";
        }
    }

    /// <summary>
    ///   Represents a range of integer values.
    /// </summary>
    public class IntRange2
    {
        public int Min { get; set; }
        public int Max { get; set; }

        public IntRange2(int min, int max)
        {
            Min = min;
            Max = max;
        }

        public override string ToString()
        {
            return $"[{Min}, {Max}]";
        }
    }

    /// <summary>
    ///   Extensions for arrays.
    /// </summary>
    public static class ArrayExtensions2
    {
        /// <summary>
        ///   Computes the range of a sequence of doubles.
        /// </summary>
        public static DoubleRange2 GetRange(this IEnumerable<double> values)
        {
            if (values == null || !values.Any())
                throw new ArgumentException("Values cannot be null or empty.");

            return new DoubleRange2(values.Min(), values.Max());
        }
    }
}
