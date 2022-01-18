using System;

namespace JetBrains.Profiler.Api.Impl.Linux
{
  internal static class LinuxHelper
  {
    public static bool IsCoreApiAlreadyLoaded()
    {
      var handle = LibDlSo2.dlopen(LibCoreApiSo.LibraryName, RTLD.RTLD_GLOBAL | RTLD.RTLD_LAZY | RTLD.RTLD_NOLOAD);
      if (handle == IntPtr.Zero)
        return false;
      LibDlSo2.dlclose(handle);
      return true;
    }
  }
}