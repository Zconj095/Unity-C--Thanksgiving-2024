using System.Diagnostics;
using System;
public class DimmdriveManager
{
    public static void CreateRamDisk(string driveLetter, int sizeMB)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "dimmdrive.exe",
            Arguments = $"-createDisk driveLetter={driveLetter} size={sizeMB}MB",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(psi))
        {
            process.WaitForExit();
            Console.WriteLine(process.StandardOutput.ReadToEnd());
        }
    }

    public static void DeleteRamDisk(string driveLetter)
    {
        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = "dimmdrive.exe",
            Arguments = $"-deleteDisk driveLetter={driveLetter}",
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(psi))
        {
            process.WaitForExit();
            Console.WriteLine(process.StandardOutput.ReadToEnd());
        }
    }
}
