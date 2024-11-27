using System;
using System.Collections.Generic;

public sealed class SamplerGradientResult
{
    /// <summary>
    /// Gets the gradients of the sample probabilities.
    /// </summary>
    public IReadOnlyList<IReadOnlyList<Dictionary<int, float>>> Gradients { get; }

    /// <summary>
    /// Gets additional information about the job.
    /// </summary>
    public IReadOnlyList<object> Metadata { get; }

    /// <summary>
    /// Gets the runtime options for the execution of the job.
    /// </summary>
    public GradientOptions Options { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="SamplerGradientResult"/> class.
    /// </summary>
    /// <param name="gradients">The gradients of the sample probabilities.</param>
    /// <param name="metadata">Additional information about the job.</param>
    /// <param name="options">Runtime options for the execution of the job.</param>
    public SamplerGradientResult(
        IReadOnlyList<IReadOnlyList<Dictionary<int, float>>> gradients,
        IReadOnlyList<object> metadata,
        GradientOptions options
    )
    {
        Gradients = gradients ?? throw new ArgumentNullException(nameof(gradients));
        Metadata = metadata ?? throw new ArgumentNullException(nameof(metadata));
        Options = options ?? throw new ArgumentNullException(nameof(options));
    }
}

public class GradientOptions
{
    // Add properties and methods relevant to runtime options.
    public bool UseOptimization { get; set; } = true;
}
