using System;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Unix
{
  internal static class UnixHelper
  {
    private static readonly Lazy<PlatformId> ourPlatformLazy = new(DeducePlatform);

    public static PlatformId Platform => ourPlatformLazy.Value;

    private static string GetSysnameFromUname()
    {
      var buf = Marshal.AllocHGlobal(8 * 1024);
      try
      {
        if (LibC.uname(buf) != 0)
          throw new Exception("Failed to get Unix system name");

        // Note: utsname::sysname is the first member of structure, so simple take it!
        return Marshal.PtrToStringAnsi(buf) ?? "";
      }
      finally
      {
        Marshal.FreeHGlobal(buf);
      }
    }

    private static PlatformId DeducePlatform()
    {
      var sysname = GetSysnameFromUname();
      return sysname switch
        {
          "Darwin" => PlatformId.MacOsX,
          "Linux" => PlatformId.Linux,
          _ => throw new Exception("Unsupported system name: " + sysname)
        };
    }
  }
}