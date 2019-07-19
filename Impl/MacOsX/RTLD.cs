using System;

// ReSharper disable InconsistentNaming
// ReSharper disable IdentifierTypo

namespace JetBrains.Profiler.Api.Impl.MacOsX
{
  [Flags]
  internal enum RTLD
  {
    RTLD_LAZY = 0x1,
    RTLD_GLOBAL = 0x8,
    RTLD_NOLOAD = 0x10
  }
}