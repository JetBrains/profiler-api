using System;
using JetBrains.Profiler.Api.Impl;
using JetBrains.Profiler.Api.Impl.Unix;
using JetBrains.Profiler.Api.Impl.Windows;

// ReSharper disable UnusedMember.Global

namespace JetBrains.Profiler.Api
{
  public static class MeasureProfiler
  {
#if ENABLE_WAIT_FOR_READY
    public static bool WaitForReady(TimeSpan timeout)
    {
      return Helper.WaitFor(timeout, () => (GetFeatures() & MeasureFeatures.Ready) != 0);
    }
#endif

    public static MeasureFeatures GetFeatures()
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (LinuxHelper.IsLibCoreApiAlreadyLoaded())
          if (Helper.ThrowOnError(LibCoreApi.V1_Measure_CheckActive(Helper.Id, out var features)))
            return features;
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsLibCoreApiAlreadyLoaded())
          if (Helper.ThrowOnError(LibCoreApi.V1_Measure_CheckActive(Helper.Id, out var features)))
            return features;
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiDllAlreadyLoaded())
          if (Helper.ThrowOnError(CoreApiDll.V1_Measure_CheckActive(Helper.Id, out var features)))
            return features;
        break;
      default:
        throw new PlatformNotSupportedException();
      }
      return MeasureFeatures.Inactive;
    }

    public static void StartCollectingData()
    {
      StartCollectingData(null);
    }

    public static void StartCollectingData(string groupName)
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (LinuxHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Measure_StartCollecting(Helper.Id, groupName));
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Measure_StartCollecting(Helper.Id, groupName));
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiDllAlreadyLoaded())
          Helper.ThrowOnError(CoreApiDll.V1_Measure_StartCollecting(Helper.Id, groupName));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }

    public static void StopCollectingData()
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (LinuxHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Measure_StopCollecting(Helper.Id));
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Measure_StopCollecting(Helper.Id));
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiDllAlreadyLoaded())
          Helper.ThrowOnError(CoreApiDll.V1_Measure_StopCollecting(Helper.Id));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }

    public static void SaveData()
    {
      SaveData(null);
    }

    public static void SaveData(string name)
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (LinuxHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Measure_Save(Helper.Id, name));
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Measure_Save(Helper.Id, name));
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiDllAlreadyLoaded())
          Helper.ThrowOnError(CoreApiDll.V1_Measure_Save(Helper.Id, name));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }

    public static void DropData()
    {
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (LinuxHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Measure_Drop(Helper.Id));
        break;
      case PlatformId.MacOsX:
        if (MacOsXHelper.IsLibCoreApiAlreadyLoaded())
          Helper.ThrowOnError(LibCoreApi.V1_Measure_Drop(Helper.Id));
        break;
      case PlatformId.Windows:
        if (WindowsHelper.IsCoreApiDllAlreadyLoaded())
          Helper.ThrowOnError(CoreApiDll.V1_Measure_Drop(Helper.Id));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }
  }
}