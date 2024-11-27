using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SeamlessTextureDatabase))]
public class SeamlessTextureDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        SeamlessTextureDatabase database = (SeamlessTextureDatabase)target;

        GUILayout.Label("Seamless Texture Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Textures", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var texture in database.textures)
            {
                if (texture.textureName.Contains(searchFilter))
                {
                    Debug.Log($"Found Texture: {texture.textureName}");
                }
            }
        }

        // Add new texture metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Texture Metadata"))
        {
            SeamlessTextureMetadata newTexture = CreateInstance<SeamlessTextureMetadata>();
            AssetDatabase.CreateAsset(newTexture, "Assets/NewSeamlessTextureMetadata.asset");
            database.AddTexture(newTexture);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display textures
        GUILayout.Space(10);
        GUILayout.Label("Stored Textures", EditorStyles.boldLabel);
        foreach (var texture in database.textures)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(texture.textureName);
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = texture;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveTexture(texture);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
