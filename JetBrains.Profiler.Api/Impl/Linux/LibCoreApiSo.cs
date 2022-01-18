using System;
using System.Runtime.InteropServices;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JetBrains.Profiler.Api.Impl.Linux
{
  internal static class LibCoreApiSo
  {
    public const string LibraryName = "libJetBrains.Profiler.CoreApi.so";

    #region Measure

    public delegate HResults V1_Measure_CheckActive_Delegate(uint id, out MeasureFeatures features);
    public delegate HResults V1_Measure_StartCollecting_Delegate(uint id, [MarshalAs(UnmanagedType.LPWStr)] string groupName);
    public delegate HResults V1_Measure_StopCollecting_Delegate(uint id);
    public delegate HResults V1_Measure_Save_Delegate(uint id, [MarshalAs(UnmanagedType.LPWStr)] string name);
    public delegate HResults V1_Measure_Drop_Delegate(uint id);
    public delegate HResults V1_Measure_Detach_Delegate(uint id);

    public static readonly V1_Measure_CheckActive_Delegate V1_Measure_CheckActive;
    public static readonly V1_Measure_StartCollecting_Delegate V1_Measure_StartCollecting;
    public static readonly V1_Measure_StopCollecting_Delegate V1_Measure_StopCollecting;
    public static readonly V1_Measure_Save_Delegate V1_Measure_Save;
    public static readonly V1_Measure_Drop_Delegate V1_Measure_Drop;
    public static readonly V1_Measure_Detach_Delegate V1_Measure_Detach;

    #endregion

    #region Memory

    public delegate HResults V1_Memory_CheckActive_Delegate(uint id, out MemoryFeatures features);
    public delegate HResults V1_Memory_GetSnapshot_Delegate(uint id, [MarshalAs(UnmanagedType.LPWStr)] string name);
    public delegate HResults V1_Memory_ForceGc_Delegate(uint id);
    public delegate HResults V1_Memory_CollectAllocations_Delegate(uint id, bool enable);
    public delegate HResults V1_Memory_Detach_Delegate(uint id);

    public static readonly V1_Memory_CheckActive_Delegate V1_Memory_CheckActive;
    public static readonly V1_Memory_GetSnapshot_Delegate V1_Memory_GetSnapshot;
    public static readonly V1_Memory_ForceGc_Delegate V1_Memory_ForceGc;
    public static readonly V1_Memory_CollectAllocations_Delegate V1_Memory_CollectAllocations;
    public static readonly V1_Memory_Detach_Delegate V1_Memory_Detach;

    #endregion

    private static TDelegate GetUnmanagedFunction<TDelegate>(IntPtr handle, string functionName) where TDelegate : class
    {
      var ptr = LibDlSo2.dlsym(handle, functionName);
      if (ptr == IntPtr.Zero)
        throw new Exception($"Failed to get function {functionName}");
      return (TDelegate)(object)Marshal.GetDelegateForFunctionPointer(ptr, typeof(TDelegate));
    }

    static LibCoreApiSo()
    {
      var libraryPath = LinuxHelper.DlIteratePhdrFindLibraryPath(LibraryName);
      var handle = libraryPath != null ? LibDlSo2.dlopen(libraryPath, RTLD.RTLD_GLOBAL | RTLD.RTLD_LAZY) : IntPtr.Zero;
      if (handle == IntPtr.Zero)
        throw new DllNotFoundException($"Failed to load library {LibraryName}");
      
      // Note(ww898): Do not call LibDlSo2.dlclose(handle) because no unload possible!!!

      V1_Measure_CheckActive = GetUnmanagedFunction<V1_Measure_CheckActive_Delegate>(handle, nameof(V1_Measure_CheckActive));
      V1_Measure_StartCollecting = GetUnmanagedFunction<V1_Measure_StartCollecting_Delegate>(handle, nameof(V1_Measure_StartCollecting));
      V1_Measure_StopCollecting = GetUnmanagedFunction<V1_Measure_StopCollecting_Delegate>(handle, nameof(V1_Measure_StopCollecting));
      V1_Measure_Save = GetUnmanagedFunction<V1_Measure_Save_Delegate>(handle, nameof(V1_Measure_Save));
      V1_Measure_Drop = GetUnmanagedFunction<V1_Measure_Drop_Delegate>(handle, nameof(V1_Measure_Drop));
      V1_Measure_Detach = GetUnmanagedFunction<V1_Measure_Detach_Delegate>(handle, nameof(V1_Measure_Detach));

      V1_Memory_CheckActive = GetUnmanagedFunction<V1_Memory_CheckActive_Delegate>(handle, nameof(V1_Memory_CheckActive));
      V1_Memory_GetSnapshot = GetUnmanagedFunction<V1_Memory_GetSnapshot_Delegate>(handle, nameof(V1_Memory_GetSnapshot));
      V1_Memory_ForceGc = GetUnmanagedFunction<V1_Memory_ForceGc_Delegate>(handle, nameof(V1_Memory_ForceGc));
      V1_Memory_CollectAllocations = GetUnmanagedFunction<V1_Memory_CollectAllocations_Delegate>(handle, nameof(V1_Memory_CollectAllocations));
      V1_Memory_Detach = GetUnmanagedFunction<V1_Memory_Detach_Delegate>(handle, nameof(V1_Memory_Detach));
    }

    #region Nested type: SafeDlHandle

    #endregion
  }
}