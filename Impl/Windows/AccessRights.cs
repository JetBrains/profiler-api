using System;

// ReSharper disable InconsistentNaming

namespace JetBrains.Profiler.Api.Impl.Windows
{
  [Flags]
  internal enum AccessRights : uint
  {
    SYNCHRONIZE = 0x00100000
  }
}