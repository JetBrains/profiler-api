using System;
using NUnit.Framework;

namespace JetBrains.Profiler.Api.Tests
{
  public class HelperTests
  {
    [Test]
    public void IdTest()
    {
      Assert.AreEqual(Environment.Version.Major, (int) (ushort) (Impl.Helper.Id >> 16));
      Assert.AreEqual(Environment.Version.Minor, (int) (ushort) Impl.Helper.Id);
    }

    [Platform("MacOsX")]
    [Test]
    public void PlatformMacOsXTest() => Assert.AreEqual(Impl.PlatformId.MacOsX, Impl.Helper.Platform);

    [Platform("Linux")]
    [Test]
    public void PlatformLinuxTest() => Assert.AreEqual(Impl.PlatformId.Linux, Impl.Helper.Platform);

    [Platform("Win32NT")]
    [Test]
    public void PlatformWindowsTest() => Assert.AreEqual(Impl.PlatformId.Windows, Impl.Helper.Platform);
  }
}