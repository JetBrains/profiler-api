using System.Runtime.InteropServices;

namespace JetBrains.Profiler.Api
{
  public class InternalProfilerException : COMException
  {
    public InternalProfilerException(int hr) : base("Internal profiler error", hr)
    {
    }
  }
}