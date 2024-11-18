using UnityEngine;

public class BilinearInterpolation : MonoBehaviour
{
    [SerializeField] private Texture2D texture; // Reference to the texture you want to sample from

    private void Start()
    {
        if (texture == null)
        {
            Debug.LogError("Texture is not assigned. Please assign a texture in the inspector.");
            return;
        }

        // Example of how to use bilinear interpolation
        Vector2 samplePoint = new Vector2(0.5f, 0.5f); // Coordinates to sample (can be any floating-point value)
        Color color = BilinearSample(texture, samplePoint);
        Debug.Log($"Sampled color: {color}");
    }

    // Bilinear interpolation function
    private Color BilinearSample(Texture2D texture, Vector2 point)
    {
        // Ensure point is within valid bounds [0, 1]
        point.x = Mathf.Clamp01(point.x);
        point.y = Mathf.Clamp01(point.y);

        // Get the integer coordinates of the four surrounding pixels
        int x0 = Mathf.FloorToInt(point.x * (texture.width - 1));
        int y0 = Mathf.FloorToInt(point.y * (texture.height - 1));
        int x1 = Mathf.Min(x0 + 1, texture.width - 1); // Ensure we don't go out of bounds
        int y1 = Mathf.Min(y0 + 1, texture.height - 1);

        // Get the fractional offsets within the pixel (relative to the pixel grid)
        float u = (point.x * (texture.width - 1)) - x0;
        float v = (point.y * (texture.height - 1)) - y0;

        // Sample the four neighboring pixels
        Color c00 = texture.GetPixel(x0, y0);
        Color c10 = texture.GetPixel(x1, y0);
        Color c01 = texture.GetPixel(x0, y1);
        Color c11 = texture.GetPixel(x1, y1);

        // Perform the bilinear interpolation
        Color interpolatedColor = 
            (1 - u) * (1 - v) * c00 +
            u * (1 - v) * c10 +
            (1 - u) * v * c01 +
            u * v * c11;

        return interpolatedColor;
    }
}
