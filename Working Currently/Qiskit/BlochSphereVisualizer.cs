using UnityEngine;

public class BlochSphereVisualizer : MonoBehaviour
{
    public GameObject SpherePrefab;

    public void VisualizeState(QuantumState state)
    {
        // Create a Bloch sphere for each qubit
        for (int i = 0; i < state.StateVector.Length; i++)
        {
            Vector3 position = new Vector3(i * 2.0f, 0, 0); // Spread qubits along X-axis
            GameObject sphere = Instantiate(SpherePrefab, position, Quaternion.identity);
            sphere.name = $"Qubit {i} Bloch Sphere";

            // Example visualization logic (state vector to color mapping)
            Renderer renderer = sphere.GetComponent<Renderer>();
            renderer.material.color = new Color((float)state.StateVector[i].Real, 0, (float)state.StateVector[i].Imaginary);
        }
    }
}
