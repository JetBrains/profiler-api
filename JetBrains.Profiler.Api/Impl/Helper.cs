using System;
using System.Runtime.InteropServices;
using JetBrains.Profiler.Api.Impl.Unix;

namespace JetBrains.Profiler.Api.Impl
{
  internal static class Helper
  {
    private static readonly Lazy<uint> ourId = new Lazy<uint>(DeduceId);
    private static readonly Lazy<PlatformId> ourPlatform = new Lazy<PlatformId>(DeducePlatformId);

    public static uint Id => ourId.Value;
    public static PlatformId Platform => ourPlatform.Value;

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

    private static PlatformId DeducePlatformId()
    {
#if NETSTANDARD1_0
#error No OS detection possible
#elif NET20 || NET35 || NET40 || NET45 || NET451 || NET452 || NET46 || NET461 || NET462 || NET47
      switch (Environment.OSVersion.Platform)
      {
      case PlatformID.Unix:
        return UnixHelper.IsMacOsX ? PlatformId.MacOsX : PlatformId.Linux;
      case PlatformID.Win32NT:
        return PlatformId.Windows;
      }
#else
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        return PlatformId.Windows;
      if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        return PlatformId.MacOsX;
      if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        return PlatformId.Linux;
#endif
      throw new PlatformNotSupportedException();
    }

    public static bool ThrowIfFail(HResults hr)
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

#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
    public static bool IsEntryPointNotFoundException(TypeLoadException e)
    {
      // Bug: System.EntryPointNotFoundException is private class in .NET Standard 1.x and .NET Core 1.x.
      return e.GetType().FullName == "System.EntryPointNotFoundException";
    }
#endif
  }
}