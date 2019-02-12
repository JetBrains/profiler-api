using System;
using JetBrains.Profiler.Api.Impl;
using JetBrains.Profiler.Api.Impl.Unix;
using JetBrains.Profiler.Api.Impl.Windows;

// ReSharper disable UnusedMember.Global

namespace JetBrains.Profiler.Api
{
  public static class MemoryProfiler
  {
    public static bool WaitForReady(TimeSpan timeout)
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        return LinuxHelper.WaitForReadySignal(timeout) &&
               LinuxHelper.IsCoreApiLoaded() &&
               Helper.ThrowOnError(CoreApiSo.V1_Memory_CheckActive(Helper.Id, out _));
      case PlatformId.MacOsX:
        return MacOsXHelper.WaitForReadySignal(timeout) &&
               MacOsXHelper.IsCoreApiLoaded() &&
               Helper.ThrowOnError(CoreApiDylib.V1_Memory_CheckActive(Helper.Id, out _));
      case PlatformId.Windows:
        return WindowsHelper.WaitForReadySignal(timeout) &&
               WindowsHelper.IsCoreApiLoaded() &&
               Helper.ThrowOnError(CoreApiDll.V1_Memory_CheckActive(Helper.Id, out _));
      default:
        throw new PlatformNotSupportedException();
      }
    }

    public static MemoryFeatures GetFeatures()
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (LinuxHelper.IsCoreApiLoaded())
          if (Helper.ThrowOnError(CoreApiSo.V1_Memory_CheckActive(Helper.Id, out var features)))
            return features;
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsCoreApiLoaded())
          if (Helper.ThrowOnError(CoreApiDylib.V1_Memory_CheckActive(Helper.Id, out var features)))
            return features;
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiLoaded())
          if (Helper.ThrowOnError(CoreApiDll.V1_Memory_CheckActive(Helper.Id, out var features)))
            return features;
        break;
      default:
        throw new PlatformNotSupportedException();
      }
      return MemoryFeatures.None;
    }

    public static void GetSnapshot()
    {
      GetSnapshot(null);
    }

    public static void GetSnapshot(string name)
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (LinuxHelper.IsCoreApiLoaded())
          Helper.ThrowOnError(CoreApiSo.V1_Memory_GetSnapshot(Helper.Id, name));
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsCoreApiLoaded())
          Helper.ThrowOnError(CoreApiDylib.V1_Memory_GetSnapshot(Helper.Id, name));
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiLoaded())
          Helper.ThrowOnError(CoreApiDll.V1_Memory_GetSnapshot(Helper.Id, name));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }

    public static void ForceGc()
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (LinuxHelper.IsCoreApiLoaded())
          Helper.ThrowOnError(CoreApiSo.V1_Memory_ForceGc(Helper.Id));
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsCoreApiLoaded())
          Helper.ThrowOnError(CoreApiDylib.V1_Memory_ForceGc(Helper.Id));
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiLoaded())
          Helper.ThrowOnError(CoreApiDll.V1_Memory_ForceGc(Helper.Id));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }

    public static void CollectAllocations(bool enable)
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (LinuxHelper.IsCoreApiLoaded())
          Helper.ThrowOnError(CoreApiSo.V1_Memory_CollectAllocations(Helper.Id, enable));
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsCoreApiLoaded())
          Helper.ThrowOnError(CoreApiDylib.V1_Memory_CollectAllocations(Helper.Id, enable));
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiLoaded())
          Helper.ThrowOnError(CoreApiDll.V1_Memory_CollectAllocations(Helper.Id, enable));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }
  }
}