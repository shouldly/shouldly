using NUnit.Framework;

namespace Shouldly.Tests
{
    [TestFixture]
    [ShouldlyMethods]
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
        public void ShouldlyMessage_PassedObjectsWhichCannotCompared_ShouldNotShowDifferences() 
        {
            Should.Error(
                () => new UncomparableClass("ted").ShouldBe(new UncomparableClass("bob")),
                "() => new UncomparableClass(\"ted\") should be bob but was ted"
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