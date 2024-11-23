using UnityEngine;

public class RinneTransformation : MonoBehaviour
{
    // Rinne-defined coordinates (input space)
    private float[] rinneCoordinates;

    // Transformation matrices for scaling, rotation, and projection
    private float[,] scalingMatrix;
    private float[,] rotationMatrix;
    private float[,] projectionMatrix;

    // Final Cartesian XYZ output
    private Vector3 cartesianOutput;

    void Start()
    {
        // Initialize Rinne-defined coordinates (Example: 3D point)
        rinneCoordinates = new float[] { 2.0f, 1.5f, -0.5f };

        // Initialize scaling matrix (example scaling factors for each axis)
        scalingMatrix = new float[3, 3]
        {
            { 1.2f, 0.0f, 0.0f },
            { 0.0f, 1.1f, 0.0f },
            { 0.0f, 0.0f, 1.3f }
        };

        // Initialize rotation matrix (example: 90-degree rotation about Y-axis)
        rotationMatrix = new float[3, 3]
        {
            { Mathf.Cos(Mathf.PI / 2), 0.0f, Mathf.Sin(Mathf.PI / 2) },
            { 0.0f, 1.0f, 0.0f },
            { -Mathf.Sin(Mathf.PI / 2), 0.0f, Mathf.Cos(Mathf.PI / 2) }
        };

        // Initialize projection matrix (example: perspective projection)
        projectionMatrix = new float[3, 3]
        {
            { 1.0f, 0.0f, 0.0f },
            { 0.0f, 1.0f, 0.0f },
            { 0.0f, 0.0f, 0.5f } // Depth scaling
        };
    }

    void Update()
    {
        // Apply the full transformation pipeline
        ApplyRinneTransformation();
    }

    private void ApplyRinneTransformation()
    {
        // Apply scaling, rotation, and projection in sequence
        float[] scaledCoordinates = MatrixMultiply(scalingMatrix, rinneCoordinates);
        float[] rotatedCoordinates = MatrixMultiply(rotationMatrix, scaledCoordinates);
        float[] projectedCoordinates = MatrixMultiply(projectionMatrix, rotatedCoordinates);

        // Convert to Vector3 for Cartesian XYZ representation
        cartesianOutput = new Vector3(
            projectedCoordinates[0],
            projectedCoordinates[1],
            projectedCoordinates[2]
        );
    }

    // Perform matrix-vector multiplication
    private float[] MatrixMultiply(float[,] matrix, float[] vector)
    {
        float[] result = new float[vector.Length];
        for (int i = 0; i < vector.Length; i++)
        {
            result[i] = 0.0f;
            for (int j = 0; j < vector.Length; j++)
            {
                result[i] += matrix[i, j] * vector[j];
            }
        }
        return result;
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            // Visualize the transformed Cartesian output as a sphere
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(cartesianOutput, 0.3f);
        }
    }

    void OnGUI()
    {
        // Display the Cartesian output
        GUI.Label(new Rect(10, 10, 300, 20), $"Cartesian Output: {cartesianOutput}");
    }
}
