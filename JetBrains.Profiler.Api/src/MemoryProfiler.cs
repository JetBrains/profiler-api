using System;
using System.Diagnostics.CodeAnalysis;
using JetBrains.HabitatDetector;
using JetBrains.Profiler.Api.Impl;
using JetBrains.Profiler.Api.Impl.Linux;
using JetBrains.Profiler.Api.Impl.MacOsX;
using JetBrains.Profiler.Api.Impl.Windows;

namespace JetBrains.Profiler.Api
{
  /// <summary>
  ///   Control memory profiling session.
  /// </summary>
  [SuppressMessage("ReSharper", "UnusedType.Global")]
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  public static class MemoryProfiler
  {
    /// <summary>
    ///   Get a set of features currently active in the profiler.
    /// </summary>
    /// <returns>The set of features.</returns>
    public static MemoryFeatures GetFeatures()
    {
      switch (HabitatInfo.Platform)
      {
      case JetPlatform.Linux:
        if (LinuxHelper.IsCoreApiAlreadyLoaded())
          if (Helper.ThrowIfFail(LibCoreApiSo.V1_Memory_CheckActive(Helper.Id, out var features)))
            return features;
        break;
      case JetPlatform.MacOsX:
        if (MacOsXHelper.IsCoreApiAlreadyLoaded())
          if (Helper.ThrowIfFail(LibCoreApiDylib.V1_Memory_CheckActive(Helper.Id, out var features)))
            return features;
        break;
      case JetPlatform.Windows:
        if (WindowsHelper.IsCoreApiAlreadyLoaded())
          if (Helper.ThrowIfFail(CoreApiDll.V1_Memory_CheckActive(Helper.Id, out var features)))
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
    [SuppressMessage("ReSharper", "IntroduceOptionalParameters.Global")]
    public static void GetSnapshot()
    {
      GetSnapshot(null);
    }

    /// <summary>
    ///   Collect memory snapshot and save it to the disk. This method forces full GC.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    /// <param name="name">The name of the memory snapshot. This is not a file name. Currently not used.</param>
    public static void GetSnapshot(string? name)
    {
      switch (HabitatInfo.Platform)
      {
      case JetPlatform.Linux:
        if (LinuxHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(LibCoreApiSo.V1_Memory_GetSnapshot(Helper.Id, name));
        break;
      case JetPlatform.MacOsX:
        if (MacOsXHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(LibCoreApiDylib.V1_Memory_GetSnapshot(Helper.Id, name));
        break;
      case JetPlatform.Windows:
        if (WindowsHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(CoreApiDll.V1_Memory_GetSnapshot(Helper.Id, name));
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
      switch (HabitatInfo.Platform)
      {
      case JetPlatform.Linux:
        if (LinuxHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(LibCoreApiSo.V1_Memory_ForceGc(Helper.Id));
        break;
      case JetPlatform.MacOsX:
        if (MacOsXHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(LibCoreApiDylib.V1_Memory_ForceGc(Helper.Id));
        break;
      case JetPlatform.Windows:
        if (WindowsHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(CoreApiDll.V1_Memory_ForceGc(Helper.Id));
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
      switch (HabitatInfo.Platform)
      {
      case JetPlatform.Linux:
        if (LinuxHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(LibCoreApiSo.V1_Memory_CollectAllocations(Helper.Id, enable));
        break;
      case JetPlatform.MacOsX:
        if (MacOsXHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(LibCoreApiDylib.V1_Memory_CollectAllocations(Helper.Id, enable));
        break;
      case JetPlatform.Windows:
        if (WindowsHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(CoreApiDll.V1_Memory_CollectAllocations(Helper.Id, enable));
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
      switch (HabitatInfo.Platform)
      {
      case JetPlatform.Linux:
        if (LinuxHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(LibCoreApiSo.V1_Memory_Detach(Helper.Id));
        break;
      case JetPlatform.MacOsX:
        if (MacOsXHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(LibCoreApiDylib.V1_Memory_Detach(Helper.Id));
        break;
      case JetPlatform.Windows:
        if (WindowsHelper.IsCoreApiAlreadyLoaded())
          Helper.ThrowIfFail(CoreApiDll.V1_Memory_Detach(Helper.Id));
        break;
      default:
        throw new PlatformNotSupportedException();
      }
    }
  }
}