using NUnit.Framework;

namespace Shouldly.Tests.ShouldBeAssignableTo
{
    public class GenericOverloadReturnsValue
    {
        [Test]
        public void ValueShouldBeReturnedWhenSuccessful()
        {
            object val = "Foo";
            // ReSharper disable once SuggestUseVarKeywordEvident
            string returnedVal = val.ShouldBeAssignableTo<string>("Some additional context");
            returnedVal.ShouldBe("Foo");
        }
    }
}