using System;
using JetBrains.Profiler.Api.Impl;
using JetBrains.Profiler.Api.Impl.Unix;
using JetBrains.Profiler.Api.Impl.Windows;

// ReSharper disable UnusedMember.Global

namespace JetBrains.Profiler.Api
{
  public static class MemoryProfiler
  {
#if ENABLE_WAIT_FOR_READY
    public static bool WaitForReady(TimeSpan timeout)
    {
      return Helper.WaitFor(timeout, () => (GetFeatures() & MemoryFeatures.Ready) != 0);
    }
#endif

    public static MemoryFeatures GetFeatures()
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (LinuxHelper.IsLibCoreApiAlreadyLoaded())
          if (Helper.ThrowOnError(LibCoreApi.V1_Memory_CheckActive(Helper.Id, out var features)))
            return features;
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsLibCoreApiAlreadyLoaded())
          if (Helper.ThrowOnError(LibCoreApi.V1_Memory_CheckActive(Helper.Id, out var features)))
            return features;
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiDllAlreadyLoaded())
          if (Helper.ThrowOnError(CoreApiDll.V1_Memory_CheckActive(Helper.Id, out var features)))
            return features;
        break;
      default:
        throw new PlatformNotSupportedException();
      }
      return MemoryFeatures.Inactive;
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
        if (LinuxHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Memory_GetSnapshot(Helper.Id, name));
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Memory_GetSnapshot(Helper.Id, name));
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiDllAlreadyLoaded())
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
        if (LinuxHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Memory_ForceGc(Helper.Id));
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Memory_ForceGc(Helper.Id));
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiDllAlreadyLoaded())
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
        if (LinuxHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Memory_CollectAllocations(Helper.Id, enable));
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Memory_CollectAllocations(Helper.Id, enable));
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiDllAlreadyLoaded())
          Helper.ThrowOnError(CoreApiDll.V1_Memory_CollectAllocations(Helper.Id, enable));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }
  }
}