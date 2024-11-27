using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainHeightmapDatabase", menuName = "Terrain Heightmap Database/Heightmap Database")]
public class TerrainHeightmapDatabase : ScriptableObject
{
    public List<TerrainHeightmapMetadata> heightmaps = new List<TerrainHeightmapMetadata>();

    public void AddHeightmap(TerrainHeightmapMetadata heightmap)
    {
        if (!heightmaps.Contains(heightmap))
        {
            heightmaps.Add(heightmap);
        }
    }

    public void RemoveHeightmap(TerrainHeightmapMetadata heightmap)
    {
        if (heightmaps.Contains(heightmap))
        {
            heightmaps.Remove(heightmap);
        }
    }

    public TerrainHeightmapMetadata FindHeightmapByName(string name)
    {
        return heightmaps.Find(heightmap => heightmap.heightmapName == name);
    }

    public List<TerrainHeightmapMetadata> FindHeightmapsByTerrainType(string terrainType)
    {
        return heightmaps.FindAll(heightmap => heightmap.terrainType == terrainType);
    }

    public List<TerrainHeightmapMetadata> FindHeightmapsByTag(string tag)
    {
        return heightmaps.FindAll(heightmap => System.Array.Exists(heightmap.tags, t => t == tag));
    }
}
