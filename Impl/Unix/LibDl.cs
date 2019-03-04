using System;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Unix
{
  public static class LibDl
  {
    private const string LibraryName = "libdl"; // Note: No extension here, because should works on Linux and MacOsX

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern IntPtr dlopen([MarshalAs(UnmanagedType.LPStr)] string filename, int flags);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern int dlclose(IntPtr handle);
  }
}