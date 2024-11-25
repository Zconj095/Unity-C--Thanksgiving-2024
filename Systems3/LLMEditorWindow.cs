using UnityEditor;
using UnityEngine;

public class LLMEditorWindow : EditorWindow
{
    private string inputPrompt = "Describe your problem...";
    private string outputResponse;

    [MenuItem("Tools/LLM Helper")]
    public static void ShowWindow()
    {
        GetWindow<LLMEditorWindow>("LLM Helper");
    }

    void OnGUI()
    {
        GUILayout.Label("LLM Integration", EditorStyles.boldLabel);
        inputPrompt = EditorGUILayout.TextField("Prompt", inputPrompt);

        if (GUILayout.Button("Send to LLM"))
        {
            // Simulate LLM response (Replace with actual API call)
            outputResponse = $"Response to: {inputPrompt}";
        }

        EditorGUILayout.LabelField("Response", outputResponse);
    }
}
