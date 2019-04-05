using System;

// ReSharper disable UnusedMember.Global

namespace JetBrains.Profiler.Api
{
  /// <summary>
  ///   Performance/coverage profiler feature set.
  /// </summary>
  [Flags]
  public enum MeasureFeatures : uint
  {
    /// <summary>
    ///   Not yet implemented.
    /// </summary>
    Ready = 0x1,

    /// <summary>
    ///   Not yet implemented.
    /// </summary>
    Detach = 0x2
  }
}