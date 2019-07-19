using System;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Linux
{
  internal static class LibDlSo2
  {
    private const string LibraryName = "libdl.so.2"; // Note: Don't use libdl.so because no such library in clean system!

    [DllImport(LibraryName)]
    public static extern IntPtr dlopen([MarshalAs(UnmanagedType.LPStr)] string filename, int flags);

    [DllImport(LibraryName)]
    public static extern int dlclose(IntPtr handle);
  }
}