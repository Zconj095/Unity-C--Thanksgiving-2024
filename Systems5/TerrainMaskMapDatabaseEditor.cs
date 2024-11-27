using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainMaskMapDatabase))]
public class TerrainMaskMapDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        TerrainMaskMapDatabase database = (TerrainMaskMapDatabase)target;

        GUILayout.Label("Terrain Mask Map Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Mask Maps", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var maskMap in database.maskMaps)
            {
                if (maskMap.maskMapName.Contains(searchFilter))
                {
                    Debug.Log($"Found Mask Map: {maskMap.maskMapName}");
                }
            }
        }

        // Add new mask map metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Mask Map Metadata"))
        {
            TerrainMaskMapMetadata newMaskMap = CreateInstance<TerrainMaskMapMetadata>();
            AssetDatabase.CreateAsset(newMaskMap, "Assets/NewTerrainMaskMapMetadata.asset");
            database.AddMaskMap(newMaskMap);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display mask maps
        GUILayout.Space(10);
        GUILayout.Label("Stored Mask Maps", EditorStyles.boldLabel);
        foreach (var maskMap in database.maskMaps)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(maskMap.maskMapName);
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = maskMap;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveMaskMap(maskMap);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
