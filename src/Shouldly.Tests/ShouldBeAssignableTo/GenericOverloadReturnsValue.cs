namespace Shouldly.Tests.ShouldBeAssignableTo;

public class GenericOverloadReturnsValue
{
    [Fact]
    public void ValueShouldBeReturnedWhenSuccessful()
    {
        object val = "Foo";
        // ReSharper disable once SuggestUseVarKeywordEvident
        var returnedVal = val.ShouldBeAssignableTo<string>("Some additional context");
        returnedVal.ShouldBe("Foo");
    }
}