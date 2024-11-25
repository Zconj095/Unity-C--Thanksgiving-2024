using UnityEditor;
using UnityEngine;

public class AssetExplorer : MonoBehaviour
{
    public void ListAssets()
    {
        string[] assetPaths = AssetDatabase.GetAllAssetPaths();
        foreach (string path in assetPaths)
        {
            Debug.Log($"Asset: {path}");
        }
    }
}
