using NUnit.Framework;
using Shouldly;

namespace Tests
{
    [TestFixture]
    [ShouldlyMethods]
    public class ShouldlyMessageTests
    {
        [Test]
        public void CanGenerateErrorMessage()
        {
            new ShouldlyMessage("expected", "actual").ToString()
                .ShouldBe(@"            new ShouldlyMessage(""expected"", ""actual"").ToString()
        can generate error message
    ""expected""
        but was
    ""actual""");
        }

        [Test]
        public void ShouldlyMessage_PassedCollectionsWhichCanBeCompared_ShouldShowDifferences() 
        {
            new ShouldlyMessage(new int[] { 1, 2, 3 }, new int[] { 2, 2, 3 }).ToString()
            .ShouldBe(@"            new ShouldlyMessage(new int[] { 1, 2, 3 }, new int[] { 2, 2, 3 }).ToString()
        shouldly message_ passed collections which can be compared_ should show differences
    [1, 2, 3]
        but was
    [2, 2, 3]
        difference
    [*2*, 2, 3]");
        }

        [Test]
        public void ShouldlyMessage_PassedObjectsWhichCannotCompared_ShouldNotShowDifferences() 
        {
            new ShouldlyMessage(new UncomparableClass("ted"), new UncomparableClass("bob")).ToString()
            .ShouldBe(@"            new ShouldlyMessage(new UncomparableClass(""ted""), new UncomparableClass(""bob"")).ToString()
        shouldly message_ passed objects which cannot compared_ should not show differences
    ted
        but was
    bob");
        }

        private class UncomparableClass {
            private string _description;

            public UncomparableClass(string description) {
                _description = description;
            }

            public override string ToString() {
                return _description;
            }
        }
    }
}