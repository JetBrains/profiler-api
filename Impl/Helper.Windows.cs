using System;
using JetBrains.Profiler.Api.Impl.Windows;

namespace JetBrains.Profiler.Api.Impl
{
  internal static partial class Helper
  {
    public static bool IsCoreApiDllAlreadyLoaded()
    {
      return Kernel32Dll.GetModuleHandleW(CoreApiDll.LibraryName) != IntPtr.Zero;
    }
  }
}