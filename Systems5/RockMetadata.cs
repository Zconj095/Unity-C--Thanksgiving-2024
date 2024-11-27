using UnityEngine;

[CreateAssetMenu(fileName = "RockMetadata", menuName = "Rock Database/Rock Metadata")]
public class RockMetadata : ScriptableObject
{
    public string rockName; // Name of the rock
    public Texture2D rockFile; // Reference to the 2D rock asset
    public Vector2Int resolution; // Resolution of the rock asset
    public string rockType; // e.g., "Granite", "Sandstone", "Basalt"
    public string[] tags; // Tags for filtering, e.g., "Smooth", "Rough", "Layered"
    public string description; // Optional description of the rock
}
