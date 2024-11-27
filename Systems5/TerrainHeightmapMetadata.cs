using UnityEngine;

[CreateAssetMenu(fileName = "TerrainHeightmapMetadata", menuName = "Terrain Heightmap Database/Heightmap Metadata")]
public class TerrainHeightmapMetadata : ScriptableObject
{
    public string heightmapName;
    public Texture2D heightmapFile; // Reference to the heightmap texture
    public string terrainType; // e.g., "Mountain", "Valley", "Plateau"
    public Vector2Int resolution; // Resolution of the heightmap (e.g., 1024x1024)
    public string[] tags; // Tags for filtering, e.g., "Rocky", "Hilly"
    public string description; // Optional description of the heightmap
}
