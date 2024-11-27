using System;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Represents a fuzzy set, where membership can be a value in the range [0..1].
    /// </summary>
    public class FuzzySet
    {
        // Name of the fuzzy set
        private readonly string name;
        // Membership function that defines the shape of the fuzzy set
        private readonly object function;

        /// <summary>
        /// Name of the fuzzy set.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// The leftmost x value of the fuzzy set's membership function.
        /// </summary>
        public float LeftLimit
        {
            get
            {
                // Use reflection to get the LeftLimit property
                PropertyInfo leftLimitProperty = function.GetType().GetProperty("LeftLimit", BindingFlags.Instance | BindingFlags.Public);
                if (leftLimitProperty == null)
                    throw new InvalidOperationException("Membership function does not have a 'LeftLimit' property.");

                return (float)leftLimitProperty.GetValue(function);
            }
        }

        /// <summary>
        /// The rightmost x value of the fuzzy set's membership function.
        /// </summary>
        public float RightLimit
        {
            get
            {
                // Use reflection to get the RightLimit property
                PropertyInfo rightLimitProperty = function.GetType().GetProperty("RightLimit", BindingFlags.Instance | BindingFlags.Public);
                if (rightLimitProperty == null)
                    throw new InvalidOperationException("Membership function does not have a 'RightLimit' property.");

                return (float)rightLimitProperty.GetValue(function);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FuzzySet"/> class.
        /// </summary>
        /// <param name="name">Name of the fuzzy set.</param>
        /// <param name="function">Membership function that defines the shape of the fuzzy set.</param>
        public FuzzySet(string name, object function)
        {
            this.name = name;
            this.function = function;
        }

        /// <summary>
        /// Calculate membership of a given value to the fuzzy set.
        /// </summary>
        /// <param name="x">Value for which membership needs to be calculated.</param>
        /// <returns>Degree of membership [0..1] of the value to the fuzzy set.</returns>
        public float GetMembership(float x)
        {
            // Use reflection to call the GetMembership method
            MethodInfo getMembershipMethod = function.GetType().GetMethod("GetMembership", BindingFlags.Instance | BindingFlags.Public);
            if (getMembershipMethod == null)
                throw new InvalidOperationException("Membership function does not have a 'GetMembership' method.");

            return (float)getMembershipMethod.Invoke(function, new object[] { x });
        }
    }
}
