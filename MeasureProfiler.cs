using System;
using JetBrains.Profiler.Api.Impl;
using JetBrains.Profiler.Api.Impl.Unix;
using JetBrains.Profiler.Api.Impl.Windows;

// ReSharper disable MemberCanBePrivate.Global
// ReSharper disable IntroduceOptionalParameters.Global
// ReSharper disable UnusedMember.Global

namespace JetBrains.Profiler.Api
{
  /// <summary>
  ///   Control performance/coverage profiling session.
  /// </summary>
  public static class MeasureProfiler
  {
#if ENABLE_WAIT_FOR_READY
    public static bool WaitForReady(TimeSpan timeout)
    {
      return Helper.WaitFor(timeout, () => (GetFeatures() & MeasureFeatures.Ready) != 0);
    }
#endif

    /// <summary>
    ///   Get a set of features currently active in the profiler.
    /// </summary>
    /// <returns>The set of features.</returns>
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
      return 0;
    }

    /// <summary>
    ///   Start collecting profiling data.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    public static void StartCollectingData()
    {
      StartCollectingData(null);
    }

    /// <summary>
    ///   Start collecting profiling data.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    /// <param name="groupName">The name of the collected data block.</param>
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

    /// <summary>
    ///   Stop collecting profiling data. This method doesn't save the collected data block to the disk.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
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

    /// <summary>
    ///   Stop collecting data if needed and save all collected data blocks to the disk.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    public static void SaveData()
    {
      SaveData(null);
    }

    /// <summary>
    ///   Stop collecting data if needed and save all collected data blocks to the disk.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    /// <param name="name">
    ///   The name of all data blocks that were not yet saved or dropped. This is not a file name. Currently
    ///   not used.
    /// </param>
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

    /// <summary>
    ///   Stop collecting data if needed and drop all collected data blocks.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
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