using System.Diagnostics;

public class DaemonToolsManager
{
    private const string DaemonToolsPath = @"C:\Program Files\DAEMON Tools Lite\dtlite.exe";

    public static void MountISO(string isoPath, int driveIndex = 0)
    {
        string arguments = $"-mount dt, {driveIndex}, \"{isoPath}\"";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = DaemonToolsPath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(psi))
        {
            process.WaitForExit();
            UnityEngine.Debug.Log(process.StandardOutput.ReadToEnd());
        }
    }

    public static void UnmountISO(int driveIndex = 0)
    {
        string arguments = $"-unmount {driveIndex}";

        ProcessStartInfo psi = new ProcessStartInfo
        {
            FileName = DaemonToolsPath,
            Arguments = arguments,
            RedirectStandardOutput = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        using (Process process = Process.Start(psi))
        {
            process.WaitForExit();
            UnityEngine.Debug.Log(process.StandardOutput.ReadToEnd());
        }
    }

    
}
