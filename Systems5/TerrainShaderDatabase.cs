using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainShaderDatabase", menuName = "Terrain Shader Database/Terrain Shader Database")]
public class TerrainShaderDatabase : ScriptableObject
{
    public List<TerrainShaderMetadata> shaders = new List<TerrainShaderMetadata>();

    public void AddShader(TerrainShaderMetadata shaderData)
    {
        if (!shaders.Contains(shaderData))
        {
            shaders.Add(shaderData);
        }
    }

    public void RemoveShader(TerrainShaderMetadata shaderData)
    {
        if (shaders.Contains(shaderData))
        {
            shaders.Remove(shaderData);
        }
    }

    public TerrainShaderMetadata FindShaderByName(string name)
    {
        return shaders.Find(shader => shader.shaderName == name);
    }

    public List<TerrainShaderMetadata> FindShadersByTag(string tag)
    {
        return shaders.FindAll(shader => System.Array.Exists(shader.tags, t => t == tag));
    }
}
