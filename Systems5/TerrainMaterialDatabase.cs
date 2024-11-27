using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainMaterialDatabase", menuName = "Terrain Material Database/Terrain Material Database")]
public class TerrainMaterialDatabase : ScriptableObject
{
    public List<TerrainMaterialMetadata> materials = new List<TerrainMaterialMetadata>();

    public void AddMaterial(TerrainMaterialMetadata materialData)
    {
        if (!materials.Contains(materialData))
        {
            materials.Add(materialData);
        }
    }

    public void RemoveMaterial(TerrainMaterialMetadata materialData)
    {
        if (materials.Contains(materialData))
        {
            materials.Remove(materialData);
        }
    }

    public TerrainMaterialMetadata FindMaterialByName(string name)
    {
        return materials.Find(material => material.materialName == name);
    }

    public List<TerrainMaterialMetadata> FindMaterialsByTerrainType(string terrainType)
    {
        return materials.FindAll(material => material.terrainType == terrainType);
    }

    public List<TerrainMaterialMetadata> FindMaterialsByTag(string tag)
    {
        return materials.FindAll(material => System.Array.Exists(material.tags, t => t == tag));
    }
}
