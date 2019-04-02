using System;

// ReSharper disable UnusedMember.Global

namespace JetBrains.Profiler.Api
{
  [Flags]
  public enum MemoryFeatures : uint
  {
    None = 0x0,
    Detach = 0x1,
    CollectAllocations = 0x2
  }
}