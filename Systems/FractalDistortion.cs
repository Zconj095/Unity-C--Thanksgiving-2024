using UnityEngine;

public class FractalDistortion : MonoBehaviour
{
    // Parameters for fractal distortion
    public float alpha = 0.8f;
    public float beta = 0.6f;
    public float gamma = 0.1f;

    // Grid size
    private int gridSize = 256;
    private float[,] distortionGrid;

    void Start()
    {
        // Initialize fractal distortion grid
        distortionGrid = new float[gridSize, gridSize];
        GenerateFractalDistortion();
    }

    void GenerateFractalDistortion()
    {
        // Initialize base fractal grid
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                distortionGrid[x, y] = Random.Range(-1f, 1f);
            }
        }

        // Apply fractal distortion over iterations
        for (int n = 0; n < 10; n++) // Iterative process
        {
            for (int x = 0; x < gridSize; x++)
            {
                for (int y = 0; y < gridSize; y++)
                {
                    float perturbation = Mathf.Sin(x * 0.1f) * Mathf.Cos(y * 0.1f); // Example perturbation
                    distortionGrid[x, y] = alpha * Mathf.Pow(distortionGrid[x, y], 2) +
                                           beta * perturbation + gamma;
                }
            }
        }

        VisualizeFractal();
    }

    void VisualizeFractal()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                // Visualize fractal points using spheres
                GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                sphere.transform.position = new Vector3(x, y, distortionGrid[x, y]);
                sphere.transform.localScale = Vector3.one * 0.1f;
                sphere.GetComponent<Renderer>().material.color = Color.Lerp(Color.blue, Color.red, (distortionGrid[x, y] + 1) / 2f);
            }
        }
    }
}
