using System;
using JetBrains.Profiler.Api.Impl.MacOsX;
using JetBrains.Profiler.Api.Impl.Unix;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class MacOsXHelper
  {
    public static bool IsLibCoreApiAlreadyLoaded()
    {
      var handle = LibDyldDylib.dlopen(LibCoreApi.LibraryName + ".dylib", (int) (DlFlags.RTLD_GLOBAL | DlFlags.RTLD_LAZY | DlFlags.RTLD_NOLOAD));
      if (handle == IntPtr.Zero)
        return false;
      LibDyldDylib.dlclose(handle);
      return true;
    }
  }
}