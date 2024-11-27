using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainNormalMapDatabase", menuName = "Terrain Normal Map Database/Normal Map Database")]
public class TerrainNormalMapDatabase : ScriptableObject
{
    public List<TerrainNormalMapMetadata> normalMaps = new List<TerrainNormalMapMetadata>();

    public void AddNormalMap(TerrainNormalMapMetadata normalMap)
    {
        if (!normalMaps.Contains(normalMap))
        {
            normalMaps.Add(normalMap);
        }
    }

    public void RemoveNormalMap(TerrainNormalMapMetadata normalMap)
    {
        if (normalMaps.Contains(normalMap))
        {
            normalMaps.Remove(normalMap);
        }
    }

    public TerrainNormalMapMetadata FindNormalMapByName(string name)
    {
        return normalMaps.Find(normalMap => normalMap.normalMapName == name);
    }

    public List<TerrainNormalMapMetadata> FindNormalMapsByTerrainType(string terrainType)
    {
        return normalMaps.FindAll(normalMap => normalMap.terrainType == terrainType);
    }

    public List<TerrainNormalMapMetadata> FindNormalMapsByTag(string tag)
    {
        return normalMaps.FindAll(normalMap => System.Array.Exists(normalMap.tags, t => t == tag));
    }
}
