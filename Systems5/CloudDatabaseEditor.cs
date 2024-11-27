using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CloudDatabase))]
public class CloudDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        CloudDatabase database = (CloudDatabase)target;

        GUILayout.Label("2D Cloud Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Clouds", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var cloud in database.clouds)
            {
                if (cloud.cloudName.Contains(searchFilter))
                {
                    Debug.Log($"Found Cloud: {cloud.cloudName}");
                }
            }
        }

        // Add new cloud metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Cloud Metadata"))
        {
            CloudMetadata newCloud = CreateInstance<CloudMetadata>();
            AssetDatabase.CreateAsset(newCloud, "Assets/NewCloudMetadata.asset");
            database.AddCloud(newCloud);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display clouds
        GUILayout.Space(10);
        GUILayout.Label("Stored Clouds", EditorStyles.boldLabel);
        foreach (var cloud in database.clouds)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(cloud.cloudName);
            if (cloud.cloudFile != null)
            {
                GUILayout.Label(cloud.cloudFile, GUILayout.Width(64), GUILayout.Height(64));
            }
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = cloud;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveCloud(cloud);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
