using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TerrainBrushDatabase", menuName = "Terrain Brush Database/Brush Database")]
public class TerrainBrushDatabase : ScriptableObject
{
    public List<TerrainBrushMetadata> brushes = new List<TerrainBrushMetadata>();

    public void AddBrush(TerrainBrushMetadata brush)
    {
        if (!brushes.Contains(brush))
        {
            brushes.Add(brush);
        }
    }

    public void RemoveBrush(TerrainBrushMetadata brush)
    {
        if (brushes.Contains(brush))
        {
            brushes.Remove(brush);
        }
    }

    public TerrainBrushMetadata FindBrushByName(string name)
    {
        return brushes.Find(brush => brush.brushName == name);
    }

    public List<TerrainBrushMetadata> FindBrushesByTag(string tag)
    {
        return brushes.FindAll(brush => System.Array.Exists(brush.tags, t => t == tag));
    }
}
