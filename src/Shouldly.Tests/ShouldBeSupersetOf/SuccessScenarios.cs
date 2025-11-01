namespace Shouldly.Tests.ShouldBeSupersetOf;

public class SuccessScenarios
{
    [Fact]
    public void ArrayIsSubsetOfSelf()
    {
        var arr = new[] { 1 };

        arr.ShouldBeSupersetOf(arr, "Some additional context");
    }
}