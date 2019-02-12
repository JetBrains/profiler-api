using System;
using System.Runtime.InteropServices;
using JetBrains.Profiler.Api.Impl.Unix;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class Helper
  {
    public static readonly uint Id =
      (uint) checked((ushort) Environment.Version.Major) << 16 |
      checked((ushort) Environment.Version.Minor);

    public static readonly PlatformId Platform = DeducePlatformId();

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

    private static PlatformId DeducePlatformId()
    {
      switch (Environment.OSVersion.Platform)
      {
      case PlatformID.Unix:
        var sysname = GetSysnameFromUname();
        switch (sysname)
        {
        case "Darwin":
          return PlatformId.MacOsX;
        case "Linux":
          return PlatformId.Linux;
        default:
          throw new Exception("Unsupported system name: " + sysname);
        }
      case PlatformID.Win32NT:
        return PlatformId.Windows;
      default:
        throw new PlatformNotSupportedException();
      }
    }

    public static bool ThrowOnError(HResults hr)
    {
      switch (hr)
      {
      case HResults.S_OK:
        return true;
      case HResults.S_FALSE:
        return false;
      default:
        throw new InternalProfilerException((int) hr);
      }
    }
  }
}