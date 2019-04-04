using System;

// ReSharper disable UnusedMember.Global

namespace JetBrains.Profiler.Api
{
  [Flags]
  public enum MemoryFeatures : uint
  {
    Inactive = 0x0,
    Ready = 0x1,
    Detach = 0x2,
    CollectAllocations = 0x4
  }
}