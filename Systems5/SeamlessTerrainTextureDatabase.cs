using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeamlessTerrainTextureDatabase", menuName = "Terrain Texture Database/Texture Database")]
public class SeamlessTerrainTextureDatabase : ScriptableObject
{
    public List<SeamlessTerrainTextureMetadata> textures = new List<SeamlessTerrainTextureMetadata>();

    public void AddTexture(SeamlessTerrainTextureMetadata texture)
    {
        if (!textures.Contains(texture))
        {
            textures.Add(texture);
        }
    }

    public void RemoveTexture(SeamlessTerrainTextureMetadata texture)
    {
        if (textures.Contains(texture))
        {
            textures.Remove(texture);
        }
    }

    public SeamlessTerrainTextureMetadata FindTextureByName(string name)
    {
        return textures.Find(texture => texture.textureName == name);
    }

    public List<SeamlessTerrainTextureMetadata> FindTexturesByBiome(string biomeType)
    {
        return textures.FindAll(texture => texture.biomeType == biomeType);
    }

    public List<SeamlessTerrainTextureMetadata> FindTexturesByTag(string tag)
    {
        return textures.FindAll(texture => System.Array.Exists(texture.tags, t => t == tag));
    }
}
