using System;
using System.Runtime.InteropServices;
using JetBrains.Profiler.Api.Impl.Unix;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class Helper
  {
    private static readonly Lazy<uint> ourIdLazy = new(DeduceId);
    private static readonly Lazy<PlatformId> ourPlatformLazy = new(DeducePlatform);

    public static uint Id => ourIdLazy.Value;
    public static PlatformId Platform => ourPlatformLazy.Value;

    private static uint DeduceId()
    {
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
      const ushort major = 4;
      const ushort minor = 0;
#else
      var version = Environment.Version;
      var major = checked((ushort) version.Major);
      var minor = checked((ushort) version.Minor);
#endif
      return (uint) major << 16 | minor;
    }

    private static PlatformId DeducePlatform()
    {
#if NETSTANDARD1_0
#error No OS detection possible
#elif NET20 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47
      return Environment.OSVersion.Platform switch
        {
          PlatformID.Unix => UnixHelper.Platform,
          PlatformID.Win32NT => PlatformId.Windows,
          _ => throw new PlatformNotSupportedException()
        };
#else
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        return PlatformId.Windows;
      if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        return PlatformId.MacOsX;
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        return PlatformId.Linux;
      throw new PlatformNotSupportedException();
#endif
    }

    public static bool ThrowIfFail(HResults hr)
    {
      return hr switch
        {
          HResults.S_OK => true,
          HResults.S_FALSE => false,
          _ => throw new InternalProfilerException((int)hr)
        };
    }
  }
}