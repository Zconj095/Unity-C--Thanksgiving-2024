using UnityEngine;

[CreateAssetMenu(fileName = "TerrainShaderMetadata", menuName = "Terrain Shader Database/Terrain Shader Metadata")]
public class TerrainShaderMetadata : ScriptableObject
{
    public string shaderName;
    public string shaderType; // e.g., "PBR", "Standard", "Custom"
    public Shader shaderFile; // Reference to the shader file
    public string[] tags; // Tags for filtering, e.g., "Grass", "Rock", "Snow"
    public string description; // Optional description of the shader
}
