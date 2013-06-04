using System;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldBeEnumerableTests
    {
        [Test]
        public void ShouldContain_WhenTrue_ShouldNotThrow()
        {
            new[] { 1, 2, 3 }.ShouldContain(2);
        }

        [Test]
        public void ShouldNotContain_WhenTrue_ShouldNotThrow()
        {
            new[] { 1, 2, 3 }.ShouldNotContain(5);
        }

        [Test]
        public void ShouldContain_WithPredicate_WhenTrue_ShouldNotThrow()
        {
            new[] { 1, 2, 3 }.ShouldContain(x => x % 3 == 0);
        }

        [Test]
        public void ShouldNotContain_WithPredicate_WhenTrue_ShouldNotThrow()
        {
            new[] { 1, 2, 3 }.ShouldNotContain(x => x % 4 == 0);
        }

        [Test]
        public void ShouldAllBe_WithPredicate_WhenTrue_ShouldNotThrow()
        {
            new[] { 1, 2, 3 }.ShouldAllBe(x => x < 4);
        }


        [Test]
        public void ShouldlyMessage_WhenComparingMatchingStringsOver100Characters_ShouldNotClipStringForComparison()
        {
            var longString = new string('a', 110) + "zzzz";

            Should.NotError(
              () => longString.ShouldContain("zzzz"));
        }

        [Test]
        public void ShouldContain_WithNumbersWhenTrue_ShouldAllowTolerance()
        {
            new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldNotContain(3.14);
            new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldContain(3.14, 0.01);
            new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f }.ShouldContain(3.14f, 0.01);
        }

        [Test]
        public void ShouldBeEmpty_WhenEmpty_ShouldNotError()
        {
            Should.NotError(() => new object[0].ShouldBeEmpty());
        }

        [Test]
        public void ShouldNotBeEmpty_WhenNotEmpty_ShouldNotError()
        {
            Should.NotError(() => new[] { new object() }.ShouldNotBeEmpty());
        }
    }
}