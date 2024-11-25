using UnityEngine;
using UnityEngine.UI;

public class SystemMonitor : MonoBehaviour
{
    public Text cpuUsageText;
    public Text ramUsageText;

    private float mockCpuUsage;

    void Start()
    {
        // Updates system stats every second
        InvokeRepeating("UpdateStats", 1f, 1f);
    }

    void UpdateStats()
    {
        // Mock CPU Usage (replace this with actual logic if available)
        mockCpuUsage = Random.Range(0, 100);

        // Get total system memory
        float totalMemory = SystemInfo.systemMemorySize;

        // Mock available RAM by subtracting a random usage value
        float usedRam = Random.Range(1000, 4000); // Mock usage in MB
        float availableRam = Mathf.Max(0, totalMemory - usedRam);

        // Update UI
        if (cpuUsageText != null)
        {
            cpuUsageText.text = $"CPU Usage: {mockCpuUsage:0.0}%";
        }

        if (ramUsageText != null)
        {
            ramUsageText.text = $"Available RAM: {availableRam:0.0} MB";
        }
    }
}
