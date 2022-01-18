using System;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Linux
{
  internal static class LinuxHelper
  {
    public static bool IsCoreApiAlreadyLoaded()
    {
      // Note(k15tfu): dlopen() in musl ignores SONAME, use dl_iterate_phdr().  See https://www.openwall.com/lists/musl/2022/01/11/4.
      return DlIteratePhdrFindLibraryPath(LibCoreApiSo.LibraryName) != null;
    }

    public static string DlIteratePhdrFindLibraryPath(string libraryName)
    {
      string found = null;
      var tailLibraryName = '/' + libraryName;
      if (IntPtr.Size == 8)
        LibCSo6.Bitness64.dl_iterate_phdr((ref LibCSo6.Bitness64.dl_phdr_info info, ulong size, IntPtr data) =>
          {
            var path = Marshal.PtrToStringAnsi(info.dlpi_name);
            if (path == null || !path.EndsWith(tailLibraryName))
              return 0;
            found = path;
            return 1;
          }, IntPtr.Zero);
      return found;
    }
  }
}