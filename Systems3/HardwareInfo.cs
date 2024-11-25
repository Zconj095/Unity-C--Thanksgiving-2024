using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.IO;

public static class HardwareInfo
{
    public static long GetL1CacheSize()
    {
#if WINDOWS
        return GetWindowsCacheSize(1);
#elif LINUX
        return GetLinuxCacheSize(1);
#elif OSX
        return GetMacOSCacheSize("hw.l1icachesize");
#else
        Console.WriteLine("Cache size retrieval not supported on this platform.");
        return 0;
#endif
    }

    public static long GetL2CacheSize()
    {
#if WINDOWS
        return GetWindowsCacheSize(2);
#elif LINUX
        return GetLinuxCacheSize(2);
#elif OSX
        return GetMacOSCacheSize("hw.l2cachesize");
#else
        Console.WriteLine("Cache size retrieval not supported on this platform.");
        return 0;
#endif
    }

    public static long GetL3CacheSize()
    {
#if WINDOWS
        return GetWindowsCacheSize(3);
#elif LINUX
        return GetLinuxCacheSize(3);
#elif OSX
        return GetMacOSCacheSize("hw.l3cachesize");
#else
        Console.WriteLine("Cache size retrieval not supported on this platform.");
        return 0;
#endif
    }

    /// <summary>
    /// Retrieves cache size on Windows using the native API.
    /// </summary>
    private static long GetWindowsCacheSize(int cacheLevel)
    {
        try
        {
            SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX[] buffer = GetProcessorInformation();
            foreach (var info in buffer)
            {
                if (info.Relationship == LOGICAL_PROCESSOR_RELATIONSHIP.RelationCache && info.Cache.Level == cacheLevel)
                {
                    return info.Cache.Size;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving L{cacheLevel} cache size on Windows: {ex.Message}");
        }
        return 0;
    }

    /// <summary>
    /// Retrieves cache size on Linux by reading /sys/devices/system/cpu/cpu*/cache/.
    /// </summary>
    private static long GetLinuxCacheSize(int cacheLevel)
    {
        try
        {
            string path = $"/sys/devices/system/cpu/cpu0/cache/index{cacheLevel - 1}/size";
            if (File.Exists(path))
            {
                string sizeStr = File.ReadAllText(path).Trim();
                if (sizeStr.EndsWith("K")) // Handle KB
                {
                    return long.Parse(sizeStr.TrimEnd('K')) * 1024;
                }
                else if (sizeStr.EndsWith("M")) // Handle MB
                {
                    return long.Parse(sizeStr.TrimEnd('M')) * 1024 * 1024;
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving L{cacheLevel} cache size on Linux: {ex.Message}");
        }
        return 0;
    }

    /// <summary>
    /// Retrieves cache size on macOS using the sysctl command.
    /// </summary>
    private static long GetMacOSCacheSize(string sysctlKey)
    {
        try
        {
            string output = ExecuteBashCommand($"sysctl {sysctlKey}");
            string[] parts = output.Split(':');
            if (parts.Length == 2 && long.TryParse(parts[1].Trim(), out long bytes))
            {
                return bytes;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error retrieving {sysctlKey} cache size on macOS: {ex.Message}");
        }
        return 0;
    }

    /// <summary>
    /// Executes a bash command and returns the output.
    /// </summary>
    private static string ExecuteBashCommand(string command)
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "/bin/bash",
            Arguments = $"-c \"{command}\"",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(startInfo))
        {
            process.WaitForExit();
            return process.StandardOutput.ReadToEnd();
        }
    }

    /// <summary>
    /// Calls the Windows API to retrieve logical processor information.
    /// </summary>
    [DllImport("kernel32.dll")]
    private static extern bool GetLogicalProcessorInformationEx(
        LOGICAL_PROCESSOR_RELATIONSHIP relationshipType,
        IntPtr buffer,
        ref uint returnedLength);

    private static SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX[] GetProcessorInformation()
    {
        uint length = 0;
        GetLogicalProcessorInformationEx(LOGICAL_PROCESSOR_RELATIONSHIP.RelationAll, IntPtr.Zero, ref length);
        IntPtr buffer = Marshal.AllocHGlobal((int)length);
        try
        {
            if (GetLogicalProcessorInformationEx(LOGICAL_PROCESSOR_RELATIONSHIP.RelationAll, buffer, ref length))
            {
                List<SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX> infoList = new List<SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX>();
                long offset = 0;
                while (offset < length)
                {
                    SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX info = Marshal.PtrToStructure<SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX>(buffer + (int)offset);
                    infoList.Add(info);
                    offset += info.Size;
                }
                return infoList.ToArray();
            }
        }
        finally
        {
            Marshal.FreeHGlobal(buffer);
        }
        return Array.Empty<SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX>();
    }

    private enum LOGICAL_PROCESSOR_RELATIONSHIP
    {
        RelationCache,
        RelationAll
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct SYSTEM_LOGICAL_PROCESSOR_INFORMATION_EX
    {
        public LOGICAL_PROCESSOR_RELATIONSHIP Relationship;
        public uint Size;
        public CACHE_DESCRIPTOR Cache;
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct CACHE_DESCRIPTOR
    {
        public byte Level;
        public uint Size; // Size in bytes
    }
}
