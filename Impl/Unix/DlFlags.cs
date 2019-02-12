using System;

// ReSharper disable InconsistentNaming

namespace JetBrains.Profiler.Api.Impl.Unix
{
  [Flags]
  internal enum DlFlags
  {
    RTLD_LOCAL = 0x00000,
    RTLD_LAZY = 0x00001,
    RTLD_NOW = 0x00002,
    RTLD_NOLOAD = 0x00004,
    RTLD_DEEPBIND = 0x00008,
    RTLD_GLOBAL = 0x00100,
    RTLD_NODELETE = 0x01000
  }
}