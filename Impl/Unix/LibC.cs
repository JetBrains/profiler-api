using System;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Unix
{
  internal static class LibC
  {
    private const string LibraryName = "libc"; // Note: No extension here, because CoreCLR support that case

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern int uname(IntPtr buf);
  }
}