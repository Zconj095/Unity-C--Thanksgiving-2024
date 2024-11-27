using UnityEngine;

namespace UnityFuzzy
{
    /// <summary>
    /// Membership function for fuzzy singletons: fuzzy sets with a single point where the membership is 1.
    /// </summary>
    /// <remarks>
    /// <para>
    /// Singleton functions are used to represent crisp (classical) numbers in a fuzzy domain. 
    /// A fuzzy singleton is a fuzzy set with only one point returning a non-zero membership.
    /// </para>
    /// <para>
    /// Example usage:
    /// <code>
    /// // Create an instance of the SingletonFunction
    /// SingletonFunction membershipFunction = new SingletonFunction(10);
    /// // Get membership for several points
    /// for (int i = 0; i < 20; i++)
    /// {
    ///     Debug.Log($"Membership of {i}: {membershipFunction.GetMembership(i)}");
    /// }
    /// </code>
    /// </para>
    /// </remarks>
    public class SingletonFunction : IMembershipFunction
    {
        /// <summary>
        /// The unique point where the membership value is 1.
        /// </summary>
        private float support;

        /// <summary>
        /// The leftmost x value of the membership function, same as the support.
        /// </summary>
        public float LeftLimit
        {
            get { return support; }
        }

        /// <summary>
        /// The rightmost x value of the membership function, same as the support.
        /// </summary>
        public float RightLimit
        {
            get { return support; }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SingletonFunction"/> class.
        /// </summary>
        /// <param name="support">The support is the only x value where the membership function returns 1.</param>
        public SingletonFunction(float support)
        {
            this.support = support;
        }

        /// <summary>
        /// Calculates the membership of a given value to the singleton function.
        /// </summary>
        /// <param name="x">The value for which the membership is calculated.</param>
        /// <returns>
        /// Degree of membership: returns 1 if the value matches the support, otherwise 0.
        /// </returns>
        public float GetMembership(float x)
        {
            return Mathf.Approximately(support, x) ? 1f : 0f;
        }
    }
}
