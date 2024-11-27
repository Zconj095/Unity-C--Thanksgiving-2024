using UnityEngine;

[CreateAssetMenu(fileName = "TerrainNormalMapMetadata", menuName = "Terrain Normal Map Database/Normal Map Metadata")]
public class TerrainNormalMapMetadata : ScriptableObject
{
    public string normalMapName;
    public Texture2D normalMapFile; // Reference to the normal map texture file
    public string terrainType; // e.g., "Mountain", "Grassland", "Desert"
    public Vector2Int resolution; // Resolution of the normal map (e.g., 1024x1024)
    public string[] tags; // Tags for filtering, e.g., "Rocky", "Smooth"
    public string description; // Optional description of the normal map
}
