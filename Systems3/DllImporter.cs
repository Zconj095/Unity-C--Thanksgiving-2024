using System;
using System.Runtime.InteropServices;

public class DllImporter
{
    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr LoadLibrary(string dllToLoad);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern IntPtr GetProcAddress(IntPtr hModule, string procedureName);

    [DllImport("kernel32.dll", SetLastError = true)]
    public static extern bool FreeLibrary(IntPtr hModule);

    public static T GetFunction<T>(string dllPath, string functionName) where T : Delegate
    {
        IntPtr hModule = LoadLibrary(dllPath);
        if (hModule == IntPtr.Zero) throw new Exception("Failed to load DLL.");

        IntPtr functionPointer = GetProcAddress(hModule, functionName);
        if (functionPointer == IntPtr.Zero) throw new Exception($"Function {functionName} not found in DLL.");

        return Marshal.GetDelegateForFunctionPointer<T>(functionPointer);
    }
}
