using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JetBrains.Profiler.Api.Impl.Linux
{
  internal static class LibCSo6
  {
    private const string LibraryName = "libc.so.6"; // Note: Don't use libc.so because no such library in clean system!

    public static class Bitness64
    {
      [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
      public delegate int dl_iterate_phdr_callback(ref dl_phdr_info info, ulong size, IntPtr data);

      [DllImport(LibraryName, ExactSpelling = true)]
      public static extern int dl_iterate_phdr(dl_iterate_phdr_callback callback, IntPtr data);

      [StructLayout(LayoutKind.Explicit, Size = 64)]
      public struct dl_phdr_info
      {
        [FieldOffset(8)]
        public IntPtr dlpi_name;
      }
    }
  }
}