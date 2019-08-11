using System;
using JetBrains.Profiler.Api.Impl.MacOsX;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class MacOsXHelper
  {
    public static bool IsLibCoreApiAlreadyLoaded()
    {
      var handle = LibDyldDylib.dlopen(LibCoreApiDylib.LibraryName, (int) (RTLD.RTLD_GLOBAL | RTLD.RTLD_LAZY | RTLD.RTLD_NOLOAD));
      if (handle == IntPtr.Zero)
        return false;
      LibDyldDylib.dlclose(handle);
      return true;
    }
  }
}