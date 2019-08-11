// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JetBrains.Profiler.Api.Impl.Linux
{
  internal static class RTLD
  {
    public const int RTLD_LAZY = 0x1;
    public const int RTLD_NOLOAD = 0x4;
    public const int RTLD_GLOBAL = 0x100;
  }
}