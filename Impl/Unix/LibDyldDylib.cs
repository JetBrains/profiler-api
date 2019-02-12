using System;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Unix
{
  public static class LibDyldDylib
  {
    private const string LibraryName = "libdyld.dylib";

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern IntPtr dlopen([MarshalAs(UnmanagedType.LPStr)] string filename, int flags);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern int dlclose(IntPtr handle);
  }
}