using UnityEngine;

public class SpatialCognition : MonoBehaviour
{
    public int gridSize = 10;
    private float[,] spatialMap;

    void Start()
    {
        spatialMap = new float[gridSize, gridSize];
        InitializeSpatialMap();
    }

    void InitializeSpatialMap()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                spatialMap[x, y] = Random.Range(0.0f, 1.0f); // Random spatial features
            }
        }
    }

    public float GetSpatialInfluence(int x, int y)
    {
        if (x >= 0 && x < gridSize && y >= 0 && y < gridSize)
        {
            return spatialMap[x, y];
        }
        return 0.0f; // Out of bounds
    }

    void OnDrawGizmos()
    {
        if (spatialMap == null) return;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Gizmos.color = Color.Lerp(Color.black, Color.white, spatialMap[x, y]);
                Gizmos.DrawCube(new Vector3(x, 0, y), Vector3.one * 0.9f);
            }
        }
    }
}
