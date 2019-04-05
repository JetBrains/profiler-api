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
  ///   Control memory profiling session.
  /// </summary>
  public static class MemoryProfiler
  {
#if ENABLE_WAIT_FOR_READY
    public static bool WaitForReady(TimeSpan timeout)
    {
      return Helper.WaitFor(timeout, () => (GetFeatures() & MemoryFeatures.Ready) != 0);
    }
#endif

    /// <summary>
    ///   Get a set of features currently active in the profiler.
    /// </summary>
    /// <returns>The set of features.</returns>
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

    /// <summary>
    ///   Forces full GC.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
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

    /// <summary>
    ///   Enable/disable collecting memory allocation data. Does nothing if collecting allocation data is disabled in the
    ///   profiler. To check whether the collecting is enabled, use <see cref="GetFeatures" /> with
    ///   <see cref="MemoryFeatures.CollectAllocations" /> flag.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
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