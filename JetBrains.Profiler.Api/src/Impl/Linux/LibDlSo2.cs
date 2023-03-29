using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JetBrains.Profiler.Api.Impl.Linux
{
  internal static class LibDlSo2
  {
    private const string LibraryName = "libdl.so.2"; // Note: Don't use libdl.so because no such library in clean system!

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern IntPtr dlopen([MarshalAs(UnmanagedType.LPStr)] string filename, int flags);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern IntPtr dlsym(IntPtr handle, [MarshalAs(UnmanagedType.LPStr)] string symbol);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern int dlclose(IntPtr handle);
  }
}