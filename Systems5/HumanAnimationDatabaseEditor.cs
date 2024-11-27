using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HumanAnimationDatabase))]
public class HumanAnimationDatabaseEditor : Editor
{
    private string searchFilter = "";
    private string searchTag = "";

    public override void OnInspectorGUI()
    {
        HumanAnimationDatabase database = (HumanAnimationDatabase)target;

        GUILayout.Label("Human Animation Database", EditorStyles.boldLabel);

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
            HumanAnimationMetadata newAnimation = CreateInstance<HumanAnimationMetadata>();
            AssetDatabase.CreateAsset(newAnimation, "Assets/NewHumanAnimationMetadata.asset");
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
