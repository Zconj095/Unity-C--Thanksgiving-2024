using UnityEngine;

public class ImageProcessor
{
    /// <summary>
    /// Converts an image into a fixed-size embedding (feature vector).
    /// </summary>
    public float[] ConvertImageToEmbedding(byte[] imageData)
    {
        // Step 1: Load image data into a Texture2D
        Texture2D texture = new Texture2D(2, 2); // Initialize with dummy size
        if (!texture.LoadImage(imageData))
        {
            Debug.LogError("Failed to load image data.");
            return null;
        }

        // Step 2: Resize the image to a fixed resolution (e.g., 32x32)
        Texture2D resizedTexture = ResizeTexture(texture, 32, 32);

        // Step 3: Extract pixel features and normalize
        float[] embedding = ExtractNormalizedPixelFeatures(resizedTexture);

        return embedding;
    }

    /// <summary>
    /// Resizes a Texture2D to the specified width and height.
    /// </summary>
    private Texture2D ResizeTexture(Texture2D original, int width, int height)
    {
        RenderTexture rt = RenderTexture.GetTemporary(width, height);
        RenderTexture.active = rt;

        // Copy original texture into the RenderTexture
        Graphics.Blit(original, rt);

        // Create a new Texture2D and read pixels from the RenderTexture
        Texture2D resized = new Texture2D(width, height);
        resized.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        resized.Apply();

        // Clean up
        RenderTexture.active = null;
        RenderTexture.ReleaseTemporary(rt);

        return resized;
    }

    /// <summary>
    /// Extracts normalized grayscale pixel values from a Texture2D.
    /// </summary>
    private float[] ExtractNormalizedPixelFeatures(Texture2D texture)
    {
        int width = texture.width;
        int height = texture.height;
        Color[] pixels = texture.GetPixels();

        float[] features = new float[width * height];
        for (int i = 0; i < pixels.Length; i++)
        {
            // Convert to grayscale and normalize to [0, 1]
            features[i] = (pixels[i].r + pixels[i].g + pixels[i].b) / 3.0f;
        }

        return features;
    }
}
