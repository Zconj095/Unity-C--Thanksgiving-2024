using UnityEngine;

[CreateAssetMenu(fileName = "TerrainMaterialMetadata", menuName = "Terrain Material Database/Terrain Material Metadata")]
public class TerrainMaterialMetadata : ScriptableObject
{
    public string materialName;
    public string terrainType; // e.g., "Grassland", "Desert", "Mountain"
    public Material materialFile; // Reference to the material file
    public string[] tags; // Tags for filtering, e.g., "Walkable", "Wet", "Procedural"
    public string description; // Optional description of the material
}
