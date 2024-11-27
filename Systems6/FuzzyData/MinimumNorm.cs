using System;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Minimum Norm, used to calculate the linguistic value of an AND operation.
    /// </summary>
    /// <remarks>
    /// <para>The minimum Norm uses a minimum operator to compute the AND
    /// among two fuzzy memberships.</para>
    /// </remarks>
    public class MinimumNorm : INorm
    {
        /// <summary>
        /// Calculates the numerical result of the AND operation applied to
        /// two fuzzy membership values using reflection.
        /// </summary>
        /// <param name="membershipA">A fuzzy membership value, [0..1].</param>
        /// <param name="membershipB">A fuzzy membership value, [0..1].</param>
        /// <returns>The numerical result of the AND operation applied to <paramref name="membershipA"/>
        /// and <paramref name="membershipB"/>.</returns>
        public float Evaluate(float membershipA, float membershipB)
        {
            // Dynamically find and invoke Mathf.Min using reflection
            Type mathfType = typeof(UnityEngine.Mathf);
            MethodInfo minMethod = mathfType.GetMethod("Min", BindingFlags.Static | BindingFlags.Public);

            if (minMethod == null)
                throw new InvalidOperationException("Mathf.Min method not found via reflection.");

            return (float)minMethod.Invoke(null, new object[] { membershipA, membershipB });
        }
    }
}
