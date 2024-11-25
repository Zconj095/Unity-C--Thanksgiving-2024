using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class PerformanceMonitor : MonoBehaviour
{
    private float memoryUsage;
    private float processingTime;

    void Update()
    {
        memoryUsage = System.GC.GetTotalMemory(false) / (1024f * 1024f); // Memory in MB
        processingTime = Time.deltaTime * 1000; // Frame processing time in ms
        Debug.Log($"Memory Usage: {memoryUsage} MB, Processing Time: {processingTime} ms");
    }
}
