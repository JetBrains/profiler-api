using System;
using System.Threading;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class Helper
  {
    #region Delegates

    public delegate bool IsDoneDelegate();

    #endregion

    public static readonly uint Id =
      (uint) checked((ushort) Environment.Version.Major) << 16 |
      checked((ushort) Environment.Version.Minor);

    public static readonly PlatformId Platform = DeducePlatformId();

    private static PlatformId DeducePlatformId()
    {
      switch (Environment.OSVersion.Platform)
      {
      case PlatformID.Unix:
        return UnixHelper.IsMacOsX ? PlatformId.MacOsX : PlatformId.Linux;
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