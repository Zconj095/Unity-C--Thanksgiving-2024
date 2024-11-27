using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CrystalDatabase", menuName = "Crystal Database/Database")]
public class CrystalDatabase : ScriptableObject
{
    public List<CrystalMetadata> crystals = new List<CrystalMetadata>();

    public void AddCrystal(CrystalMetadata crystal)
    {
        if (!crystals.Contains(crystal))
        {
            crystals.Add(crystal);
        }
    }

    public void RemoveCrystal(CrystalMetadata crystal)
    {
        if (crystals.Contains(crystal))
        {
            crystals.Remove(crystal);
        }
    }

    public CrystalMetadata FindCrystalByName(string name)
    {
        return crystals.Find(crystal => crystal.crystalName == name);
    }

    public List<CrystalMetadata> FindCrystalsByTag(string tag)
    {
        return crystals.FindAll(crystal => System.Array.Exists(crystal.tags, t => t == tag));
    }

    public List<CrystalMetadata> FindCrystalsByType(string type)
    {
        return crystals.FindAll(crystal => crystal.crystalType == type);
    }
}
