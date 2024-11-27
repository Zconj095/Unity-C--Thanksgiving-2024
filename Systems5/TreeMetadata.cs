using UnityEngine;

[CreateAssetMenu(fileName = "TreeMetadata", menuName = "Tree Database/Tree Metadata")]
public class TreeMetadata : ScriptableObject
{
    public string treeName; // Name of the tree
    public Texture2D treeFile; // Reference to the 2D tree asset
    public Vector2Int resolution; // Resolution of the tree asset
    public string treeType; // e.g., "Oak", "Pine", "Palm"
    public string[] tags; // Tags for filtering, e.g., "Winter", "Tropical", "Evergreen"
    public string description; // Optional description of the tree
}
