using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FlowerDatabase))]
public class FlowerDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        FlowerDatabase database = (FlowerDatabase)target;

        GUILayout.Label("2D Flower Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Flowers", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var flower in database.flowers)
            {
                if (flower.flowerName.Contains(searchFilter))
                {
                    Debug.Log($"Found Flower: {flower.flowerName}");
                }
            }
        }

        // Add new flower metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Flower Metadata"))
        {
            FlowerMetadata newFlower = CreateInstance<FlowerMetadata>();
            AssetDatabase.CreateAsset(newFlower, "Assets/NewFlowerMetadata.asset");
            database.AddFlower(newFlower);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display flowers
        GUILayout.Space(10);
        GUILayout.Label("Stored Flowers", EditorStyles.boldLabel);
        foreach (var flower in database.flowers)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(flower.flowerName);
            if (flower.flowerFile != null)
            {
                GUILayout.Label(flower.flowerFile, GUILayout.Width(64), GUILayout.Height(64));
            }
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = flower;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveFlower(flower);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
