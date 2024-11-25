using UnityEngine;

public class ARLiveGraph : MonoBehaviour
{
    public LineRenderer lineRenderer;
    private float[] dataPoints = new float[100];
    private int index = 0;

    void Start()
    {
        // Initialize the LineRenderer with the correct number of positions
        lineRenderer.positionCount = dataPoints.Length;

        // Initialize data points to zero
        for (int i = 0; i < dataPoints.Length; i++)
        {
            dataPoints[i] = 0;
            lineRenderer.SetPosition(i, new Vector3(i, 0, 0));
        }
    }

    void Update()
    {
        float cpuUsage = GetCPUUsage(); // Replace with real function
        dataPoints[index] = cpuUsage;
        index = (index + 1) % dataPoints.Length;

        for (int i = 0; i < dataPoints.Length; i++)
        {
            // Wrap around the graph for smooth updating
            int wrappedIndex = (index + i) % dataPoints.Length;
            lineRenderer.SetPosition(i, new Vector3(i, dataPoints[wrappedIndex], 0));
        }
    }

    float GetCPUUsage()
    {
        // Mock function for testing
        return Random.Range(0, 100);
    }
}
