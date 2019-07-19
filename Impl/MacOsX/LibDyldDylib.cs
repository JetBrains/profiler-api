using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JetBrains.Profiler.Api.Impl.MacOsX
{
  internal static class LibDyldDylib
  {
    private const string LibraryName = "/usr/lib/system/libdyld.dylib";

    [DllImport(LibraryName)]
    public static extern IntPtr dlopen([MarshalAs(UnmanagedType.LPStr)] string filename, int flags);

    [DllImport(LibraryName)]
    public static extern int dlclose(IntPtr handle);
  }
}