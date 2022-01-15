
namespace JetBrains.Profiler.Api.Impl.Linux
{
  internal static class LinuxHelper
  {
    public static bool IsCoreApiAlreadyLoaded()
    {
      // Note(k15tfu): dlopen() in musl ignores SONAME, use dl_iterate_phdr().  See https://www.openwall.com/lists/musl/2022/01/11/4.
      return LibCSo6.Helper.DlIteratePhdrFindLibraryPath(LibCoreApiSo.LibraryName) != null;
    }
  }
}