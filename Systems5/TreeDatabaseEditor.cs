using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TreeDatabase))]
public class TreeDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        TreeDatabase database = (TreeDatabase)target;

        GUILayout.Label("2D Tree Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Trees", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var tree in database.trees)
            {
                if (tree.treeName.Contains(searchFilter))
                {
                    Debug.Log($"Found Tree: {tree.treeName}");
                }
            }
        }

        // Add new tree metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Tree Metadata"))
        {
            TreeMetadata newTree = CreateInstance<TreeMetadata>();
            AssetDatabase.CreateAsset(newTree, "Assets/NewTreeMetadata.asset");
            database.AddTree(newTree);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display trees
        GUILayout.Space(10);
        GUILayout.Label("Stored Trees", EditorStyles.boldLabel);
        foreach (var tree in database.trees)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(tree.treeName);
            if (tree.treeFile != null)
            {
                GUILayout.Label(tree.treeFile, GUILayout.Width(64), GUILayout.Height(64));
            }
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = tree;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveTree(tree);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
