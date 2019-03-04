using System;
using System.Threading;
using JetBrains.Profiler.Api.Impl.Windows;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class WindowsHelper
  {
    // ReSharper disable once UseStringInterpolation
    private static readonly string ourWindowsEventName = string.Format("JetBrains.Profiler.Windows.Api_{0}_v{1}.{2}",
      Kernel32Dll.GetCurrentProcessId(),
      Environment.Version.Major,
      Environment.Version.Minor);

    public static bool WaitForReadySignal(TimeSpan timeout)
    {
      var endTime = DateTime.UtcNow + timeout;
      var handle = IntPtr.Zero;
      try
      {
        while ((handle = Kernel32Dll.OpenEventW((uint) AccessRights.SYNCHRONIZE, false, ourWindowsEventName)) == IntPtr.Zero)
        {
          if (DateTime.UtcNow >= endTime)
            return false;
          Thread.Sleep(100);
        }
        return (WaitResult) Kernel32Dll.WaitForSingleObject(handle, NormalizeForWait(DateTime.UtcNow, endTime)) == WaitResult.WAIT_OBJECT_0;
      }
      finally
      {
        if (handle != IntPtr.Zero)
          Kernel32Dll.CloseHandle(handle);
      }
    }

    public static bool IsCoreApiDllAlreadyLoaded()
    {
      return Kernel32Dll.GetModuleHandleW(CoreApiDll.LibraryName) != IntPtr.Zero;
    }

    private static uint NormalizeForWait(DateTime startTime, DateTime endTime)
    {
      // Note: WaitInfinite.INFINITE == uint.MaxValue !!!
      var milliseconds = (endTime - startTime).TotalMilliseconds;
      if (milliseconds < 0)
        return 0;
      if ((uint) WaitInfinite.INFINITE <= milliseconds)
        return (uint) WaitInfinite.INFINITE - 1;
      return checked((uint) milliseconds);
    }
  }
}