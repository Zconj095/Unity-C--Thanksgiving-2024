using UnityEngine;

[CreateAssetMenu(fileName = "ParticleTextureMetadata", menuName = "Particle Texture Database/Texture Metadata")]
public class ParticleTextureMetadata : ScriptableObject
{
    public string textureName;
    public Texture2D textureFile; // Reference to the particle texture
    public Vector2Int resolution; // Texture resolution (e.g., 512x512)
    public string[] tags; // Tags for filtering, e.g., "Fire", "Smoke", "Spark"
    public string description; // Optional description of the texture
}
