using UnityEngine;

[CreateAssetMenu(fileName = "CloudMetadata", menuName = "Cloud Database/Cloud Metadata")]
public class CloudMetadata : ScriptableObject
{
    public string cloudName; // Name of the cloud
    public Texture2D cloudFile; // Reference to the 2D cloud asset
    public Vector2Int resolution; // Resolution of the cloud asset
    public string cloudType; // e.g., "Cumulus", "Stratus", "Cirrus"
    public string[] tags; // Tags for filtering, e.g., "Stormy", "Fluffy", "Transparent"
    public string description; // Optional description of the cloud
}
