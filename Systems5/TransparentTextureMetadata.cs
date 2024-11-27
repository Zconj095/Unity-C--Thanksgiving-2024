using UnityEngine;

[CreateAssetMenu(fileName = "TransparentTextureMetadata", menuName = "Transparent Texture Database/Texture Metadata")]
public class TransparentTextureMetadata : ScriptableObject
{
    public string textureName;
    public Texture2D textureFile; // Reference to the transparent texture
    public Vector2Int resolution; // Texture resolution (e.g., 512x512)
    public string[] tags; // Tags for filtering, e.g., "UI", "Overlay", "Glass"
    public string description; // Optional description of the texture
}
