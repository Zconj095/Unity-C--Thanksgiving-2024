using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainNormalMapDatabase))]
public class TerrainNormalMapDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        TerrainNormalMapDatabase database = (TerrainNormalMapDatabase)target;

        GUILayout.Label("Terrain Normal Map Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Normal Maps", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var normalMap in database.normalMaps)
            {
                if (normalMap.normalMapName.Contains(searchFilter))
                {
                    Debug.Log($"Found Normal Map: {normalMap.normalMapName}");
                }
            }
        }

        // Add new normal map metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Normal Map Metadata"))
        {
            TerrainNormalMapMetadata newNormalMap = CreateInstance<TerrainNormalMapMetadata>();
            AssetDatabase.CreateAsset(newNormalMap, "Assets/NewTerrainNormalMapMetadata.asset");
            database.AddNormalMap(newNormalMap);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display normal maps
        GUILayout.Space(10);
        GUILayout.Label("Stored Normal Maps", EditorStyles.boldLabel);
        foreach (var normalMap in database.normalMaps)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(normalMap.normalMapName);
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = normalMap;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveNormalMap(normalMap);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
