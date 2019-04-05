using System;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.MacOsX
{
  internal static class LibNCursesDylib
  {
    private const string LibraryName = "/usr/lib/libncurses.dylib";

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern IntPtr dlopen([MarshalAs(UnmanagedType.LPStr)] string filename, int flags);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern int dlclose(IntPtr handle);
  }
}