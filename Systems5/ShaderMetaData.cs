using UnityEngine;

[CreateAssetMenu(fileName = "ShaderMetadata", menuName = "Shader Database/Shader Metadata")]
public class ShaderMetadata : ScriptableObject
{
    public string shaderName;
    public string description;
    public Shader shader;
    public string[] tags; // Optional tags for categorization
}
