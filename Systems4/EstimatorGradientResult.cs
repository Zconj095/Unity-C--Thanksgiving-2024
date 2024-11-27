using System;
using System.Collections.Generic;

public sealed class EstimatorGradientResult
{
    /// <summary>
    /// Gets the gradients of the expectation values.
    /// </summary>
    public IReadOnlyList<float[,]> Gradients { get; }

    /// <summary>
    /// Gets additional information about the job.
    /// </summary>
    public IReadOnlyList<Dictionary<string, object>> Metadata { get; }

    /// <summary>
    /// Gets the runtime options for the execution of the job.
    /// </summary>
    public GradientOptions Options { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="EstimatorGradientResult"/> class.
    /// </summary>
    /// <param name="gradients">The gradients of the expectation values.</param>
    /// <param name="metadata">Additional information about the job.</param>
    /// <param name="options">Runtime options for the execution of the job.</param>
    public EstimatorGradientResult(
        IReadOnlyList<float[,]> gradients,
        IReadOnlyList<Dictionary<string, object>> metadata,
        GradientOptions options
    )
    {
        Gradients = gradients ?? throw new ArgumentNullException(nameof(gradients));
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        Options = options ?? throw new ArgumentNullException(nameof(options));
    }
}
