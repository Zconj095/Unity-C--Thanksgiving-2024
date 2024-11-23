using UnityEngine;

public class FusionSystemManager : MonoBehaviour
{
    // Cartesian Space Representation
    private Vector3[] cartesianPoints;

    // State variables for Russel & Zoe fusion
    private float[] stateRussel;
    private float[] stateZoe;

    // Fusion matrix for non-linear transformations
    private float[,] fusionMatrix;

    // Output State after fusion
    private float[] fusedState;

    // Cartesian output
    private Vector3 outputPoint;

    void Start()
    {
        // Initialize Cartesian Points (example: a grid in space)
        cartesianPoints = new Vector3[3]
        {
            new Vector3(1.0f, 0.0f, 0.0f),
            new Vector3(0.0f, 1.0f, 0.0f),
            new Vector3(0.0f, 0.0f, 1.0f)
        };

        // Initialize states for Russel and Zoe
        stateRussel = new float[] { 1.0f, 0.5f, 0.2f };
        stateZoe = new float[] { 0.8f, 0.3f, 0.7f };

        // Initialize a 3x3 Fusion Matrix (Example values for a rotation-like transformation)
        fusionMatrix = new float[3, 3]
        {
            { 0.5f, 0.3f, 0.2f },
            { 0.4f, 0.7f, 0.1f },
            { 0.3f, 0.5f, 0.6f }
        };

        // Initialize fused state
        fusedState = new float[3];
    }

    void Update()
    {
        // Perform the fusion and Cartesian transformation
        PerformFusion();
        MapToCartesianSpace();
    }

    private void PerformFusion()
    {
        // Fuse the Russel and Zoe states using the fusion matrix
        for (int i = 0; i < fusedState.Length; i++)
        {
            fusedState[i] = 0.0f;
            for (int j = 0; j < fusedState.Length; j++)
            {
                fusedState[i] += fusionMatrix[i, j] * (stateRussel[j] + stateZoe[j]);
            }
        }
    }

    private void MapToCartesianSpace()
    {
        // Map the fused state into Cartesian space (normalize for visualization)
        outputPoint = new Vector3(
            fusedState[0] / (fusedState[0] + fusedState[1] + fusedState[2]),
            fusedState[1] / (fusedState[0] + fusedState[1] + fusedState[2]),
            fusedState[2] / (fusedState[0] + fusedState[1] + fusedState[2])
        );

        // Scale or offset outputPoint for specific spatial applications
        outputPoint *= 5.0f; // Example scaling
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            // Visualize the Cartesian Points
            Gizmos.color = Color.cyan;
            foreach (var point in cartesianPoints)
            {
                Gizmos.DrawSphere(point, 0.2f);
            }

            // Visualize the fused output point
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(outputPoint, 0.3f);
        }
    }

    void OnGUI()
    {
        // Display the fused state and Cartesian mapping
        GUI.Label(new Rect(10, 10, 300, 20), $"Fused State: [{string.Join(", ", fusedState)}]");
        GUI.Label(new Rect(10, 30, 300, 20), $"Output Point: {outputPoint}");
    }
}
