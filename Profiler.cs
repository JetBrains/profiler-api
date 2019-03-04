using System;
using JetBrains.Profiler.Api.Impl;

// ReSharper disable UnusedMember.Global

namespace JetBrains.Profiler.Api
{
  public static class Profiler
  {
    public static bool WaitForReady(TimeSpan timeout)
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        return LinuxHelper.WaitForReadySignal(timeout) &&
               LinuxHelper.IsLibCoreApiAlreadyLoaded();
      case PlatformId.MacOsX:
        return MacOsXHelper.WaitForReadySignal(timeout) &&
               MacOsXHelper.IsLibCoreApiAlreadyLoaded();
      case PlatformId.Windows:
        return WindowsHelper.WaitForReadySignal(timeout) &&
               WindowsHelper.IsCoreApiDllAlreadyLoaded();
      default:
        throw new ArgumentOutOfRangeException();
      }
    }
  }
}