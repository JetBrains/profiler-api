using System;
using JetBrains.Profiler.Api.Impl.Unix;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class LinuxHelper
  {
    public static bool WaitForReadySignal(TimeSpan timeout)
    {
      throw new NotImplementedException();
    }

    public static bool IsLibCoreApiAlreadyLoaded()
    {
      return UnixHelper.IsAlreadyLoaded(LibCoreApi.LibraryName + ".so");
    }
  }
}