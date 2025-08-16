namespace Shouldly.Tests.ShouldBeSupersetOf;

public class SuccessScenarios
{
    [Fact]
    public void ArrayIsSupoersetOfSelf()
    {
        var arr = new[] { 1 };

        arr.ShouldBeSupersetOf(arr, "Some additional context");
    }

    [Fact]
    public void AnythingIsSupersetOfEmptyArray()
    {
        new[]{1, 2, 3, 4}.ShouldBeSupersetOf([], "Some additional context");
    }
}