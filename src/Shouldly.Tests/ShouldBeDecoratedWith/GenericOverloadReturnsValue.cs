using Xunit;

namespace Shouldly.Tests.ShouldBeDecoratedWith
{
    public class GenericOverloadReturnsValue
   
    {

    [Fact]
        public void ValueShouldBeReturnedWhenSuccessful()
        {
            object val = "Foo";
            // ReSharper disable once SuggestUseVarKeywordEvident
            string returnedVal = val.ShouldBeOfType<string>("Some additional context");
            returnedVal.ShouldBe("Foo");
        }
    }
}