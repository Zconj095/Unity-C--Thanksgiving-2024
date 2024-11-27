using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainBrushDatabase))]
public class TerrainBrushDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        TerrainBrushDatabase database = (TerrainBrushDatabase)target;

        GUILayout.Label("Terrain Brush Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Brushes", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var brush in database.brushes)
            {
                if (brush.brushName.Contains(searchFilter))
                {
                    Debug.Log($"Found Brush: {brush.brushName}");
                }
            }
        }

        // Add new brush metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Brush Metadata"))
        {
            TerrainBrushMetadata newBrush = CreateInstance<TerrainBrushMetadata>();
            AssetDatabase.CreateAsset(newBrush, "Assets/NewTerrainBrushMetadata.asset");
            database.AddBrush(newBrush);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display brushes
        GUILayout.Space(10);
        GUILayout.Label("Stored Brushes", EditorStyles.boldLabel);
        foreach (var brush in database.brushes)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(brush.brushName);
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = brush;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveBrush(brush);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
