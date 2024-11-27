using UnityEngine;

[CreateAssetMenu(fileName = "CrystalMetadata", menuName = "Crystal Database/Crystal Metadata")]
public class CrystalMetadata : ScriptableObject
{
    public string crystalName; // Name of the crystal
    public Texture2D crystalFile; // Reference to the 2D crystal asset
    public Vector2Int resolution; // Resolution of the crystal asset
    public string crystalType; // e.g., "Quartz", "Emerald", "Ruby"
    public string[] tags; // Tags for filtering, e.g., "Rare", "Shiny", "Blue"
    public string description; // Optional description of the crystal
}
