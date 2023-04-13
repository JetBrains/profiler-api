using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Linux
{
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  internal static class LibCoreApiSo
  {
    internal const string LibraryName = "libJetBrains.Profiler.CoreApi.so";

    [SuppressMessage("ReSharper", "NotAccessedField.Local")]
    private static readonly SafeDlHandle ourHandle; // Note(ww898): Keep the handle till domain or load context unloading!!!

    static LibCoreApiSo()
    {
      var libraryPath = LinuxHelper.DlIteratePhdrFindLibraryPath(LibraryName);
      if (libraryPath == null)
        throw new DllNotFoundException("Failed to find already loaded shared library " + LibraryName);

      var handle = LibDlSo2.dlopen(libraryPath, RTLD.RTLD_GLOBAL | RTLD.RTLD_LAZY);
      if (handle == IntPtr.Zero)
        throw new DllNotFoundException("Failed to load shared library " + LibraryName);
      ourHandle = new SafeDlHandle(handle);

      TDelegate GetDlFunction<TDelegate>(string functionName) where TDelegate : Delegate
      {
        var ptr = LibDlSo2.dlsym(handle, functionName);
        if (ptr == IntPtr.Zero)
          throw new
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
            TypeLoadException
#else
            EntryPointNotFoundException
#endif
            ("Failed to get a function entry point " + functionName);

#pragma warning disable CS0618
        return (TDelegate)Marshal.GetDelegateForFunctionPointer(ptr, typeof(TDelegate));
#pragma warning restore CS0618
      }

      // @formatter:off
      V1_Measure_CheckActive     = GetDlFunction<V1_Measure_CheckActive_Delegate    >(nameof(V1_Measure_CheckActive    ));
      V1_Measure_StartCollecting = GetDlFunction<V1_Measure_StartCollecting_Delegate>(nameof(V1_Measure_StartCollecting));
      V1_Measure_StopCollecting  = GetDlFunction<V1_Measure_StopCollecting_Delegate >(nameof(V1_Measure_StopCollecting ));
      V1_Measure_Save            = GetDlFunction<V1_Measure_Save_Delegate           >(nameof(V1_Measure_Save           ));
      V1_Measure_Drop            = GetDlFunction<V1_Measure_Drop_Delegate           >(nameof(V1_Measure_Drop           ));
      V1_Measure_Detach          = GetDlFunction<V1_Measure_Detach_Delegate         >(nameof(V1_Measure_Detach         ));
      // @formatter:on

      // @formatter:off
      V1_Memory_CheckActive        = GetDlFunction<V1_Memory_CheckActive_Delegate       >(nameof(V1_Memory_CheckActive       ));
      V1_Memory_GetSnapshot        = GetDlFunction<V1_Memory_GetSnapshot_Delegate       >(nameof(V1_Memory_GetSnapshot       ));
      V1_Memory_ForceGc            = GetDlFunction<V1_Memory_ForceGc_Delegate           >(nameof(V1_Memory_ForceGc           ));
      V1_Memory_CollectAllocations = GetDlFunction<V1_Memory_CollectAllocations_Delegate>(nameof(V1_Memory_CollectAllocations));
      V1_Memory_Detach             = GetDlFunction<V1_Memory_Detach_Delegate            >(nameof(V1_Memory_Detach            ));
      // @formatter:on
    }

    #region Nested type: SafeDlHandle

    private sealed class SafeDlHandle
    {
      private readonly IntPtr myHandle;

      internal SafeDlHandle(IntPtr handle)
      {
        myHandle = handle;
      }

      ~SafeDlHandle()
      {
        if (myHandle != IntPtr.Zero)
          LibDlSo2.dlclose(myHandle);
      }
    }

    #endregion

    #region Measure

    // @formatter:off
    internal delegate HResults V1_Measure_CheckActive_Delegate(uint id, out MeasureFeatures features);
    internal delegate HResults V1_Measure_StartCollecting_Delegate(uint id, [MarshalAs(UnmanagedType.LPWStr)] string? groupName);
    internal delegate HResults V1_Measure_StopCollecting_Delegate(uint id);
    internal delegate HResults V1_Measure_Save_Delegate(uint id, [MarshalAs(UnmanagedType.LPWStr)] string? name);
    internal delegate HResults V1_Measure_Drop_Delegate(uint id);
    internal delegate HResults V1_Measure_Detach_Delegate(uint id);
    // @formatter:on

    internal static readonly V1_Measure_CheckActive_Delegate V1_Measure_CheckActive;
    internal static readonly V1_Measure_StartCollecting_Delegate V1_Measure_StartCollecting;
    internal static readonly V1_Measure_StopCollecting_Delegate V1_Measure_StopCollecting;
    internal static readonly V1_Measure_Save_Delegate V1_Measure_Save;
    internal static readonly V1_Measure_Drop_Delegate V1_Measure_Drop;
    internal static readonly V1_Measure_Detach_Delegate V1_Measure_Detach;

    #endregion

    #region Memory

    // @formatter:off
    internal delegate HResults V1_Memory_CheckActive_Delegate(uint id, out MemoryFeatures features);
    internal delegate HResults V1_Memory_GetSnapshot_Delegate(uint id, [MarshalAs(UnmanagedType.LPWStr)] string? name);
    internal delegate HResults V1_Memory_ForceGc_Delegate(uint id);
    internal delegate HResults V1_Memory_CollectAllocations_Delegate(uint id, bool enable);
    internal delegate HResults V1_Memory_Detach_Delegate(uint id);
    // @formatter:on

    internal static readonly V1_Memory_CheckActive_Delegate V1_Memory_CheckActive;
    internal static readonly V1_Memory_GetSnapshot_Delegate V1_Memory_GetSnapshot;
    internal static readonly V1_Memory_ForceGc_Delegate V1_Memory_ForceGc;
    internal static readonly V1_Memory_CollectAllocations_Delegate V1_Memory_CollectAllocations;
    internal static readonly V1_Memory_Detach_Delegate V1_Memory_Detach;

    #endregion
  }
}