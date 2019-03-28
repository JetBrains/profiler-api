using System;
using System.Threading;

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
      return (uint) checked((ushort) Environment.Version.Major) << 16 |
             checked((ushort) Environment.Version.Minor);
    }

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