using System;
using System.Runtime.InteropServices;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable IdentifierTypo
// ReSharper disable InconsistentNaming
// ReSharper disable MemberCanBePrivate.Global

namespace JetBrains.Profiler.Api.Impl.Linux
{
  internal static class LibCSo6
  {
    private const string LibraryName = "libc.so.6"; // Note: Don't use libc.so because no such library in clean system!

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int dl_iterate_phdr_callback(ref dl_phdr_info info, nuint size, IntPtr data);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern int dl_iterate_phdr(dl_iterate_phdr_callback callback, IntPtr data);

    [StructLayout(LayoutKind.Sequential)]
    public struct dl_phdr_info
    {
      public nuint dlpi_addr;
      public IntPtr dlpi_name;
      public IntPtr dlpi_phdr;
      public ushort dlpi_phnum;

      // For other fields, check the size argument of <see cref="dl_iterate_phdr_callback" /> first.
    }
  }
}