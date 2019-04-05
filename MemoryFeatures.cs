using System;

// ReSharper disable UnusedMember.Global

namespace JetBrains.Profiler.Api
{
  /// <summary>
  ///   Memory profiler feature set.
  /// </summary>
  [Flags]
  public enum MemoryFeatures : uint
  {
    /// <summary>
    ///   Not yet implemented.
    /// </summary>
    Ready = 0x1,

    /// <summary>
    ///   Not yet implemented.
    /// </summary>
    Detach = 0x2,

    /// <summary>
    ///   Indicates whether it is possible to use <see cref="MemoryProfiler.CollectAllocations" />:
    ///   <list type="table">
    ///     <item>
    ///       <term>0</term>
    ///       <description>the profiler ignores <see cref="MemoryProfiler.CollectAllocations" />.</description>
    ///     </item>
    ///     <item>
    ///       <term>1</term>
    ///       <description><see cref="MemoryProfiler.CollectAllocations" /> will take effect on the profiler.</description>
    ///     </item>
    ///   </list>
    /// </summary>
    CollectAllocations = 0x4
  }
}