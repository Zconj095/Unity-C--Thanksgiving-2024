using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CrystalDatabase))]
public class CrystalDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        CrystalDatabase database = (CrystalDatabase)target;

        GUILayout.Label("2D Crystal Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Crystals", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var crystal in database.crystals)
            {
                if (crystal.crystalName.Contains(searchFilter))
                {
                    Debug.Log($"Found Crystal: {crystal.crystalName}");
                }
            }
        }

        // Add new crystal metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Crystal Metadata"))
        {
            CrystalMetadata newCrystal = CreateInstance<CrystalMetadata>();
            AssetDatabase.CreateAsset(newCrystal, "Assets/NewCrystalMetadata.asset");
            database.AddCrystal(newCrystal);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display crystals
        GUILayout.Space(10);
        GUILayout.Label("Stored Crystals", EditorStyles.boldLabel);
        foreach (var crystal in database.crystals)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(crystal.crystalName);
            if (crystal.crystalFile != null)
            {
                GUILayout.Label(crystal.crystalFile, GUILayout.Width(64), GUILayout.Height(64));
            }
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = crystal;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveCrystal(crystal);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
