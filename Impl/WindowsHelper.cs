using System;
using JetBrains.Profiler.Api.Impl.Windows;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class WindowsHelper
  {
    public static bool IsCoreApiDllAlreadyLoaded()
    {
      return Kernel32Dll.GetModuleHandleW(CoreApiDll.LibraryName) != IntPtr.Zero;
    }
  }
}