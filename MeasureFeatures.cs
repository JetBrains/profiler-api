using System;

// ReSharper disable InconsistentNaming
// ReSharper disable UnusedMember.Global

namespace JetBrains.Profiler.Api
{
  [Flags]
  public enum MeasureFeatures : uint
  {
    None = 0x0,
    Detach = 0x1
  }
}