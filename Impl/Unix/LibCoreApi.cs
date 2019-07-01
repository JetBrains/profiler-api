using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Unix
{
  internal static class LibCoreApi
  {
    public const string LibraryName = "libJetBrains.Profiler.CoreApi"; // Note: No extension here, because should works on Linux and MacOsX

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

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern HResults V1_Measure_Detach(uint id);

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

    [DllImport(LibraryName, ExactSpelling = true)]
    public static extern HResults V1_Memory_Detach(uint id);

    #endregion
  }
}