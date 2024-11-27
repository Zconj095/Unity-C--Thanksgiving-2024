using UnityEngine;

[CreateAssetMenu(fileName = "SeamlessTextureMetadata", menuName = "Seamless Texture Database/Texture Metadata")]
public class SeamlessTextureMetadata : ScriptableObject
{
    public string textureName;
    public Texture2D textureFile; // Reference to the seamless texture
    public Vector2Int resolution; // Texture resolution (e.g., 1024x1024)
    public string[] tags; // Tags for filtering, e.g., "Stone", "Wood", "Grass"
    public string description; // Optional description of the texture
}
