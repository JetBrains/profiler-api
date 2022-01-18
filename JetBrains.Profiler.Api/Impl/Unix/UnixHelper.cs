using System;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Unix
{
  internal static class UnixHelper
  {
    private static readonly Lazy<bool> ourIsMacOsX = new(DeduceIsMacOsX);

    public static bool IsMacOsX => ourIsMacOsX.Value;

    private static string GetSysnameFromUname()
    {
      var buf = Marshal.AllocHGlobal(8 * 1024);
      try
      {
        if (LibC.uname(buf) != 0)
          throw new Exception("Failed to get Unix system name");

        // Note: utsname::sysname is the first member of structure, so simple take it!
        return Marshal.PtrToStringAnsi(buf);
      }
      finally
      {
        Marshal.FreeHGlobal(buf);
      }
    }

    private static bool DeduceIsMacOsX()
    {
      var sysname = GetSysnameFromUname();
      switch (sysname)
      {
      case "Darwin":
        return true;
      case "Linux":
        return false;
      default:
        throw new Exception("Unsupported system name: " + sysname);
      }
    }
  }
}