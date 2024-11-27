using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TerrainMaterialDatabase))]
public class TerrainMaterialDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        TerrainMaterialDatabase database = (TerrainMaterialDatabase)target;

        GUILayout.Label("Terrain Material Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Materials", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var material in database.materials)
            {
                if (material.materialName.Contains(searchFilter))
                {
                    Debug.Log($"Found Material: {material.materialName}");
                }
            }
        }

        // Add new material metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Material Metadata"))
        {
            TerrainMaterialMetadata newMaterial = CreateInstance<TerrainMaterialMetadata>();
            AssetDatabase.CreateAsset(newMaterial, "Assets/NewTerrainMaterialMetadata.asset");
            database.AddMaterial(newMaterial);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display materials
        GUILayout.Space(10);
        GUILayout.Label("Stored Materials", EditorStyles.boldLabel);
        foreach (var material in database.materials)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(material.materialName);
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = material;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveMaterial(material);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
