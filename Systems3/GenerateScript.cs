using System.IO;
using UnityEngine;

public class ScriptGenerator : MonoBehaviour
{
    public void GenerateScript(string className, string content)
    {
        string path = Application.dataPath + $"/Scripts/{className}.cs";
        File.WriteAllText(path, content);
        Debug.Log($"Script {className}.cs generated at {path}");
    }
}
