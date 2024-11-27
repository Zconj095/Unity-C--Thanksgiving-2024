using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ShaderDatabase))]
public class ShaderDatabaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        ShaderDatabase database = (ShaderDatabase)target;

        GUILayout.Label("Shader Database", EditorStyles.boldLabel);

        if (GUILayout.Button("Add Shader"))
        {
            ShaderMetadata newShader = CreateInstance<ShaderMetadata>();
            AssetDatabase.CreateAsset(newShader, "Assets/NewShaderMetadata.asset");
            database.AddShader(newShader);
            EditorUtility.SetDirty(database);
        }

        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        GUILayout.Space(10);
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
