using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JetBrains.Profiler.Api.Impl.Linux
{
  internal static class LibCSo6
  {
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int dl_iterate_phdr_callback_delegate(ref dl_phdr_info info, ulong size, IntPtr data);

    private const string LibraryName = "libc.so.6"; // Note: Don't use libc.so because no such library in clean system!

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern int dl_iterate_phdr(dl_iterate_phdr_callback_delegate callback, IntPtr data);

    [StructLayout(LayoutKind.Explicit, Size = 64)]
    public struct dl_phdr_info
    {
      [FieldOffset(8)] public IntPtr dlpi_name;
    }

    public static class Helper
    {
      public static string DlIteratePhdrFindLibraryPath(string libraryName)
      {
        string found = null;
        dl_iterate_phdr((ref dl_phdr_info info, ulong size, IntPtr data) =>
        {
          var path = Marshal.PtrToStringAnsi(info.dlpi_name);
          if (path?.EndsWith($"/{libraryName}") ?? false)
          {
            found = path;
            return 1;
          }

          return 0;
        }, IntPtr.Zero);

        return found;
      }
    }
  }
}