using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Linux
{
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  [SuppressMessage("ReSharper", "IdentifierTypo")]
  internal static class LibCSo6
  {
    private const string LibraryName = "libc.so.6"; // Note: Don't use libc.so because no such library in clean system!

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    internal delegate int dl_iterate_phdr_callback(ref dl_phdr_info info, nuint size, IntPtr data);

    [DllImport(LibraryName, ExactSpelling = true)]
    internal static extern int dl_iterate_phdr(dl_iterate_phdr_callback callback, IntPtr data);
  }
}