using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api.Impl.Linux
{
  /// <summary>
  ///   For other fields, check the size argument of <see cref="LibCSo6.dl_iterate_phdr_callback" /> first.
  /// </summary>
  [SuppressMessage("ReSharper", "InconsistentNaming")]
  [SuppressMessage("ReSharper", "IdentifierTypo")]
  [SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
  [SuppressMessage("ReSharper", "FieldCanBeMadeReadOnly.Global")]
  [StructLayout(LayoutKind.Sequential)]
  internal struct dl_phdr_info
  {
    public nuint dlpi_addr;
    public IntPtr dlpi_name;
    public IntPtr dlpi_phdr;
    public ushort dlpi_phnum;
  }
}