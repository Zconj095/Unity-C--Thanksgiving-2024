using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class ARSystemStats : MonoBehaviour
{
    public Text cpuText;
    public Text ramText;

#if UNITY_STANDALONE_WIN
    [DllImport("kernel32.dll", SetLastError = true)]
    private static extern bool GlobalMemoryStatusEx(ref MEMORYSTATUSEX lpBuffer);

    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    private struct MEMORYSTATUSEX
    {
        public uint dwLength;
        public uint dwMemoryLoad;
        public ulong ullTotalPhys;
        public ulong ullAvailPhys;
        public ulong ullTotalPageFile;
        public ulong ullAvailPageFile;
        public ulong ullTotalVirtual;
        public ulong ullAvailVirtual;
        public ulong ullAvailExtendedVirtual;

        public static MEMORYSTATUSEX Create()
        {
            return new MEMORYSTATUSEX
            {
                dwLength = (uint)Marshal.SizeOf(typeof(MEMORYSTATUSEX)),
                dwMemoryLoad = 0,
                ullTotalPhys = 0,
                ullAvailPhys = 0,
                ullTotalPageFile = 0,
                ullAvailPageFile = 0,
                ullTotalVirtual = 0,
                ullAvailVirtual = 0,
                ullAvailExtendedVirtual = 0
            };
        }
    }


    private long prevIdleTime = 0;
    private long prevKernelTime = 0;
    private long prevUserTime = 0;

    [DllImport("kernel32.dll")]
    private static extern void GetSystemTimes(out long idleTime, out long kernelTime, out long userTime);

    void Start()
    {
        InvokeRepeating("UpdateStats", 1f, 1f);
    }

    void UpdateStats()
    {
        float cpuUsage = GetCpuUsage();
        float availableRAM = GetAvailableRAM();

        cpuText.text = $"CPU Usage: {cpuUsage:0.0}%";
        ramText.text = $"Available RAM: {availableRAM:0.0} MB";
    }

    private float GetCpuUsage()
    {
        GetSystemTimes(out long idleTime, out long kernelTime, out long userTime);

        long idleDiff = idleTime - prevIdleTime;
        long kernelDiff = kernelTime - prevKernelTime;
        long userDiff = userTime - prevUserTime;

        prevIdleTime = idleTime;
        prevKernelTime = kernelTime;
        prevUserTime = userTime;

        long total = kernelDiff + userDiff;

        return total == 0 ? 0 : (1 - (float)idleDiff / total) * 100;
    }

    private float GetAvailableRAM()
    {
        MEMORYSTATUSEX memStatus = new MEMORYSTATUSEX();
        if (GlobalMemoryStatusEx(ref memStatus))
        {
            return (float)(memStatus.ullAvailPhys / (1024.0 * 1024.0)); // Convert bytes to MB
        }
        return 0f;
    }
#else
    void Start()
    {
        Debug.LogError("System statistics are only supported on Windows.");
    }
#endif
}
