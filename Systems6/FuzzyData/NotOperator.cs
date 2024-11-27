using System;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// NOT operator, used to calculate the complement of a fuzzy set.
    /// </summary>
    /// <remarks>
    /// <para>The NOT operator definition is (1 - m) for all the values of membership m of the fuzzy set.</para>
    /// </remarks>
    public class NotOperator : IUnaryOperator
    {
        /// <summary>
        /// Calculates the numerical result of the NOT operation applied to
        /// a fuzzy membership value using reflection.
        /// </summary>
        /// <param name="membership">A fuzzy membership value, [0..1].</param>
        /// <returns>The numerical result of the unary operation NOT applied to <paramref name="membership"/>.</returns>
        public float Evaluate(float membership)
        {
            // Use reflection to dynamically find and invoke the subtraction operation
            Type mathType = typeof(UnityEngine.Mathf);
            MethodInfo subtractMethod = mathType.GetMethod("Subtract", BindingFlags.Static | BindingFlags.Public);

            if (subtractMethod == null)
            {
                // Since Mathf.Subtract doesn't exist, perform manual subtraction
                return 1 - membership;
            }

            return (float)subtractMethod.Invoke(null, new object[] { 1, membership });
        }
    }
}
