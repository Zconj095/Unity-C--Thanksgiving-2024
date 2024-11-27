using System;
using System.Reflection;

namespace UnityFuzzy
{
    /// <summary>
    /// Interface with the common methods of Fuzzy Unary Operator.
    /// </summary>
    /// <remarks>
    /// <para>All fuzzy operators that act as a Unary Operator (NOT, VERY, LITTLE) must implement this interface.</para>
    /// </remarks>
    public interface IUnaryOperator
    {
        /// <summary>
        /// Calculates the numerical result of a Unary operation applied to one
        /// fuzzy membership value.
        /// </summary>
        /// <param name="membership">A fuzzy membership value, [0..1].</param>
        /// <returns>The numerical result of the operation applied to <paramref name="membership"/>.</returns>
        float Evaluate(float membership);
    }

    public class ReflectiveUnaryOperator : IUnaryOperator
    {
        private readonly string methodName;

        public ReflectiveUnaryOperator(string methodName)
        {
            this.methodName = methodName;
        }

        public float Evaluate(float membership)
        {
            // Dynamically find and invoke the specified method in Mathf
            Type mathfType = typeof(UnityEngine.Mathf);
            MethodInfo method = mathfType.GetMethod(methodName, BindingFlags.Static | BindingFlags.Public);

            if (method == null)
                throw new InvalidOperationException($"Mathf.{methodName} method not found via reflection.");

            return (float)method.Invoke(null, new object[] { membership });
        }
    }
}
