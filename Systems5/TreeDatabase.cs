using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TreeDatabase", menuName = "Tree Database/Database")]
public class TreeDatabase : ScriptableObject
{
    public List<TreeMetadata> trees = new List<TreeMetadata>();

    public void AddTree(TreeMetadata tree)
    {
        if (!trees.Contains(tree))
        {
            trees.Add(tree);
        }
    }

    public void RemoveTree(TreeMetadata tree)
    {
        if (trees.Contains(tree))
        {
            trees.Remove(tree);
        }
    }

    public TreeMetadata FindTreeByName(string name)
    {
        return trees.Find(tree => tree.treeName == name);
    }

    public List<TreeMetadata> FindTreesByTag(string tag)
    {
        return trees.FindAll(tree => System.Array.Exists(tree.tags, t => t == tag));
    }

    public List<TreeMetadata> FindTreesByType(string type)
    {
        return trees.FindAll(tree => tree.treeType == type);
    }
}
