// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JetBrains.Profiler.Api.Impl.MacOsX
{
  internal static class RTLD
  {
    public const int RTLD_LAZY = 0x1;
    public const int RTLD_GLOBAL = 0x8;
    public const int RTLD_NOLOAD = 0x10;
  }
}