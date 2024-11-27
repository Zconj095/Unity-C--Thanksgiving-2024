using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainDatabase))]
public class TerrainDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchBiome = "";

    public override void OnInspectorGUI()
    {
        TerrainDatabase database = (TerrainDatabase)target;

        GUILayout.Label("Terrain Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Terrains", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var terrain in database.terrains)
            {
                if (terrain.terrainName.Contains(searchFilter))
                {
                    Debug.Log($"Found Terrain: {terrain.terrainName}");
                }
            }
        }

        // Add new terrain metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Terrain Metadata"))
        {
            TerrainMetadata newTerrain = CreateInstance<TerrainMetadata>();
            AssetDatabase.CreateAsset(newTerrain, "Assets/NewTerrainMetadata.asset");
            database.AddTerrain(newTerrain);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display terrains
        GUILayout.Space(10);
        GUILayout.Label("Stored Terrains", EditorStyles.boldLabel);
        foreach (var terrain in database.terrains)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(terrain.terrainName);
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = terrain;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveTerrain(terrain);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
