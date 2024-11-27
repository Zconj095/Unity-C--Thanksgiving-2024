using UnityEngine;

[CreateAssetMenu(fileName = "SeamlessTerrainTextureMetadata", menuName = "Terrain Texture Database/Texture Metadata")]
public class SeamlessTerrainTextureMetadata : ScriptableObject
{
    public string textureName;
    public Texture2D textureFile; // Reference to the seamless texture
    public Vector2Int resolution; // Texture resolution (e.g., 1024x1024)
    public string biomeType; // e.g., "Forest", "Desert", "Mountain"
    public string[] tags; // Tags for filtering, e.g., "Rocky", "Grassy", "Snowy"
    public string description; // Optional description of the texture
}
