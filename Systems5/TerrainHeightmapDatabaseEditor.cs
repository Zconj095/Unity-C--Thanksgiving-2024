using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainHeightmapDatabase))]
public class TerrainHeightmapDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        TerrainHeightmapDatabase database = (TerrainHeightmapDatabase)target;

        GUILayout.Label("Terrain Heightmap Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Heightmaps", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var heightmap in database.heightmaps)
            {
                if (heightmap.heightmapName.Contains(searchFilter))
                {
                    Debug.Log($"Found Heightmap: {heightmap.heightmapName}");
                }
            }
        }

        // Add new heightmap metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Heightmap Metadata"))
        {
            TerrainHeightmapMetadata newHeightmap = CreateInstance<TerrainHeightmapMetadata>();
            AssetDatabase.CreateAsset(newHeightmap, "Assets/NewTerrainHeightmapMetadata.asset");
            database.AddHeightmap(newHeightmap);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display heightmaps
        GUILayout.Space(10);
        GUILayout.Label("Stored Heightmaps", EditorStyles.boldLabel);
        foreach (var heightmap in database.heightmaps)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(heightmap.heightmapName);
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = heightmap;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveHeightmap(heightmap);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
