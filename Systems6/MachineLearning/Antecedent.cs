using System;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Antecedent expression for Decision Rules.
    /// </summary>
    public struct Antecedent : IEquatable<Antecedent>
    {
        private int index;
        private ComparisonKind comparison;
        private double value;

        /// <summary>
        ///   Gets the index of the variable used as the
        ///   left-hand side term of this expression.
        /// </summary>
        public int Index => index;

        /// <summary>
        ///   Gets the comparison being made between the variable
        ///   value at <see cref="Index"/> and <see cref="Value"/>.
        /// </summary>
        public ComparisonKind Comparison => comparison;

        /// <summary>
        ///   Gets the right-hand side of this expression.
        /// </summary>
        public double Value => value;

        /// <summary>
        ///   Creates a new instance of the <see cref="Antecedent"/> struct.
        /// </summary>
        /// <param name="index">The variable index.</param>
        /// <param name="comparison">The comparison to be made using the value at 
        ///   <paramref name="index"/> and <paramref name="value"/>.</param>
        /// <param name="value">The value to be compared against.</param>
        public Antecedent(int index, ComparisonKind comparison, double value)
        {
            this.index = index;
            this.comparison = comparison;
            this.value = value;
        }

        /// <summary>
        ///   Checks if this antecedent applies to a given input.
        /// </summary>
        /// <param name="input">An input vector.</param>
        /// <returns>True if the input element at position <see cref="Index"/>
        ///   compares to <see cref="Value"/> using <see cref="Comparison"/>; false otherwise.</returns>
        public bool Match(double[] input)
        {
            if (index < 0 || index >= input.Length)
                throw new ArgumentOutOfRangeException(nameof(index), "Index is out of bounds for the input array.");

            double x = input[index];

            // Handle missing values
            if (double.IsNaN(x))
                return true;

            // Perform the comparison
            return comparison switch
            {
                ComparisonKind.Equal => x == value,
                ComparisonKind.NotEqual => x != value,
                ComparisonKind.GreaterThan => x > value,
                ComparisonKind.GreaterThanOrEqual => x >= value,
                ComparisonKind.LessThan => x < value,
                ComparisonKind.LessThanOrEqual => x <= value,
                _ => throw new InvalidOperationException("Unsupported comparison type.")
            };
        }

        /// <summary>
        ///   Determines whether the specified <see cref="Antecedent"/>
        ///   is equal to this instance.
        /// </summary>
        public bool Equals(Antecedent other)
        {
            return Index == other.Index &&
                   Comparison == other.Comparison &&
                   Value.Equals(other.Value);
        }

        /// <summary>
        ///   Determines whether the specified <see cref="object"/>
        ///   is equal to this instance.
        /// </summary>
        public override bool Equals(object obj)
        {
            return obj is Antecedent other && Equals(other);
        }

        /// <summary>
        ///   Returns a hash code for this instance.
        /// </summary>
        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 31 + index.GetHashCode();
                hash = hash * 31 + comparison.GetHashCode();
                hash = hash * 31 + value.GetHashCode();
                return hash;
            }
        }

        /// <summary>
        ///   Returns a string that represents this instance.
        /// </summary>
        public override string ToString()
        {
            string comparisonOperator = comparison switch
            {
                ComparisonKind.Equal => "==",
                ComparisonKind.NotEqual => "!=",
                ComparisonKind.GreaterThan => ">",
                ComparisonKind.GreaterThanOrEqual => ">=",
                ComparisonKind.LessThan => "<",
                ComparisonKind.LessThanOrEqual => "<=",
                _ => "?"
            };
            return $"x[{Index}] {comparisonOperator} {Value}";
        }

        /// <summary>
        ///   Implements the equality operator.
        /// </summary>
        public static bool operator ==(Antecedent a, Antecedent b) => a.Equals(b);

        /// <summary>
        ///   Implements the inequality operator.
        /// </summary>
        public static bool operator !=(Antecedent a, Antecedent b) => !a.Equals(b);
    }
}
