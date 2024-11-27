using System;
using UnityEngine;

namespace UnityFuzzy
{
    /// <summary>
    /// Interface specifying the methods required for all membership functions in Unity.
    /// </summary>
    /// <remarks>
    /// <para>
    /// All membership functions must implement this interface, which is used to calculate
    /// a value's degree of membership to a fuzzy set.
    /// </para>
    /// </remarks>
    public interface IMembershipFunction
    {
        /// <summary>
        /// Calculate membership of a given value to the fuzzy set.
        /// </summary>
        /// <param name="x">The value whose membership will be calculated.</param>
        /// <returns>Degree of membership [0..1] of the value to the fuzzy set.</returns>
        float GetMembership(float x);

        /// <summary>
        /// The leftmost x value of the membership function.
        /// </summary>
        float LeftLimit { get; }

        /// <summary>
        /// The rightmost x value of the membership function.
        /// </summary>
        float RightLimit { get; }
    }
}
