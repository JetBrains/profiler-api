using System;
using JetBrains.Profiler.Api.Impl.Linux;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class LinuxHelper
  {
    public static bool IsLibCoreApiAlreadyLoaded()
    {
      var handle = LibDlSo2.dlopen(LibCoreApiSo.LibraryName, (int) (RTLD.RTLD_GLOBAL | RTLD.RTLD_LAZY | RTLD.RTLD_NOLOAD));
      if (handle == IntPtr.Zero)
        return false;
      LibDlSo2.dlclose(handle);
      return true;
    }
  }
}