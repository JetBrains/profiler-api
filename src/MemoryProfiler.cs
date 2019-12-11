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
  ///   Control memory profiling session.
  /// </summary>
  public static class MemoryProfiler
  {
    /// <summary>
    ///   Get a set of features currently active in the profiler.
    /// </summary>
    /// <returns>The set of features.</returns>
    public static MemoryFeatures GetFeatures()
    {
      var id = Helper.Id;
      MemoryFeatures features = 0;
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (Helper.IsLibCoreApiSoAlreadyLoaded())
          if (Helper.InvokeCoreApi(() => LibCoreApiSo.V1_Memory_CheckActive(id, out features)))
            return features;
        break;
      case PlatformId.MacOsX:
        if (Helper.IsLibCoreApiDylibAlreadyLoaded())
          if (Helper.InvokeCoreApi(() => LibCoreApiDylib.V1_Memory_CheckActive(id, out features)))
            return features;
        break;
      case PlatformId.Windows:
        if (Helper.IsCoreApiDllAlreadyLoaded())
          if (Helper.InvokeCoreApi(() => CoreApiDll.V1_Memory_CheckActive(id, out features)))
            return features;
        break;
      default:
        throw new PlatformNotSupportedException();
      }
      return 0;
    }

    /// <summary>
    ///   Collect memory snapshot and save it to the disk. This method forces full GC.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    public static void GetSnapshot()
    {
      GetSnapshot(null);
    }

    /// <summary>
    ///   Collect memory snapshot and save it to the disk. This method forces full GC.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    /// <param name="name">The name of the memory snapshot. This is not a file name. Currently not used.</param>
    public static void GetSnapshot(string name)
    {
      var id = Helper.Id;
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (Helper.IsLibCoreApiSoAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiSo.V1_Memory_GetSnapshot(id, name));
        break;
      case PlatformId.MacOsX:
        if (Helper.IsLibCoreApiDylibAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiDylib.V1_Memory_GetSnapshot(id, name));
        break;
      case PlatformId.Windows:
        if (Helper.IsCoreApiDllAlreadyLoaded())
          Helper.InvokeCoreApi(() => CoreApiDll.V1_Memory_GetSnapshot(id, name));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }

    /// <summary>
    ///   Forces full GC.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    public static void ForceGc()
    {
      var id = Helper.Id;
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (Helper.IsLibCoreApiSoAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiSo.V1_Memory_ForceGc(id));
        break;
      case PlatformId.MacOsX:
        if (Helper.IsLibCoreApiDylibAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiDylib.V1_Memory_ForceGc(id));
        break;
      case PlatformId.Windows:
        if (Helper.IsCoreApiDllAlreadyLoaded())
          Helper.InvokeCoreApi(() => CoreApiDll.V1_Memory_ForceGc(id));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }

    /// <summary>
    ///   Enable/disable collecting memory allocation data. Does nothing if collecting allocation data is disabled in the
    ///   profiler. To check whether the collecting is enabled, use <see cref="GetFeatures" /> with
    ///   <see cref="MemoryFeatures.CollectAllocations" /> flag.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    public static void CollectAllocations(bool enable)
    {
      var id = Helper.Id;
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (Helper.IsLibCoreApiSoAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiSo.V1_Memory_CollectAllocations(id, enable));
        break;
      case PlatformId.MacOsX:
        if (Helper.IsLibCoreApiDylibAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiDylib.V1_Memory_CollectAllocations(id, enable));
        break;
      case PlatformId.Windows:
        if (Helper.IsCoreApiDllAlreadyLoaded())
          Helper.InvokeCoreApi(() => CoreApiDll.V1_Memory_CollectAllocations(id, enable));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }
    
    /// <summary>
    ///   Detach the profiler from the profiled process. Does nothing if detaching is disabled in the profiler. To check
    ///   whether the detaching is enabled, use <see cref="GetFeatures" /> with <see cref="MemoryFeatures.Detach" /> flag.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    public static void Detach()
    {
      var id = Helper.Id;
      switch (Helper.Platform)
      {
      case PlatformId.Linux:
        if (Helper.IsLibCoreApiSoAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiSo.V1_Memory_Detach(id));
        break;
      case PlatformId.MacOsX:
        if (Helper.IsLibCoreApiDylibAlreadyLoaded())
          Helper.InvokeCoreApi(() => LibCoreApiDylib.V1_Memory_Detach(id));
        break;
      case PlatformId.Windows:
        if (Helper.IsCoreApiDllAlreadyLoaded())
          Helper.InvokeCoreApi(() => CoreApiDll.V1_Memory_Detach(id));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }
  }
}