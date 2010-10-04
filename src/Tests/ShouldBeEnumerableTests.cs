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

        [Test]
        public void ShouldContain_WithNumbersWhenTrue_ShouldAllowTolerance() {
            new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldNotContain(3.14);
            new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldContain(3.14, 0.01);
            new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f }.ShouldContain(3.14f, 0.01);
        }

        [Test]
        public void ShouldContain_WithNumbersWhenFalse_ShouldErrorWithMessage() {
            Should.Error(() =>
                new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldContain(3.14, 0.001),
                "new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 } should contain 3.14 but was [1, 2.1, 3.14159265358979, 4.321, 5.4321]");
            Should.Error(() =>
                new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f }.ShouldContain(3.14f, 0.001),
                "new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f } should contain 3.14 but was [1, 2.1, 3.141593, 4.321, 5.4321]");
        }
    }
}