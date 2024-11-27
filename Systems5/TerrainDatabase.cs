using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainDatabase", menuName = "Terrain Database/Terrain Database")]
public class TerrainDatabase : ScriptableObject
{
    public List<TerrainMetadata> terrains = new List<TerrainMetadata>();

    public void AddTerrain(TerrainMetadata terrainData)
    {
        if (!terrains.Contains(terrainData))
        {
            terrains.Add(terrainData);
        }
    }

    public void RemoveTerrain(TerrainMetadata terrainData)
    {
        if (terrains.Contains(terrainData))
        {
            terrains.Remove(terrainData);
        }
    }

    public TerrainMetadata FindTerrainByName(string name)
    {
        return terrains.Find(terrain => terrain.terrainName == name);
    }

    public List<TerrainMetadata> FindTerrainsByBiome(string biomeType)
    {
        return terrains.FindAll(terrain => terrain.biomeType == biomeType);
    }

    public List<TerrainMetadata> FindTerrainsByTag(string tag)
    {
        return terrains.FindAll(terrain => System.Array.Exists(terrain.tags, t => t == tag));
    }
}
