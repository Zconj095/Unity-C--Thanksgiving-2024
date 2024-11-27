using UnityEngine;

[CreateAssetMenu(fileName = "FlowerMetadata", menuName = "Flower Database/Flower Metadata")]
public class FlowerMetadata : ScriptableObject
{
    public string flowerName; // Name of the flower
    public Texture2D flowerFile; // Reference to the 2D flower asset
    public Vector2Int resolution; // Resolution of the flower asset
    public string flowerType; // e.g., "Rose", "Tulip", "Daisy"
    public string[] tags; // Tags for filtering, e.g., "Red", "Spring", "Decorative"
    public string description; // Optional description of the flower
}
