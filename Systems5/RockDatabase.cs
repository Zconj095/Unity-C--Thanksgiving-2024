using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RockDatabase", menuName = "Rock Database/Database")]
public class RockDatabase : ScriptableObject
{
    public List<RockMetadata> rocks = new List<RockMetadata>();

    public void AddRock(RockMetadata rock)
    {
        if (!rocks.Contains(rock))
        {
            rocks.Add(rock);
        }
    }

    public void RemoveRock(RockMetadata rock)
    {
        if (rocks.Contains(rock))
        {
            rocks.Remove(rock);
        }
    }

    public RockMetadata FindRockByName(string name)
    {
        return rocks.Find(rock => rock.rockName == name);
    }

    public List<RockMetadata> FindRocksByTag(string tag)
    {
        return rocks.FindAll(rock => System.Array.Exists(rock.tags, t => t == tag));
    }

    public List<RockMetadata> FindRocksByType(string type)
    {
        return rocks.FindAll(rock => rock.rockType == type);
    }
}
