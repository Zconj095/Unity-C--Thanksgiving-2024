using System;

namespace EdgeLoreMachineLearning
{
    /// <summary>
    /// Interface for supervised learning with support for multiple output types.
    /// </summary>
    public interface ILinearSupportVectorMachineLearning :
        ISupervisedLearning<SupportVectorMachine, double[], double>,
        ISupervisedLearning<SupportVectorMachine, double[], int>,
        ISupervisedLearning<SupportVectorMachine, double[], bool>,
        ISupportVectorMachineLearning
    {
    }

    /// <summary>
    /// Interface for SVM learning algorithms using generic input types.
    /// </summary>
    public interface ISupportVectorMachineLearning :
        ISupportVectorMachineLearning<double[]>
    {
    }

    /// <summary>
    /// Interface for SVM learning algorithms using generic input types.
    /// </summary>
    public interface ISupportVectorMachineLearning<TInput>
    {
        /// <summary>
        /// Runs the learning algorithm.
        /// </summary>
        /// <returns>The error rate of the model.</returns>
        [Obsolete("Use Learn method instead.")]
        double Run();

        /// <summary>
        /// Runs the learning algorithm and optionally computes the error.
        /// </summary>
        /// <param name="computeError">Whether to compute the error after training.</param>
        /// <returns>The error rate of the model.</returns>
        [Obsolete("Use Learn method instead.")]
        double Run(bool computeError);

        /// <summary>
        /// Learns the model using the provided inputs and outputs.
        /// </summary>
        /// <typeparam name="TModel">The type of model being learned.</typeparam>
        /// <param name="inputs">The training inputs.</param>
        /// <param name="outputs">The training outputs.</param>
        /// <param name="weights">Optional sample weights.</param>
        /// <returns>The learned model.</returns>
        TModel Learn<TModel>(TInput[] inputs, double[] outputs, double[] weights = null) where TModel : SupportVectorMachine;
    }

    /// <summary>
    /// Interface for SVM learning algorithms with a specified kernel.
    /// </summary>
    public interface ISupportVectorMachineLearning<TKernel, TInput>
        where TKernel : IKernel<TInput>
        where TInput : ICloneable
    {
        /// <summary>
        /// Gets or sets the support vector machine being learned.
        /// </summary>
        SupportVectorMachine<TKernel, TInput> Model { get; set; }

        /// <summary>
        /// Runs the learning algorithm.
        /// </summary>
        /// <returns>The error rate of the model.</returns>
        [Obsolete("Use Learn method instead.")]
        double Run();

        /// <summary>
        /// Runs the learning algorithm and optionally computes the error.
        /// </summary>
        /// <param name="computeError">Whether to compute the error after training.</param>
        /// <returns>The error rate of the model.</returns>
        [Obsolete("Use Learn method instead.")]
        double Run(bool computeError);

        /// <summary>
        /// Learns the model using the provided inputs and outputs.
        /// </summary>
        /// <param name="inputs">The training inputs.</param>
        /// <param name="outputs">The training outputs.</param>
        /// <param name="weights">Optional sample weights.</param>
        /// <returns>The learned model.</returns>
        SupportVectorMachine<TKernel, TInput> Learn(TInput[] inputs, double[] outputs, double[] weights = null);
    }

}
