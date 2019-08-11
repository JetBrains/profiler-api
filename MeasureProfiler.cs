using System;
using JetBrains.Profiler.Api.Impl;
using JetBrains.Profiler.Api.Impl.Linux;
using JetBrains.Profiler.Api.Impl.MacOsX;
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
    /// <summary>
    ///   Get a set of features currently active in the profiler.
    /// </summary>
    /// <returns>The set of features.</returns>
    public static MeasureFeatures GetFeatures()
    {
      var id = Helper.Id;
      MeasureFeatures features = 0;
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (Helper.IsLibCoreApiSoAlreadyLoaded())
          if (Helper.InvokeCoreApi(() => LibCoreApiSo.V1_Measure_CheckActive(id, out features)))
            return features;
        break;
      case PlatformId.MacOsX:
        if (Helper.IsLibCoreApiDylibAlreadyLoaded())
          if (Helper.InvokeCoreApi(() => LibCoreApiDylib.V1_Measure_CheckActive(id, out features)))
            return features;
        break;
      case PlatformId.Windows:
        if (Helper.IsCoreApiDllAlreadyLoaded())
          if (Helper.InvokeCoreApi(() => CoreApiDll.V1_Measure_CheckActive(id, out features)))
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
      var id = Helper.Id;
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (Helper.IsLibCoreApiSoAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiSo.V1_Measure_StartCollecting(id, groupName));
        break;
      case PlatformId.MacOsX:
        if (Helper.IsLibCoreApiDylibAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiDylib.V1_Measure_StartCollecting(id, groupName));
        break;
      case PlatformId.Windows:
        if (Helper.IsCoreApiDllAlreadyLoaded())
          Helper.InvokeCoreApi(() => CoreApiDll.V1_Measure_StartCollecting(id, groupName));
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
      var id = Helper.Id;
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (Helper.IsLibCoreApiSoAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiSo.V1_Measure_StopCollecting(id));
        break;
      case PlatformId.MacOsX:
        if (Helper.IsLibCoreApiDylibAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiDylib.V1_Measure_StopCollecting(id));
        break;
      case PlatformId.Windows:
        if (Helper.IsCoreApiDllAlreadyLoaded())
          Helper.InvokeCoreApi(() => CoreApiDll.V1_Measure_StopCollecting(id));
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
      var id = Helper.Id;
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (Helper.IsLibCoreApiSoAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiSo.V1_Measure_Save(id, name));
        break;
      case PlatformId.MacOsX:
        if (Helper.IsLibCoreApiDylibAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiDylib.V1_Measure_Save(id, name));
        break;
      case PlatformId.Windows:
        if (Helper.IsCoreApiDllAlreadyLoaded())
          Helper.InvokeCoreApi(() => CoreApiDll.V1_Measure_Save(id, name));
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
      var id = Helper.Id;
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (Helper.IsLibCoreApiSoAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiSo.V1_Measure_Drop(id));
        break;
      case PlatformId.MacOsX:
        if (Helper.IsLibCoreApiDylibAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiDylib.V1_Measure_Drop(id));
        break;
      case PlatformId.Windows:
        if (Helper.IsCoreApiDllAlreadyLoaded())
          Helper.InvokeCoreApi(() => CoreApiDll.V1_Measure_Drop(id));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }

    /// <summary>
    ///   Detach the profiler from the profiled process. Does nothing if detaching is disabled in the profiler. To check
    ///   whether the detaching is enabled, use <see cref="GetFeatures" /> with <see cref="MeasureFeatures.Detach" /> flag.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    public static void Detach()
    {
      var id = Helper.Id;
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (Helper.IsLibCoreApiSoAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiSo.V1_Measure_Detach(id));
        break;
      case PlatformId.MacOsX:
        if (Helper.IsLibCoreApiDylibAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiDylib.V1_Measure_Detach(id));
        break;
      case PlatformId.Windows:
        if (Helper.IsCoreApiDllAlreadyLoaded())
          Helper.InvokeCoreApi(() => CoreApiDll.V1_Measure_Detach(id));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }
  }
}