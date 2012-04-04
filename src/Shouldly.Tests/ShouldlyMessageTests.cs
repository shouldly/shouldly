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
              string.Format("() => longString should contain \"zzzz\" but was \"{0}\"",longString.Substring(0,100))
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

        private class UncomparableClass {
            private readonly string _description;

            public UncomparableClass(string description) {
                _description = description;
            }

            public override string ToString() {
                return _description;
            }
        }
    }
}