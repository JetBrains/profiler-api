using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Linux
{
  [SuppressMessage("ReSharper", "CommentTypo")]
  internal static class LinuxHelper
  {
    internal static bool IsCoreApiAlreadyLoaded()
    {
      // Note(k15tfu): dlopen() in musl ignores SONAME, use dl_iterate_phdr(). See https://www.openwall.com/lists/musl/2022/01/11/4.
      return DlIteratePhdrFindLibraryPath(LibCoreApiSo.LibraryName) != null;
    }

    [SuppressMessage("ReSharper", "IdentifierTypo")]
    internal static string? DlIteratePhdrFindLibraryPath(string libraryName)
    {
      var tailLibraryName = '/' + libraryName;
      string? resultPath = null;
      LibCSo6.dl_iterate_phdr((ref dl_phdr_info info, nuint size, IntPtr data) =>
        {
          var path = Marshal.PtrToStringAnsi(info.dlpi_name);
          if (path == null || !path.EndsWith(tailLibraryName))
            return 0;
          resultPath = path;
          return 1;
        }, IntPtr.Zero);

      return resultPath;
    }
  }
}