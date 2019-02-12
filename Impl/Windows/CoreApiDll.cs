using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Windows
{
  internal static class CoreApiDll
  {
    public const string LibraryName = "JetBrains.Profiler.CoreApi.dll";

    #region Measure

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern HResults V1_Measure_CheckActive(uint id, out MeasureFeatures features);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern HResults V1_Measure_StartCollecting(uint id, [MarshalAs(UnmanagedType.LPWStr)] string groupName);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern HResults V1_Measure_StopCollecting(uint id);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern HResults V1_Measure_Save(uint id, [MarshalAs(UnmanagedType.LPWStr)] string name);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern HResults V1_Measure_Drop(uint id);

    #endregion

    #region Memory

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern HResults V1_Memory_CheckActive(uint id, out MemoryFeatures features);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern HResults V1_Memory_GetSnapshot(uint id, [MarshalAs(UnmanagedType.LPWStr)] string name);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern HResults V1_Memory_ForceGc(uint id);

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern HResults V1_Memory_CollectAllocations(uint id, bool enable);

    #endregion
  }
}