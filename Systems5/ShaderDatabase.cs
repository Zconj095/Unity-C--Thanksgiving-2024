using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShaderDatabase", menuName = "Shader Database/Shader Database")]
public class ShaderDatabase : ScriptableObject
{
    public List<ShaderMetadata> shaders = new List<ShaderMetadata>();

    public void AddShader(ShaderMetadata shaderData)
    {
        if (!shaders.Contains(shaderData))
        {
            shaders.Add(shaderData);
        }
    }

    public void RemoveShader(ShaderMetadata shaderData)
    {
        if (shaders.Contains(shaderData))
        {
            shaders.Remove(shaderData);
        }
    }

    public ShaderMetadata FindShaderByName(string name)
    {
        return shaders.Find(shader => shader.shaderName == name);
    }
}
