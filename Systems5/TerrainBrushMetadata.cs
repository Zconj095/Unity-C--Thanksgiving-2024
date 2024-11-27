using UnityEngine;

[CreateAssetMenu(fileName = "TerrainBrushMetadata", menuName = "Terrain Brush Database/Brush Metadata")]
public class TerrainBrushMetadata : ScriptableObject
{
    public string brushName;
    public Texture2D brushTexture; // Reference to the brush texture
    public Vector2Int resolution; // Brush resolution (e.g., 512x512)
    public string[] tags; // Tags for filtering (e.g., "Smooth", "Rough", "Cliff")
    public string description; // Optional description of the brush
}
