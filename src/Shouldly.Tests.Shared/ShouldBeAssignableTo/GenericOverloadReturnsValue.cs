using Xunit;

namespace Shouldly.Tests.ShouldBeAssignableTo
{
    public class GenericOverloadReturnsValue
    {
        [Fact]
        public void ValueShouldBeReturnedWhenSuccessful()
        {
            object val = "Foo";
            // ReSharper disable once SuggestUseVarKeywordEvident
            string returnedVal = val.ShouldBeAssignableTo<string>("Some additional context");
            returnedVal.ShouldBe("Foo");
        }
    }
}