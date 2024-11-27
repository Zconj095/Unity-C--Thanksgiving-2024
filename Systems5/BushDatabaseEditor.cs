using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BushDatabase))]
public class BushDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        BushDatabase database = (BushDatabase)target;

        GUILayout.Label("2D Bush Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Bushes", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var bush in database.bushes)
            {
                if (bush.bushName.Contains(searchFilter))
                {
                    Debug.Log($"Found Bush: {bush.bushName}");
                }
            }
        }

        // Add new bush metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Bush Metadata"))
        {
            BushMetadata newBush = CreateInstance<BushMetadata>();
            AssetDatabase.CreateAsset(newBush, "Assets/NewBushMetadata.asset");
            database.AddBush(newBush);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display bushes
        GUILayout.Space(10);
        GUILayout.Label("Stored Bushes", EditorStyles.boldLabel);
        foreach (var bush in database.bushes)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(bush.bushName);
            if (bush.bushFile != null)
            {
                GUILayout.Label(bush.bushFile, GUILayout.Width(64), GUILayout.Height(64));
            }
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = bush;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveBush(bush);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
