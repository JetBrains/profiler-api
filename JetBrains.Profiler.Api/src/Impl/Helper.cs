namespace JetBrains.Profiler.Api.Impl
{
  internal static class Helper
  {
    public static readonly uint Id;

    static Helper()
    {
#if NETCOREAPP1_0 || NETCOREAPP1_1 || NETSTANDARD1_0 || NETSTANDARD1_1 || NETSTANDARD1_2 || NETSTANDARD1_3 || NETSTANDARD1_4 || NETSTANDARD1_5 || NETSTANDARD1_6
      const ushort major = 4;
      const ushort minor = 0;
#else
      var version = System.Environment.Version;
      var major = checked((ushort) version.Major);
      var minor = checked((ushort) version.Minor);
#endif
      Id = (uint) major << 16 | minor;
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