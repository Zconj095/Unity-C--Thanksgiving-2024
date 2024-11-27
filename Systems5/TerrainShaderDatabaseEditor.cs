using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainShaderDatabase))]
public class TerrainShaderDatabaseEditor : Editor
{
    private string searchFilter = "";

    public override void OnInspectorGUI()
    {
        TerrainShaderDatabase database = (TerrainShaderDatabase)target;

        GUILayout.Label("Terrain Shader Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Shaders", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var shader in database.shaders)
            {
                if (shader.shaderName.Contains(searchFilter))
                {
                    Debug.Log($"Found Shader: {shader.shaderName}");
                }
            }
        }

        // Add new shader metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Shader Metadata"))
        {
            TerrainShaderMetadata newShader = CreateInstance<TerrainShaderMetadata>();
            AssetDatabase.CreateAsset(newShader, "Assets/NewTerrainShaderMetadata.asset");
            database.AddShader(newShader);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display shaders
        GUILayout.Space(10);
        GUILayout.Label("Stored Shaders", EditorStyles.boldLabel);
        foreach (var shader in database.shaders)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(shader.shaderName);
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = shader;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveShader(shader);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
