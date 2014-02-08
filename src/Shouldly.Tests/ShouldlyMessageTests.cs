using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    public class ShouldlyMessageTests
    {
        [Test]
        public void ShouldBe()
        {
            Should.Error(
                () => "expected".ShouldBe("actual"),
                "() => \"expected\" should be \"actual\" but was \"expected\""
            );

            Should.Error(
                () => 2.ShouldBe(1),
                "() => 2 should be 1 but was 2"
            );

            Should.Error(
                () => (new[] { 1, 2, 3 }).ShouldBe(new[] { 2, 2, 3 }),
                "() => (new[] { 1, 2, 3 }) should be [2, 2, 3] but was [1, 2, 3] difference [*1*, 2, 3]"
            );

            Should.Error(
                () => new UncomparableClass("ted").ShouldBe(new UncomparableClass("bob")),
                "() => new UncomparableClass(\"ted\") should be bob but was ted"
            );

            Should.Error(() =>
                         "SamplE".ShouldBe("sAMPLe", Case.Sensitive),
                         "'SamplE' should be 'sAMPLe' but was 'SamplE'");

            Should.Error(() =>
                         new[] { 2, 1 }.ShouldBe(new[] { 1, 2 }),
                         "new[] { 2, 1 } should be [1, 2] but was [2, 1] difference [*2*, *1*]"
                );

            var aWidget = new Widget { Name = "Joe", Enabled = true };
            var bWidget = new Widget { Name = "Joeyjojoshabadoo Jr", Enabled = true };

            IEnumerable<Widget> aEnumerable = aWidget.ToEnumerable();
            IEnumerable<Widget> bEnumerable = new[] { bWidget };

            Should.Error(() =>
                aEnumerable.ShouldBe(bEnumerable),
                "aEnumerable should be [Name(Joeyjojoshabadoo Jr) Enabled(True)] but was [Name(Joe) Enabled(True)] difference [*Name(Joe) Enabled(True)*]"
            );

            IEnumerable<int> something = null;
            Should.Error(
                () => something.ShouldBe(new[] { 1, 2, 3 }),
                "() => something should be [1, 2, 3] but was null");

            Action a = () => 1.ShouldBe(2);
            Should.Error(a,
                "Action a = () => 1 should be 2 but was 1");
        }

        [Test]
        public void ShouldBeTypeOf()
        {
            Should.Error(
                () => 2.ShouldBeTypeOf<double>(),
                "() => 2 should be type of System.Double but was System.Int32"
            );
        }

        public class Vampire { public int BitesTaken { get; set; } }

        [Test]
        public void ShouldContain()
        {
            var longString = new string('a', 110);

            Should.Error(
                () => longString.ShouldContain("zzzz"),
                string.Format("() => longString should contain \"zzzz\" but was \"{0}\"", longString.Substring(0, 100))
            );

            var justTheRightLengthString = new string('a', 80);
            Should.Error(
                () => justTheRightLengthString.ShouldContain("zzzz"),
                string.Format("() => justTheRightLengthString should contain \"zzzz\" but was \"{0}\"", justTheRightLengthString)
            );

            Should.Error(() =>
                new[] { 1, 2, 3 }.ShouldContain(5),
                "new[]{ 1, 2, 3 } should contain 5 but was [1, 2, 3]");

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

            Should.Error(() =>
                         new[] { 1, 2, 3 }.ShouldContain(x => x % 4 == 0),
                         "new[]{1,2,3} should contain an element satisfying the condition ((x % 4) == 0) but does not");

            Should.Error(() =>
                new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldContain(3.14, 0.001),
                "new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 } should contain 3.14 but was [1, 2.1, 3.14159265358979, 4.321, 5.4321]");

            Should.Error(() =>
                new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f }.ShouldContain(3.14f, 0.001),
                "new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f } should contain 3.14 but was [1, 2.1, 3.141593, 4.321, 5.4321]");
        }

        [Test]
        public void ShouldNotContain()
        {
            Should.Error(() =>
                         new[] { 1, 2, 3 }.ShouldNotContain(x => x % 3 == 0),
                         "new[]{1,2,3} should not contain an element satisfying the condition ((x % 3) == 0) but does");


            Should.Error(() =>
                new[] { 1, 2, 3 }.ShouldNotContain(2),
                "new[]{1,2,3} should not contain 2 but was [1, 2, 3]");
        }

        [Test]
        public void ShouldBeEmpty()
        {
            IEnumerable<object> objects = null;
            Should.Error(
                () => objects.ShouldBeEmpty(),
                "() => objects should be empty but was null");

            objects = (new[] { new object(), new object() });
            Should.Error(
                () => objects.ShouldBeEmpty(),
                "() => objects should be empty but was [System.Object, System.Object]");
        }

        [Test]
        public void ShouldNotBeEmpty()
        {
            IEnumerable<object> objects = null;
            Should.Error(
                () => objects.ShouldNotBeEmpty(),
                "() => objects should not be empty but was null");

            objects = new object[0];
            Should.Error(
                () => objects.ShouldNotBeEmpty(),
                "() => objects should not be empty but was");
        }

        [Test]
        public void ShouldBeSameAs()
        {
            var aReferenceType = new object();
            var anotherReferenceType = new object();
            Should.Error(
                () => aReferenceType.ShouldBeSameAs(anotherReferenceType),
                "() => aReferenceType should be same as System.Object but was System.Object"
            );

            var list = new List<int> { 1, 2, 3 };
            var equalListWithDifferentRef = new List<int> { 1, 2, 3 };
            Should.Error(
                () => list.ShouldBeSameAs(equalListWithDifferentRef),
                "() => list should be same as [1, 2, 3] but was [1, 2, 3] difference [1, 2, 3]"
            );

            const int boxedInt = 1;
            Should.Error(
                () => boxedInt.ShouldBeSameAs(boxedInt),
                "() => boxedInt should be same as 1 but was 1"
            );
        }

        [Test]
        public void ShouldNotThrow()
        {
            Should.Error(
                () => Shouldly.Should.NotThrow(() => { throw new IndexOutOfRangeException(); }),
                "() => Shouldly.Should not throw System.IndexOutOfRangeException but does"
                );
        }

        [Test]
        public void ShouldThrow()
        {
            Action shouldThrowAction =
            () => Shouldly.Should.Throw<NotImplementedException>(() =>
            {
                throw new RankException();
            });
            Should.Error(shouldThrowAction, "() => Shouldly.Should throw System.NotImplementedException but was System.RankException");

            shouldThrowAction =
            () => Shouldly.Should.Throw<NotImplementedException>(() => { });

            Should.Error(shouldThrowAction, "() => Shouldly.Should throw System.NotImplementedException but does not");
        }

        [Test]
        public void ShouldBeCloseTo()
        {
            const string testMessage = "muhst eat braiiinnzzzz";
            Should.Error(() =>
                         testMessage
                             .ShouldBeCloseTo("must eat brains"),
                         @"testMessage should be close to 'must eat brains' but was 'muhst eat braiiinnzzzz'");
        }

        private class UncomparableClass
        {
            private readonly string _description;

            public UncomparableClass(string description)
            {
                _description = description;
            }

            public override string ToString()
            {
                return _description;
            }
        }
    }
}