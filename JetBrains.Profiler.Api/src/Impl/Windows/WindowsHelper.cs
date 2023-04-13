using System;

namespace JetBrains.Profiler.Api.Impl.Windows
{
  internal static class WindowsHelper
  {
    internal static bool IsCoreApiAlreadyLoaded()
    {
      return Kernel32Dll.GetModuleHandleW(CoreApiDll.LibraryName) != IntPtr.Zero;
    }
  }
}