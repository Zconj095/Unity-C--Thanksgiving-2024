using System;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Maximum CoNorm, used to calculate the linguistic value of an OR operation.
    /// </summary>
    /// <remarks>
    /// <para>The maximum CoNorm uses a maximum operator to compute the OR
    /// among two fuzzy memberships.</para>
    /// </remarks>
    public class MaximumCoNorm : ICoNorm
    {
        /// <summary>
        /// Calculates the numerical result of the OR operation applied to
        /// two fuzzy membership values using reflection.
        /// </summary>
        /// <param name="membershipA">A fuzzy membership value, [0..1].</param>
        /// <param name="membershipB">A fuzzy membership value, [0..1].</param>
        /// <returns>The numerical result of the binary operation OR applied to <paramref name="membershipA"/>
        /// and <paramref name="membershipB"/>.</returns>
        public float Evaluate(float membershipA, float membershipB)
        {
            // Dynamically find and invoke Mathf.Max using reflection
            Type mathfType = typeof(UnityEngine.Mathf);
            MethodInfo maxMethod = mathfType.GetMethod("Max", BindingFlags.Static | BindingFlags.Public);

            if (maxMethod == null)
                throw new InvalidOperationException("Mathf.Max method not found via reflection.");

            return (float)maxMethod.Invoke(null, new object[] { membershipA, membershipB });
        }
    }
}
