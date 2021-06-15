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
      try
      {
        switch (Helper.Platform)
        {
        case PlatformId.Linux:
          if (Helper.IsLibCoreApiSoAlreadyLoaded())
            if (Helper.ThrowIfFail(LibCoreApiSo.V1_Memory_CheckActive(Helper.Id, out var features)))
              return features;
          break;
        case PlatformId.MacOsX:
          if (Helper.IsLibCoreApiDylibAlreadyLoaded())
            if (Helper.ThrowIfFail(LibCoreApiDylib.V1_Memory_CheckActive(Helper.Id, out var features)))
              return features;
          break;
        case PlatformId.Windows:
          if (Helper.IsCoreApiDllAlreadyLoaded())
            if (Helper.ThrowIfFail(CoreApiDll.V1_Memory_CheckActive(Helper.Id, out var features)))
              return features;
          break;
        default:
          throw new PlatformNotSupportedException();
        }
      }
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
      catch (TypeLoadException e) when (Helper.IsEntryPointNotFoundException(e))
#else
      catch (EntryPointNotFoundException)
#endif
      {
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
      try
      {
        switch (Helper.Platform)
        {
        case PlatformId.Linux:
          if (Helper.IsLibCoreApiSoAlreadyLoaded())
            Helper.ThrowIfFail(LibCoreApiSo.V1_Memory_GetSnapshot(Helper.Id, name));
          break;
        case PlatformId.MacOsX:
          if (Helper.IsLibCoreApiDylibAlreadyLoaded())
            Helper.ThrowIfFail(LibCoreApiDylib.V1_Memory_GetSnapshot(Helper.Id, name));
          break;
        case PlatformId.Windows:
          if (Helper.IsCoreApiDllAlreadyLoaded())
            Helper.ThrowIfFail(CoreApiDll.V1_Memory_GetSnapshot(Helper.Id, name));
          break;
        default:
          throw new PlatformNotSupportedException();
        }
      }
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
      catch (TypeLoadException e) when (Helper.IsEntryPointNotFoundException(e))
#else
      catch (EntryPointNotFoundException)
#endif
      {
      }
    }

    /// <summary>
    ///   Forces full GC.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    public static void ForceGc()
    {
      try
      {
        switch (Helper.Platform)
        {
        case PlatformId.Linux:
          if (Helper.IsLibCoreApiSoAlreadyLoaded())
            Helper.ThrowIfFail(LibCoreApiSo.V1_Memory_ForceGc(Helper.Id));
          break;
        case PlatformId.MacOsX:
          if (Helper.IsLibCoreApiDylibAlreadyLoaded())
            Helper.ThrowIfFail(LibCoreApiDylib.V1_Memory_ForceGc(Helper.Id));
          break;
        case PlatformId.Windows:
          if (Helper.IsCoreApiDllAlreadyLoaded())
            Helper.ThrowIfFail(CoreApiDll.V1_Memory_ForceGc(Helper.Id));
          break;
        default:
          throw new PlatformNotSupportedException();
        }
      }
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
      catch (TypeLoadException e) when (Helper.IsEntryPointNotFoundException(e))
#else
      catch (EntryPointNotFoundException)
#endif
      {
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
      try
      {
        switch (Helper.Platform)
        {
        case PlatformId.Linux:
          if (Helper.IsLibCoreApiSoAlreadyLoaded())
            Helper.ThrowIfFail(LibCoreApiSo.V1_Memory_CollectAllocations(Helper.Id, enable));
          break;
        case PlatformId.MacOsX:
          if (Helper.IsLibCoreApiDylibAlreadyLoaded())
            Helper.ThrowIfFail(LibCoreApiDylib.V1_Memory_CollectAllocations(Helper.Id, enable));
          break;
        case PlatformId.Windows:
          if (Helper.IsCoreApiDllAlreadyLoaded())
            Helper.ThrowIfFail(CoreApiDll.V1_Memory_CollectAllocations(Helper.Id, enable));
          break;
        default:
          throw new PlatformNotSupportedException();
        }
      }
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
      catch (TypeLoadException e) when (Helper.IsEntryPointNotFoundException(e))
#else
      catch (EntryPointNotFoundException)
#endif
      {
      }
    }

    /// <summary>
    ///   Detach the profiler from the profiled process. Does nothing if detaching is disabled in the profiler. To check
    ///   whether the detaching is enabled, use <see cref="GetFeatures" /> with <see cref="MemoryFeatures.Detach" /> flag.
    ///   Doesn't throw any errors even if the application is run with profiling disabled.
    /// </summary>
    public static void Detach()
    {
      try
      {
        switch (Helper.Platform)
        {
        case PlatformId.Linux:
          if (Helper.IsLibCoreApiSoAlreadyLoaded())
            Helper.ThrowIfFail(LibCoreApiSo.V1_Memory_Detach(Helper.Id));
          break;
        case PlatformId.MacOsX:
          if (Helper.IsLibCoreApiDylibAlreadyLoaded())
            Helper.ThrowIfFail(LibCoreApiDylib.V1_Memory_Detach(Helper.Id));
          break;
        case PlatformId.Windows:
          if (Helper.IsCoreApiDllAlreadyLoaded())
            Helper.ThrowIfFail(CoreApiDll.V1_Memory_Detach(Helper.Id));
          break;
        default:
          throw new PlatformNotSupportedException();
        }
      }
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
      catch (TypeLoadException e) when (Helper.IsEntryPointNotFoundException(e))
#else
      catch (EntryPointNotFoundException)
#endif
      {
      }
    }
  }
}