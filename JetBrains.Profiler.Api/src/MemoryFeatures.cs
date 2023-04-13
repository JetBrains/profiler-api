using System;
using System.Diagnostics.CodeAnalysis;

namespace JetBrains.Profiler.Api
{
  /// <summary>
  ///   Memory profiler feature set.
  /// </summary>
  [SuppressMessage("ReSharper", "UnusedMember.Global")]
  [Flags]
  public enum MemoryFeatures : uint
  {
    /// <summary>
    ///   Indicates whether the profiler is ready to work. You need to check this flag only in case you attach the
    ///   profiler to the current process: the flag will be set once the profiler is ready to accept your commands. If
    ///   you start a process under profiling, this flag is always set. After you detach the profiler from the process,
    ///   the flag is cleared.
    /// </summary>
    Ready = 0x1,

    /// <summary>
    ///   Indicates whether it is possible to use <see cref="MemoryProfiler.Detach" />:
    ///   <list type="table">
    ///     <item>
    ///       <term>0</term>
    ///       <description>the profiler ignores <see cref="MemoryProfiler.Detach" />.</description>
    ///     </item>
    ///     <item>
    ///       <term>1</term>
    ///       <description><see cref="MemoryProfiler.Detach" /> will take effect on the profiler.</description>
    ///     </item>
    ///   </list>
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