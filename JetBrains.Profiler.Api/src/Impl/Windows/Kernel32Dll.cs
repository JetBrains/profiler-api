using System;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Windows
{
  internal static class Kernel32Dll
  {
    private const string LibraryName = "kernel32.dll";

    [DllImport(LibraryName, ExactSpelling = true, SetLastError = true)]
    internal static extern IntPtr GetModuleHandleW([MarshalAs(UnmanagedType.LPWStr)] string lpModuleName);
  }
}