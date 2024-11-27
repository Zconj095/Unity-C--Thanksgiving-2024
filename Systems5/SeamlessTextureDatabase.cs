using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SeamlessTextureDatabase", menuName = "Seamless Texture Database/Texture Database")]
public class SeamlessTextureDatabase : ScriptableObject
{
    public List<SeamlessTextureMetadata> textures = new List<SeamlessTextureMetadata>();

    public void AddTexture(SeamlessTextureMetadata texture)
    {
        if (!textures.Contains(texture))
        {
            textures.Add(texture);
        }
    }

    public void RemoveTexture(SeamlessTextureMetadata texture)
    {
        if (textures.Contains(texture))
        {
            textures.Remove(texture);
        }
    }

    public SeamlessTextureMetadata FindTextureByName(string name)
    {
        return textures.Find(texture => texture.textureName == name);
    }

    public List<SeamlessTextureMetadata> FindTexturesByTag(string tag)
    {
        return textures.FindAll(texture => System.Array.Exists(texture.tags, t => t == tag));
    }
}
