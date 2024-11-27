using UnityEngine;
using UnityEngine.TerrainTools;

public class TerrainTextureApplicator : MonoBehaviour
{
    public SeamlessTerrainTextureDatabase textureDatabase;
    public Terrain terrain;

    public void ApplyTexture(string textureName)
    {
        var textureData = textureDatabase.FindTextureByName(textureName);
        if (textureData != null && textureData.textureFile != null)
        {
            TerrainLayer layer = new TerrainLayer
            {
                diffuseTexture = textureData.textureFile,
                tileSize = new Vector2(textureData.resolution.x, textureData.resolution.y)
            };

            terrain.terrainData.terrainLayers = new TerrainLayer[] { layer };
        }
        else
        {
            Debug.LogWarning($"Texture '{textureName}' not found in the database.");
        }
    }

    public void ApplyRandomTextureByBiome(string biomeType)
    {
        var textures = textureDatabase.FindTexturesByBiome(biomeType);
        if (textures.Count > 0)
        {
            var randomTexture = textures[Random.Range(0, textures.Count)];
            TerrainLayer layer = new TerrainLayer
            {
                diffuseTexture = randomTexture.textureFile,
                tileSize = new Vector2(randomTexture.resolution.x, randomTexture.resolution.y)
            };

            terrain.terrainData.terrainLayers = new TerrainLayer[] { layer };
        }
        else
        {
            Debug.LogWarning($"No textures with biome '{biomeType}' found.");
        }
    }
}
