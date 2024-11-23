using UnityEngine;

public class CosmicFusionSystem : MonoBehaviour
{
    // Centroid coordinates
    private Vector3 cosmicCentroid;

    // Rose Quartz sealing radius
    public float roseQuartzRadius = 3.0f;

    // Sapphire and Lapis Lazuli attributes
    private Vector3 sapphireInfluence;
    private Vector3 lapisLazuliInfluence;

    // Fused state
    private Vector3 fusedCentroid;

    void Start()
    {
        // Initialize cosmic centroid
        cosmicCentroid = new Vector3(0, 0, 0);

        // Initialize Sapphire and Lapis Lazuli influences
        sapphireInfluence = new Vector3(1.5f, -0.5f, 0.7f);
        lapisLazuliInfluence = new Vector3(-0.8f, 0.6f, 1.0f);

        // Initialize fused centroid
        fusedCentroid = cosmicCentroid;
    }

    void Update()
    {
        // Compute the fused centroid
        ComputeFusedCentroid();
    }

    private void ComputeFusedCentroid()
    {
        // Blend Sapphire and Lapis Lazuli influences with the cosmic centroid
        fusedCentroid = cosmicCentroid + 0.5f * sapphireInfluence + 0.5f * lapisLazuliInfluence;

        // Apply Rose Quartz sealing: constrain the fused centroid within the sealing radius
        if (fusedCentroid.magnitude > roseQuartzRadius)
        {
            fusedCentroid = fusedCentroid.normalized * roseQuartzRadius;
        }
    }

    void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            // Visualize the cosmic centroid
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(cosmicCentroid, 0.2f);

            // Visualize the fused centroid
            Gizmos.color = Color.magenta;
            Gizmos.DrawSphere(fusedCentroid, 0.2f);

            // Visualize the Rose Quartz sealing as a sphere
            Gizmos.color = new Color(1.0f, 0.5f, 0.6f, 0.4f); // Semi-transparent pink
            Gizmos.DrawWireSphere(cosmicCentroid, roseQuartzRadius);
        }
    }

    void OnGUI()
    {
        // Display fused centroid details
        GUI.Label(new Rect(10, 10, 300, 20), $"Fused Centroid: {fusedCentroid}");
        GUI.Label(new Rect(10, 30, 300, 20), $"Sealing Radius: {roseQuartzRadius}");
    }
}
