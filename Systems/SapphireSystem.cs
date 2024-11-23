using UnityEngine;

public class SapphireSystem : MonoBehaviour
{
    // Sapphire sinusoidal states
    private float[] sapphireSinusoids;

    // Chaos influence (perturbation amplitudes)
    private float[] chaosPerturbations;

    // Harmony factor to balance chaos
    public float harmonyFactor = 0.8f;

    // Lapis Lazuli and Finnonian states
    private float[] lapisLazuliState;
    private float finnonianState;

    // Transformative Sea of Zerah (refreshment state)
    private float zerahRefreshment;

    // Time variable for oscillations
    private float time;

    void Start()
    {
        // Initialize Sapphire sinusoidal states (3D periodic oscillations)
        sapphireSinusoids = new float[] { 1.0f, 0.0f, -1.0f };

        // Initialize chaos perturbations (random influences)
        chaosPerturbations = new float[] { 0.3f, -0.4f, 0.5f };

        // Initialize Lapis Lazuli states
        lapisLazuliState = new float[] { 0.7f, 0.2f, 0.6f };

        // Initialize Finnonian and Zerah states
        finnonianState = 1.0f; // Rebirth factor
        zerahRefreshment = 1.5f; // Transformative influence
    }

    void Update()
    {
        // Increment time for oscillations
        time += Time.deltaTime;

        // Update Sapphire Sinusoids
        UpdateSapphireSinusoids();

        // Synergize harmony and chaos
        SynergizeHarmonyChaos();

        // Apply Finnonian rebirth and Zerah refreshment
        ApplyFinnonianRebirth();
    }

    private void UpdateSapphireSinusoids()
    {
        // Generate sinusoidal oscillations
        for (int i = 0; i < sapphireSinusoids.Length; i++)
        {
            sapphireSinusoids[i] = Mathf.Sin(time + i * Mathf.PI / 2);
        }
    }

    private void SynergizeHarmonyChaos()
    {
        // Blend chaos perturbations with sinusoids using the harmony factor
        for (int i = 0; i < sapphireSinusoids.Length; i++)
        {
            sapphireSinusoids[i] += chaosPerturbations[i] * harmonyFactor;
        }
    }

    private void ApplyFinnonianRebirth()
    {
        // Transform Sapphire states using Lapis Lazuli and Zerah influences
        for (int i = 0; i < sapphireSinusoids.Length; i++)
        {
            sapphireSinusoids[i] *= finnonianState + zerahRefreshment * lapisLazuliState[i];
        }
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            // Visualize Sapphire Sinusoids as spheres in 3D space
            Gizmos.color = Color.cyan;
            for (int i = 0; i < sapphireSinusoids.Length; i++)
            {
                Gizmos.DrawSphere(new Vector3(i, sapphireSinusoids[i], 0), 0.2f);
            }

            // Visualize Finnonian rebirth effect as a larger sphere
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(new Vector3(1, finnonianState, 0), 0.3f);

            // Visualize Zerah refreshment influence
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(new Vector3(2, zerahRefreshment, 0), 0.3f);
        }
    }

    void OnGUI()
    {
        // Display Sapphire states, Finnonian state, and Zerah refreshment
        GUI.Label(new Rect(10, 10, 400, 20), $"Sapphire States: [{string.Join(", ", sapphireSinusoids)}]");
        GUI.Label(new Rect(10, 30, 300, 20), $"Finnonian State: {finnonianState}");
        GUI.Label(new Rect(10, 50, 300, 20), $"Zerah Refreshment: {zerahRefreshment}");
    }
}
