using System;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JetBrains.Profiler.Api.Impl.Linux
{
  [Flags]
  internal enum RTLD
  {
    RTLD_LAZY = 0x1,
    RTLD_NOLOAD = 0x4,
    RTLD_GLOBAL = 0x100
  }
}