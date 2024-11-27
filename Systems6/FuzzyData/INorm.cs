using System;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Interface with the common methods of a Fuzzy Norm.
    /// </summary>
    /// <remarks><para>All fuzzy operators that act as a Norm must implement this interface.</para></remarks>
    public interface INorm
    {
        /// <summary>
        /// Calculates the numerical result of a Norm (AND) operation applied to
        /// two fuzzy membership values.
        /// </summary>
        /// <param name="membershipA">A fuzzy membership value, [0..1].</param>
        /// <param name="membershipB">A fuzzy membership value, [0..1].</param>
        /// <returns>The numerical result the operation AND applied to <paramref name="membershipA"/>
        /// and <paramref name="membershipB"/>.</returns>
        float Evaluate(float membershipA, float membershipB);
    }

    public class ReflectiveNorm : INorm
    {
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
