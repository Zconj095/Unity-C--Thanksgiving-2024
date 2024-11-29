using System;
using UnityEngine;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    ///   Parameters for learning a binary decision model. An object of this class is passed by
    ///   `OneVsRestLearning` or `OneVsOneLearning` to instruct how binary learning algorithms should
    ///   create their binary classifiers.
    /// </summary>
    /// <typeparam name="TBinary">The type of the binary model to be learned.</typeparam>
    /// <typeparam name="TInput">The input type for the binary classifiers.</typeparam>
    [Serializable]
    public class InnerParameters<TBinary, TInput>
    {
        [SerializeField] private TBinary model;
        [SerializeField] private TInput[] inputs;
        [SerializeField] private bool[] outputs;
        [SerializeField] private ClassPair pair;

        /// <summary>
        ///   Gets the binary model to be learned.
        /// </summary>
        public TBinary Model => model;

        /// <summary>
        ///   Gets the input data that should be used to train the classifier.
        /// </summary>
        public TInput[] Inputs => inputs;

        /// <summary>
        ///   Gets the output data that should be used to train the classifier.
        /// </summary>
        public bool[] Outputs => outputs;

        /// <summary>
        ///   Gets the class pair that the classifier will be designated to learn.
        /// </summary>
        public ClassPair Pair => pair;

        /// <summary>
        /// Initializes a new instance of the <see cref="InnerParameters{TBinary, TInput}"/> class.
        /// </summary>
        /// <param name="model">The binary model to be learned.</param>
        /// <param name="inputs">The inputs to be used.</param>
        /// <param name="outputs">The outputs to be used.</param>
        /// <param name="pair">The class labels for the problem to be learned.</param>
        public InnerParameters(TBinary model, TInput[] inputs, bool[] outputs, ClassPair pair)
        {
            this.model = model;
            this.inputs = inputs;
            this.outputs = outputs;
            this.pair = pair;
        }
    }
}
