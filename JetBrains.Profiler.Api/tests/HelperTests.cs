using System;
using JetBrains.Profiler.Api.Impl;
using NUnit.Framework;

namespace JetBrains.Profiler.Api.Tests
{
  [TestFixture]
  public sealed class HelperTests
  {
    [Test]
    public void IdTest()
    {
      Assert.AreEqual(Environment.Version.Major, (int)(ushort)(Helper.Id >> 16));
      Assert.AreEqual(Environment.Version.Minor, (int)(ushort)Helper.Id);
    }
  }
}