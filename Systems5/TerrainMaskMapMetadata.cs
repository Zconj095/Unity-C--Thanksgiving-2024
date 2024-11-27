using UnityEngine;

[CreateAssetMenu(fileName = "TerrainMaskMapMetadata", menuName = "Terrain Mask Map Database/Mask Map Metadata")]
public class TerrainMaskMapMetadata : ScriptableObject
{
    public string maskMapName;
    public Texture2D maskMapFile; // Reference to the mask map texture file
    public string terrainType; // e.g., "Forest", "Desert", "Rocky"
    public Vector2Int resolution; // Resolution of the mask map (e.g., 1024x1024)
    public string[] tags; // Tags for filtering, e.g., "Cliff", "Path", "Water"
    public string description; // Optional description of the mask map
}
