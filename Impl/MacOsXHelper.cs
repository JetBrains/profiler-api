using System;
using JetBrains.Profiler.Api.Impl.Unix;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class MacOsXHelper
  {
    public static bool WaitForReadySignal(TimeSpan timeout)
    {
      // Todo: Should be implemented for attach!!!
      return true;
    }

    public static bool IsLibCoreApiAlreadyLoaded()
    {
      return UnixHelper.IsAlreadyLoaded(LibCoreApi.LibraryName + ".dylib");
    }
  }
}