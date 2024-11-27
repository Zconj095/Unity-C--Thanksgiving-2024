using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ParticleTextureDatabase", menuName = "Particle Texture Database/Texture Database")]
public class ParticleTextureDatabase : ScriptableObject
{
    public List<ParticleTextureMetadata> textures = new List<ParticleTextureMetadata>();

    public void AddTexture(ParticleTextureMetadata texture)
    {
        if (!textures.Contains(texture))
        {
            textures.Add(texture);
        }
    }

    public void RemoveTexture(ParticleTextureMetadata texture)
    {
        if (textures.Contains(texture))
        {
            textures.Remove(texture);
        }
    }

    public ParticleTextureMetadata FindTextureByName(string name)
    {
        return textures.Find(texture => texture.textureName == name);
    }

    public List<ParticleTextureMetadata> FindTexturesByTag(string tag)
    {
        return textures.FindAll(texture => System.Array.Exists(texture.tags, t => t == tag));
    }
}
