using System.Diagnostics.CodeAnalysis;

namespace JetBrains.Profiler.Api.Impl.Linux
{
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  [SuppressMessage("ReSharper", "IdentifierTypo")]
  internal static class RTLD
  {
    internal const int RTLD_LAZY = 0x1;
    internal const int RTLD_NOLOAD = 0x4;
    internal const int RTLD_GLOBAL = 0x100;
  }
}