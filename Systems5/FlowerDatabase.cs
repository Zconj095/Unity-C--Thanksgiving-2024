using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FlowerDatabase", menuName = "Flower Database/Database")]
public class FlowerDatabase : ScriptableObject
{
    public List<FlowerMetadata> flowers = new List<FlowerMetadata>();

    public void AddFlower(FlowerMetadata flower)
    {
        if (!flowers.Contains(flower))
        {
            flowers.Add(flower);
        }
    }

    public void RemoveFlower(FlowerMetadata flower)
    {
        if (flowers.Contains(flower))
        {
            flowers.Remove(flower);
        }
    }

    public FlowerMetadata FindFlowerByName(string name)
    {
        return flowers.Find(flower => flower.flowerName == name);
    }

    public List<FlowerMetadata> FindFlowersByTag(string tag)
    {
        return flowers.FindAll(flower => System.Array.Exists(flower.tags, t => t == tag));
    }

    public List<FlowerMetadata> FindFlowersByType(string type)
    {
        return flowers.FindAll(flower => flower.flowerType == type);
    }
}
