using UnityEngine;

[CreateAssetMenu(fileName = "TerrainMetadata", menuName = "Terrain Database/Terrain Metadata")]
public class TerrainMetadata : ScriptableObject
{
    public string terrainName;
    public string biomeType; // e.g., "Forest", "Desert", "Mountain"
    public TerrainData terrainData; // Reference to the terrain asset
    public Vector2 terrainSize; // Terrain size (width, height)
    public string[] tags; // Tags for filtering, e.g., "Playable", "Procedural"
}
