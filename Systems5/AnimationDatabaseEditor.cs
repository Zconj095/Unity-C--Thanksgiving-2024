using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(AnimationDatabase))]
public class AnimationDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchByTag = "";

    public override void OnInspectorGUI()
    {
        AnimationDatabase database = (AnimationDatabase)target;

        GUILayout.Label("Animation Database", EditorStyles.boldLabel);

        // Search by name
        GUILayout.Space(5);
        GUILayout.Label("Search Animations", EditorStyles.boldLabel);
        searchFilter = EditorGUILayout.TextField("Search by Name", searchFilter);

        if (GUILayout.Button("Search"))
        {
            foreach (var animation in database.animations)
            {
                if (animation.animationName.Contains(searchFilter))
                {
                    Debug.Log($"Found Animation: {animation.animationName}");
                }
            }
        }

        // Add new animation metadata
        GUILayout.Space(10);
        if (GUILayout.Button("Add Animation Metadata"))
        {
            AnimationMetadata newAnimation = CreateInstance<AnimationMetadata>();
            AssetDatabase.CreateAsset(newAnimation, "Assets/NewAnimationMetadata.asset");
            database.AddAnimation(newAnimation);
            EditorUtility.SetDirty(database);
        }

        // Save database
        if (GUILayout.Button("Save Database"))
        {
            EditorUtility.SetDirty(database);
            AssetDatabase.SaveAssets();
        }

        // Display animations
        GUILayout.Space(10);
        GUILayout.Label("Stored Animations", EditorStyles.boldLabel);
        foreach (var animation in database.animations)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(animation.animationName);
            if (GUILayout.Button("Select"))
            {
                Selection.activeObject = animation;
            }
            if (GUILayout.Button("Remove"))
            {
                database.RemoveAnimation(animation);
                EditorUtility.SetDirty(database);
            }
            GUILayout.EndHorizontal();
        }
    }
}
