using UnityEngine;

[CreateAssetMenu(fileName = "BushMetadata", menuName = "Bush Database/Bush Metadata")]
public class BushMetadata : ScriptableObject
{
    public string bushName; // Name of the bush
    public Texture2D bushFile; // Reference to the 2D bush asset
    public Vector2Int resolution; // Resolution of the bush asset
    public string bushType; // e.g., "Shrub", "Berry Bush", "Flowering Bush"
    public string[] tags; // Tags for filtering, e.g., "Dense", "Spring", "Decorative"
    public string description; // Optional description of the bush
}
