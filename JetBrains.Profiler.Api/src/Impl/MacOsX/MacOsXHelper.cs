using System;

namespace JetBrains.Profiler.Api.Impl.MacOsX
{
  internal static class MacOsXHelper
  {
    public static bool IsCoreApiAlreadyLoaded()
    {
      var handle = LibDyldDylib.dlopen(LibCoreApiDylib.LibraryName, RTLD.RTLD_GLOBAL | RTLD.RTLD_LAZY | RTLD.RTLD_NOLOAD);
      if (handle == IntPtr.Zero)
        return false;
      LibDyldDylib.dlclose(handle);
      return true;
    }
  }
}