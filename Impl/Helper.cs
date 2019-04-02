using System;
using System.Threading;

#if NETCOREAPP || NETSTANDARD
using System.Runtime.InteropServices;
#endif

namespace JetBrains.Profiler.Api.Impl
{
  internal static class Helper
  {
    #region Delegates

    public delegate bool IsDoneDelegate();

    #endregion

    private static readonly Lazy<uint> ourId = new Lazy<uint>(DeduceId);
    private static readonly Lazy<PlatformId> ourPlatform = new Lazy<PlatformId>(DeducePlatformId);

    public static uint Id => ourId.Value;
    public static PlatformId Platform => ourPlatform.Value;

    private static uint DeduceId()
    {
#if NETCOREAPP1_0 || NETCOREAPP1_1
      const ushort major = 4;
      const ushort minor = 0;
#else
      var version = Environment.Version;
      var major = checked((ushort) version.Major);
      var minor = checked((ushort) version.Minor);
#endif
      return (uint) major << 16 | minor;
    }

    private static PlatformId DeducePlatformId()
    {
#if NETCOREAPP || NETSTANDARD
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        return PlatformId.Windows;
      if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        return PlatformId.MacOsX;
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        return PlatformId.Linux;
#else
      switch (Environment.OSVersion.Platform)
      {
      case PlatformID.Unix:
        return UnixHelper.IsMacOsX ? PlatformId.MacOsX : PlatformId.Linux;
      case PlatformID.Win32NT:
        return PlatformId.Windows;
      }
#endif
      throw new PlatformNotSupportedException();
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

    public static bool WaitFor(TimeSpan timeout, IsDoneDelegate isDone)
    {
      var endTime = DateTime.UtcNow + timeout;
      while (!isDone())
      {
        if (DateTime.UtcNow >= endTime)
          return false;
        Thread.Sleep(100);
      }
      return true;
    }
  }
}