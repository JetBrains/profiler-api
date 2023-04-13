using System.Diagnostics.CodeAnalysis;

namespace JetBrains.Profiler.Api.Impl.MacOsX
{
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  [SuppressMessage("ReSharper", "IdentifierTypo")]
  internal static class RTLD
  {
    internal const int RTLD_LAZY = 0x1;
    internal const int RTLD_GLOBAL = 0x8;
    internal const int RTLD_NOLOAD = 0x10;
  }
}