using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BushDatabase", menuName = "Bush Database/Database")]
public class BushDatabase : ScriptableObject
{
    public List<BushMetadata> bushes = new List<BushMetadata>();

    public void AddBush(BushMetadata bush)
    {
        if (!bushes.Contains(bush))
        {
            bushes.Add(bush);
        }
    }

    public void RemoveBush(BushMetadata bush)
    {
        if (bushes.Contains(bush))
        {
            bushes.Remove(bush);
        }
    }

    public BushMetadata FindBushByName(string name)
    {
        return bushes.Find(bush => bush.bushName == name);
    }

    public List<BushMetadata> FindBushesByTag(string tag)
    {
        return bushes.FindAll(bush => System.Array.Exists(bush.tags, t => t == tag));
    }

    public List<BushMetadata> FindBushesByType(string type)
    {
        return bushes.FindAll(bush => bush.bushType == type);
    }
}
