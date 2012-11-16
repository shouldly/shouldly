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
        public void CanGenerateErrorMessage()
        {
            Should.Error(
                () => "expected".ShouldBe("actual"),
                "() => \"expected\" should be \"actual\" but was \"expected\""
            );
        }

        [Test]
        public void CanGenerate()
        {
            Should.Error(
                () => 2.ShouldBe(1),
                "() => 2 should be 1 but was 2"
                );
        }

        [Test]
        public void ShouldlyMessage_PassedCollectionsWhichCanBeCompared_ShouldShowDifferences()
        {
            Should.Error(
                () => (new[] { 1, 2, 3 }).ShouldBe(new[] { 2, 2, 3 }),
                "() => (new[] { 1, 2, 3 }) should be [2, 2, 3] but was [1, 2, 3] difference [*1*, 2, 3]"
            );
        }

        [Test]
        public void ShouldlyMessage_CanGenerateForOfTypeAssertion()
        {
            Should.Error(
                () => 2.ShouldBeTypeOf<double>(),
                "() => 2 should be type of System.Double but was System.Int32"
                );
        }

        [Test]
        public void ShouldlyMessage_PassedObjectsWhichCannotCompared_ShouldNotShowDifferences()
        {
            Should.Error(
                () => new UncomparableClass("ted").ShouldBe(new UncomparableClass("bob")),
                "() => new UncomparableClass(\"ted\") should be bob but was ted"
            );
        }

        [Test]
        public void ShouldlyMessage_WhenComparingStringsOver100Characters_ShouldLimitTheMessageTo100Characters()
        {
            var longString = new string('a', 110);
            Should.Error(
              () => longString.ShouldContain("zzzz"),
              string.Format("() => longString should contain \"zzzz\" but was \"{0}\"", longString.Substring(0, 100))
          );
        }

        [Test]
        public void ShouldlyMessage_WhenComparingMatchingStringsOver100Characters_ShouldNotClipStringForComparison()
        {
            var longString = new string('a', 110) + "zzzz";

            Should.NotError(
              () => longString.ShouldContain("zzzz"));
        }

        [Test]
        public void ShouldlyMessage_WhenComparingStringsUnder100Characters_ShouldNotLimitTheMessage()
        {
            var longString = new string('a', 80);
            Should.Error(
              () => longString.ShouldContain("zzzz"),
              string.Format("() => longString should contain \"zzzz\" but was \"{0}\"", longString)
          );
        }

        [Test]
        public void ShouldContain_WhenFalse_ShouldErrorWithMessage()
        {
            Should.Error(() =>
                new[] { 1, 2, 3 }.ShouldContain(5),
                "new[]{ 1, 2, 3 } should contain 5 but was [1, 2, 3]");
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
        public void ShouldContain_WithPredicate_WhenFalse_ShouldErrorWithMessage()
        {
            Should.Error(() =>
                         new[] { 1, 2, 3 }.ShouldContain(x => x % 4 == 0),
                         "new[]{1,2,3} should contain an element satisfying the condition ((x % 4) = 0) but does not");
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
        public void ShouldContain_WithNumbersWhenFalse_ShouldErrorWithMessage()
        {
            Should.Error(() =>
                new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 }.ShouldContain(3.14, 0.001),
                "new[] { 1.0, 2.1, Math.PI, 4.321, 5.4321 } should contain 3.14 but was [1, 2.1, 3.14159265358979, 4.321, 5.4321]");
            Should.Error(() =>
                new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f }.ShouldContain(3.14f, 0.001),
                "new[] { 1.0f, 2.1f, (float)Math.PI, 4.321f, 5.4321f } should contain 3.14 but was [1, 2.1, 3.141593, 4.321, 5.4321]");
        }

        [Test]
        public void ShouldBeEmpty_WhenNull_ShouldError()
        {
            IEnumerable<object> objects = null;
            Should.Error(
                () => objects.ShouldBeEmpty(),
                "() => objects should be empty but was null");
        }

        [Test]
        public void ShouldBeEmpty_WhenNotEmpty_ShouldError()
        {
            var objects = (new[] { new object(), new object() });
            Should.Error(
                () => objects.ShouldBeEmpty(),
                "() => objects should be empty but was [System.Object, System.Object]");
        }

        [Test]
        public void ShouldNotBeEmpty_WhenNull_ShouldError()
        {
            IEnumerable<object> objects = null;
            Should.Error(
                () => objects.ShouldNotBeEmpty(),
                "() => objects should not be empty but was null");
        }

        [Test]
        public void ShouldNotBeEmpty_WhenEmpty_ShouldError()
        {
            var objects = new object[0];
            Should.Error(
                () => objects.ShouldNotBeEmpty(),
                "() => objects should not be empty but was");
        }

        [Test]
        public void ShouldBeSameAs_WhenDifferentReferences_ShouldThrow()
        {
            var first = new object();
            var second = new object();

            Should.Error(
                () => first.ShouldBeSameAs(second),
                "() => first should be same as System.Object but was System.Object"
            );
        }

        [Test]
        public void ShouldBeSameAs_WhenEqualListsButDifferentReferences_ShouldThrow()
        {
            var list = new List<int> { 1, 2, 3 };
            var equalListWithDifferentRef = new List<int> { 1, 2, 3 };

            Should.Error(
                () => list.ShouldBeSameAs(equalListWithDifferentRef),
                "() => list should be same as [1, 2, 3] but was [1, 2, 3] difference [1, 2, 3]"
            );
        }

        [Test]
        public void ShouldBeSameAs_WhenComparingBoxedValueType_WillThrow()
        {
            const int first = 1;

            Should.Error(
                () => first.ShouldBeSameAs(first),
                "() => first should be same as 1 but was 1"
            );

            first.ShouldNotBeSameAs(first);
        }

        [Test]
        public void ShouldBeCloseTo_ShowsYouWhereTheStringsDiffer()
        {
            const string testMessage = "muhst eat braiiinnzzzz";
            Should.Error(() =>
            testMessage
                .ShouldBeCloseTo("must eat brains"),
@"testMessage
    should be close to
'must eat brains'
    but was
'muhst eat braiiinnzzzz'");
        }

        [Test]
        public void ShouldBe_GivenCaseSensitiveOptionAndStringsDifferingOnlyInCase_ShouldFail()
        {
            Should.Error(() =>
                         "SamplE".ShouldBe("sAMPLe", Case.Sensitive),
                         "'SamplE' should be 'sAMPLe' but was 'SamplE'");
        }

        [Test]
        public void ShouldBe_EnumerableValues_ShouldCompareItemsInEachEnumerable()
        {
            new[] { 1, 2 }.ShouldBe(new[] { 1, 2 });

            Should.Error(() =>
                         new[] { 2, 1 }.ShouldBe(new[] { 1, 2 }),
                         "new[] { 2, 1 } should be [1, 2] but was [2, 1] difference [*2*, *1*]"
                );
        }

        [Test]
        public void ShouldBe_EnumerableTypesOfDifferentRuntimeTypes_ShouldShowDifferences()
        {
            var a = new Widget { Name = "Joe", Enabled = true };
            var b = new Widget { Name = "Joeyjojoshabadoo Jr", Enabled = true };

            IEnumerable<Widget> aEnumerable = a.ToEnumerable();
            IEnumerable<Widget> bEnumerable = new[] { b };

            Should.Error(() =>
                aEnumerable.ShouldBe(bEnumerable),
                "aEnumerable should be [Name(Joeyjojoshabadoo Jr) Enabled(True)] but was [Name(Joe) Enabled(True)] difference [*Name(Joe) Enabled(True)*]"
            );
        }

        [Test]
        public void ShouldBe_ShouldNotThrowWhenCalledOnANullEnumerableReference()
        {
            IEnumerable<int> something = null;
            Should.Error(
                () => something.ShouldBe(new[] { 1, 2, 3 }),
                "() => something should be [1, 2, 3] but was null");
        }

        [Test]
        public void ShouldBe_Action()
        {
            Action a = () => 1.ShouldBe(2);

            Should.Error(a,
                "Action a = () => 1 should be 2 but was 1");
        }

        [Test]
        public void ShouldBe_Expression()
        {
            Expression<Action> lambda = () => 1.ShouldBe(2);

            Should.Error(lambda.Compile(),
            "The provided expression should be 2 but was 1");
        }

        [Test]
        public void ShouldNotThrow_IfCallThrows_ShouldShowException()
        {
            Should.Error(
                () => Shouldly.Should.NotThrow(() => { throw new IndexOutOfRangeException(); }),
                "() => Shouldly.Should not throw System.IndexOutOfRangeException but does"
                );
        }

        [Test]
        public void ShouldThrow_WhenItThrowsIncorrectException()
        {
            Action shouldThrowAction =
            () => Shouldly.Should.Throw<NotImplementedException>(() =>
            {
                throw new RankException();
            });

            Should.Error(shouldThrowAction, "() => Shouldly.Should throw System.NotImplementedException but was System.RankException");
        }
        
        [Test]
        public void ShouldThrow_WhenItDoesntThrow()
        {
            Action shouldThrowAction =
            () => Shouldly.Should.Throw<NotImplementedException>(() =>
            {
            });

            Should.Error(shouldThrowAction, "() => Shouldly.Should throw System.NotImplementedException but does not");
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