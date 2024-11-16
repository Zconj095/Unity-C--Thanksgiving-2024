using System;

public class RawFeatureVector : FeatureMap
{
    /// <summary>
    /// Constructs the Raw Feature Vector.
    /// Automatically pads and normalizes input vectors to create valid quantum states.
    /// </summary>
    /// <param name="featureDimension">The feature dimension, must be greater than or equal to 1.</param>
    public RawFeatureVector(int featureDimension)
    {
        if (featureDimension < 1)
        {
            throw new ArgumentException("Feature dimension must be at least 1.");
        }

        FeatureDimension = featureDimension;
        NumQubits = NextPowerOfTwo(featureDimension);
    }

    /// <summary>
    /// Constructs the quantum circuit that encodes the raw feature vector.
    /// </summary>
    /// <param name="data">Input feature vector (array of floats).</param>
    /// <param name="qubits">Optional QuantumRegister for the qubits.</param>
    /// <param name="inverse">Indicates whether to construct the inverse circuit.</param>
    /// <returns>QuantumCircuit encoding the feature vector.</returns>
    public override QuantumCircuit ConstructCircuit(float[] data, QuantumRegister qubits = null, bool inverse = false)
    {
        if (data.Length != FeatureDimension)
        {
            throw new ArgumentException($"Input data length ({data.Length}) does not match the feature dimension ({FeatureDimension}).");
        }

        // Pad the feature vector to the nearest power of 2
        int paddedLength = 1 << NumQubits;
        float[] paddedVector = new float[paddedLength];
        Array.Copy(data, paddedVector, data.Length);

        // Normalize the vector
        paddedVector = NormalizeVector(paddedVector);

        // Use StateVectorCircuit to create the circuit
        StateVectorCircuit svc = new StateVectorCircuit(paddedVector);

        return svc.ConstructCircuit(qubits);
    }

    /// <summary>
    /// Computes the next power of two greater than or equal to a given integer.
    /// </summary>
    /// <param name="value">The input integer.</param>
    /// <returns>The nearest power of two greater than or equal to the input.</returns>
    private static int NextPowerOfTwo(int value)
    {
        int power = 1;
        while (power < value)
        {
            power *= 2;
        }
        return power;
    }

    /// <summary>
    /// Normalizes a vector such that its L2 norm equals 1.
    /// </summary>
    /// <param name="vector">Input vector to normalize.</param>
    /// <returns>Normalized vector.</returns>
    private static float[] NormalizeVector(float[] vector)
    {
        float norm = 0;
        foreach (float v in vector)
        {
            norm += v * v;
        }
        norm = (float)Math.Sqrt(norm);

        if (norm == 0)
        {
            throw new ArgumentException("Vector norm cannot be zero.");
        }

        for (int i = 0; i < vector.Length; i++)
        {
            vector[i] /= norm;
        }

        return vector;
    }
}
