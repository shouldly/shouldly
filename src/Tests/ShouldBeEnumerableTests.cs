using System;
using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    public class ShouldBeEnumerableTests
    {
        [Test]
        public void ShouldContain_WhenTrue_ShouldNotThrow()
        {
            new[]{1,2,3}.ShouldContain(2);
        }

        [Test]
        public void ShouldContain_WhenFalse_ShouldErrorWithMessage()
        {
            Should.Error(() =>
                new[] { 1, 2, 3 }.ShouldContain(5),
                "new[]{1,2,3} should contain 5 but was [1, 2, 3]");
        }

        [Test]
        public void ShouldNotContain_WhenTrue_ShouldNotThrow()
        {
            new[]{1,2,3}.ShouldNotContain(5);
        }

        [Test]
        public void ShouldNotContain_WhenFalse_ShouldErrorWithMessage()
        {
            Should.Error(() => 
                new[] { 1, 2, 3 }.ShouldNotContain(2),
                "new[]{1,2,3} should not contain 2 but was [1, 2, 3]");
        }
    }
}