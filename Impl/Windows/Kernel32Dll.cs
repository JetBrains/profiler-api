using System;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Windows
{
  internal static class Kernel32Dll
  {
    private const string LibraryName = "kernel32.dll";

    [DllImport(LibraryName, ExactSpelling = true, SetLastError = true)]
    public static extern uint GetCurrentProcessId();

    [DllImport(LibraryName, ExactSpelling = true, SetLastError = true)]
    internal static extern IntPtr OpenEventW(uint dwDesiredAccess, bool inheritHandle, [MarshalAs(UnmanagedType.LPWStr)] string name);

    [DllImport(LibraryName, ExactSpelling = true, SetLastError = true)]
    public static extern uint WaitForSingleObject(IntPtr hHandle, uint dwMilliseconds);

    [DllImport(LibraryName, ExactSpelling = true, SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    public static extern bool CloseHandle(IntPtr hObject);

    [DllImport(LibraryName, ExactSpelling = true, SetLastError = true)]
    public static extern IntPtr GetModuleHandleW([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
  }
}