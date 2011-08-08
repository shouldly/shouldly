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

        public class Vampire
        {
            public int BitesTaken { get; set; }
        }

        [Test]
        public void ShouldContain_WithPredicate_UsingObjectsShouldDisplayMeaningfulMessage()
        {
            var vampires = new[]
                               {
                                   new Vampire {BitesTaken = 1},
                                   new Vampire {BitesTaken = 2},
                                   new Vampire {BitesTaken = 3},
                                   new Vampire {BitesTaken = 4},
                                   new Vampire {BitesTaken = 5},
                                   new Vampire {BitesTaken = 6},
                               };

            Should.Error(() =>
                vampires.ShouldContain(x => x.BitesTaken > 7),
                "vampires should contain an element satisfying the condition (x.BitesTaken > 7) but does not");
        }

        [Test]
        public void ShouldContain_WithPredicate_WhenTrue_ShouldNotThrow()
        {
            new[]{1,2,3}.ShouldContain(x => x % 3 == 0);
        }

        [Test]
        public void ShouldContain_WithPredicate_WhenFalse_ShouldErrorWithMessage()
        {
            Should.Error(() =>
            new[]{1,2,3}.ShouldContain(x => x % 4 == 0),
            "new[]{1,2,3} should contain an element satisfying the condition ((x % 4) = 0) but does not");
        }

        [Test]
        public void ShouldNotContain_WithPredicate_WhenTrue_ShouldNotThrow()
        {
            new[] { 1, 2, 3 }.ShouldNotContain(x => x % 4 == 0);
        }

        [Test]
        public void ShouldNotContain_WithPredicate_WhenFalse_ShouldErrorWithMessage()
        {
            Should.Error(() =>
            new[] { 1, 2, 3 }.ShouldNotContain(x => x % 3 == 0),
            "new[]{1,2,3} should not contain an element satisfying the condition ((x % 3) = 0) but does");
        }

        [Test]
        public void ShouldNotContain_WhenFalse_ShouldErrorWithMessage()
        {
            Should.Error(() => 
                new[] { 1, 2, 3 }.ShouldNotContain(2),
                "new[]{1,2,3} should not contain 2 but was [1, 2, 3]");
        }

        [Test]
        public void ShouldContain_WithNumbersWhenTrue_ShouldAllowTolerance()
        {
            new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldNotContain(3.14);
            new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldContain(3.14, 0.01);
            new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f }.ShouldContain(3.14f, 0.01);
        }

        [Test]
        public void ShouldContain_WithNumbersWhenFalse_ShouldErrorWithMessage()
        {
            Should.Error(() =>
                new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldContain(3.14, 0.001),
                "new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 } should contain 3.14 but was [1, 2.1, 3.14159265358979, 4.321, 5.4321]");
            Should.Error(() =>
                new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f }.ShouldContain(3.14f, 0.001),
                "new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f } should contain 3.14 but was [1, 2.1, 3.141593, 4.321, 5.4321]");
        }

    }
}