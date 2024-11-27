using System;
using UnityEngine;

namespace UnityFuzzy
{
    /// <summary>
    /// Interface specifying the methods required for all defuzzification methods
    /// in a Fuzzy Inference System, adapted for Unity.
    /// </summary>
    /// <remarks>
    /// <para>
    /// In many applications, a Fuzzy Inference System performs linguistic computation, 
    /// but a numerical value is required at the end of the inference process. This numerical 
    /// value is often used to control other systems. A defuzzification method converts the 
    /// fuzzy linguistic output into a numerical value.
    /// </para>
    /// <para>
    /// This interface serves as a contract for implementing various defuzzification methods
    /// in Unity.
    /// </para>
    /// </remarks>
    public interface IDefuzzifier
    {
        /// <summary>
        /// Defuzzification method to compute the numerical representation of fuzzy output.
        /// </summary>
        /// <param name="fuzzyOutput">An object containing the fuzzy output from 
        /// a Fuzzy Inference System's rules.</param>
        /// <param name="normOperator">An object used to constrain the label's 
        /// membership with its firing strength.</param>
        /// <returns>Numerical representation of the fuzzy output.</returns>
        float Defuzzify(object fuzzyOutput, object normOperator);
    }
}
