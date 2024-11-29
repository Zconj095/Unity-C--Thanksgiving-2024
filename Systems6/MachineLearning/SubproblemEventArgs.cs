using System;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Subproblem progress event argument.
    /// </summary>
    [Serializable]
    public class SubproblemEventArgsV2 : EventArgs
    {
        [SerializeField] private int class1;
        [SerializeField] private int class2;
        [SerializeField] private int progress;
        [SerializeField] private int maximum;

        /// <summary>
        ///   One of the classes belonging to the subproblem.
        /// </summary>
        public int Class1 => class1;

        /// <summary>
        ///  One of the classes belonging to the subproblem.
        /// </summary>
        public int Class2 => class2;

        /// <summary>
        ///   Gets the progress of the overall problem,
        ///   ranging from zero up to <see cref="Maximum"/>.
        /// </summary>
        public int Progress
        {
            get => progress;
            set => progress = value;
        }

        /// <summary>
        ///   Gets the maximum value for the current <see cref="Progress"/>.
        /// </summary>
        public int Maximum
        {
            get => maximum;
            set => maximum = value;
        }

        /// <summary>
        ///   Initializes a new instance of the <see cref="SubproblemEventArgsV2"/> class.
        /// </summary>
        /// <param name="class1">One of the classes in the subproblem.</param>
        /// <param name="class2">The other class in the subproblem.</param>
        public SubproblemEventArgsV2(int class1, int class2)
        {
            this.class1 = class1;
            this.class2 = class2;
        }
    }
}
