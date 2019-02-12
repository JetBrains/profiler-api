using System;
using JetBrains.Profiler.Api.Impl.Unix;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class LinuxHelper
  {
    public static bool WaitForReadySignal(TimeSpan timeout)
    {
      throw new NotImplementedException();
    }

    public static bool IsCoreApiLoaded()
    {
      var handle = LibDlSo.dlopen(CoreApiSo.LibraryName, (int) (DlFlags.RTLD_GLOBAL | DlFlags.RTLD_LAZY | DlFlags.RTLD_NOLOAD));
      if (handle == IntPtr.Zero)
        return false;
      LibDlSo.dlclose(handle);
      return true;
    }
  }
}