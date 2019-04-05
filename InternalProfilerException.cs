using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api
{
  /// <summary>
  ///   Internal profiler exception.
  /// </summary>
  public class InternalProfilerException : COMException
  {
    /// <summary>
    ///   Construct the internal profiler exception.
    /// </summary>
    /// <param name="hr">HRESULT value.</param>
    public InternalProfilerException(int hr) : base("Internal profiler error", hr)
    {
    }
  }
}