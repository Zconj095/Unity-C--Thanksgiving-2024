using UnityEngine;

public class TextureApplicator : MonoBehaviour
{
    public SeamlessTextureDatabase textureDatabase;
    public Renderer targetRenderer;

    public void ApplyTexture(string textureName)
    {
        var textureData = textureDatabase.FindTextureByName(textureName);
        if (textureData != null && textureData.textureFile != null)
        {
            targetRenderer.material.mainTexture = textureData.textureFile;
        }
        else
        {
            Debug.LogWarning($"Texture '{textureName}' not found in the database.");
        }
    }

    public void ApplyRandomTextureByTag(string tag)
    {
        var textures = textureDatabase.FindTexturesByTag(tag);
        if (textures.Count > 0)
        {
            var randomTexture = textures[Random.Range(0, textures.Count)];
            targetRenderer.material.mainTexture = randomTexture.textureFile;
        }
        else
        {
            Debug.LogWarning($"No textures with tag '{tag}' found.");
        }
    }
}
