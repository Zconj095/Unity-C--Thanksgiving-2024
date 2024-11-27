using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TransparentTextureDatabase", menuName = "Transparent Texture Database/Texture Database")]
public class TransparentTextureDatabase : ScriptableObject
{
    public List<TransparentTextureMetadata> textures = new List<TransparentTextureMetadata>();

    public void AddTexture(TransparentTextureMetadata texture)
    {
        if (!textures.Contains(texture))
        {
            textures.Add(texture);
        }
    }

    public void RemoveTexture(TransparentTextureMetadata texture)
    {
        if (textures.Contains(texture))
        {
            textures.Remove(texture);
        }
    }

    public TransparentTextureMetadata FindTextureByName(string name)
    {
        return textures.Find(texture => texture.textureName == name);
    }

    public List<TransparentTextureMetadata> FindTexturesByTag(string tag)
    {
        return textures.FindAll(texture => System.Array.Exists(texture.tags, t => t == tag));
    }
}
