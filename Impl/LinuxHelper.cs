using System;
using JetBrains.Profiler.Api.Impl.Linux;
using JetBrains.Profiler.Api.Impl.Unix;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class LinuxHelper
  {
    public static bool IsLibCoreApiAlreadyLoaded()
    {
      var handle = LibDlSo2.dlopen(LibCoreApi.LibraryName + ".so", (int) (RTLD.RTLD_GLOBAL | RTLD.RTLD_LAZY | RTLD.RTLD_NOLOAD));
      if (handle == IntPtr.Zero)
        return false;
      LibDlSo2.dlclose(handle);
      return true;
    }
  }
}