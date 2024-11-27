using System;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Product Norm, used to calculate the linguistic value of an AND operation.
    /// </summary>
    /// <remarks>
    /// <para>The product Norm uses a multiplication operator to compute the
    /// AND among two fuzzy memberships.</para>
    /// </remarks>
    public class ProductNorm : INorm
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
            // Dynamically find and invoke multiplication using reflection
            Type mathType = typeof(UnityEngine.Mathf);
            MethodInfo multiplyMethod = mathType.GetMethod("Multiply", BindingFlags.Static | BindingFlags.Public);

            if (multiplyMethod == null)
            {
                // Since Mathf.Multiply doesn't exist, perform manual multiplication
                return membershipA * membershipB;
            }

            return (float)multiplyMethod.Invoke(null, new object[] { membershipA, membershipB });
        }
    }
}
