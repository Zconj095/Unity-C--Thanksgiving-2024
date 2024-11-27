using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainMaskMapDatabase", menuName = "Terrain Mask Map Database/Mask Map Database")]
public class TerrainMaskMapDatabase : ScriptableObject
{
    public List<TerrainMaskMapMetadata> maskMaps = new List<TerrainMaskMapMetadata>();

    public void AddMaskMap(TerrainMaskMapMetadata maskMap)
    {
        if (!maskMaps.Contains(maskMap))
        {
            maskMaps.Add(maskMap);
        }
    }

    public void RemoveMaskMap(TerrainMaskMapMetadata maskMap)
    {
        if (maskMaps.Contains(maskMap))
        {
            maskMaps.Remove(maskMap);
        }
    }

    public TerrainMaskMapMetadata FindMaskMapByName(string name)
    {
        return maskMaps.Find(maskMap => maskMap.maskMapName == name);
    }

    public List<TerrainMaskMapMetadata> FindMaskMapsByTerrainType(string terrainType)
    {
        return maskMaps.FindAll(maskMap => maskMap.terrainType == terrainType);
    }

    public List<TerrainMaskMapMetadata> FindMaskMapsByTag(string tag)
    {
        return maskMaps.FindAll(maskMap => System.Array.Exists(maskMap.tags, t => t == tag));
    }
}
