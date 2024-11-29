using UnityEngine;

public class FrequencyFluxRelayScan : MonoBehaviour
{
    public int gridSize = 10;
    public float scanRadius = 5.0f;
    public float frequencyAmplitude = 10.0f;
    public GameObject cellPrefab;
    private GameObject[,] grid;

    void Start()
    {
        InitializeGrid();
        PerformScan();
    }

    void InitializeGrid()
    {
        grid = new GameObject[gridSize, gridSize];
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 position = new Vector3(x - gridSize / 2, 0, y - gridSize / 2);
                grid[x, y] = Instantiate(cellPrefab, position, Quaternion.identity);
            }
        }
    }

    void PerformScan()
    {
        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                float fluxValue = SimulateFluxValue(x, y);
                UpdateCellVisual(grid[x, y], fluxValue);
            }
        }
    }

    float SimulateFluxValue(int x, int y)
    {
        float distance = Vector3.Distance(new Vector3(x, 0, y), Vector3.zero);
        return Mathf.Max(0, frequencyAmplitude - distance * scanRadius);
    }

    void UpdateCellVisual(GameObject cell, float fluxValue)
    {
        Renderer renderer = cell.GetComponent<Renderer>();
        float intensity = fluxValue / frequencyAmplitude;
        renderer.material.color = new Color(intensity, 0, 1 - intensity);
    }
}
