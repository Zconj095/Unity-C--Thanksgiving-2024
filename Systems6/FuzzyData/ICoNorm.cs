using System;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Interface with the common methods of a Fuzzy CoNorm.
    /// </summary>
    /// <remarks><para>All fuzzy operators that act as a CoNorm must implement this interface.</para></remarks>
    public interface ICoNorm
    {
        /// <summary>
        /// Calculates the numerical result of a CoNorm (OR) operation applied to
        /// two fuzzy membership values.
        /// </summary>
        /// <param name="membershipA">A fuzzy membership value, [0..1].</param>
        /// <param name="membershipB">A fuzzy membership value, [0..1].</param>
        /// <returns>The numerical result the operation OR applied to <paramref name="membershipA"/>
        /// and <paramref name="membershipB"/>.</returns>
        float Evaluate(float membershipA, float membershipB);
    }

    public class ReflectiveCoNorm : ICoNorm
    {
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
