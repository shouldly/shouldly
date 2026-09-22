namespace Shouldly.Tests.ShouldBeSupersetOf;

public class SuccessScenarios
{
    [Fact]
    public void ArrayIsSupersetOfSelf()
    {
        var arr = new[] { 1 };

        arr.ShouldBeSupersetOf(arr, "Some additional context");
    }

    [Fact]
    public void AnythingIsSupersetOfEmptyArray()
    {
        new[] { 1, 2, 3, 4 }.ShouldBeSupersetOf(new int[0], "Some additional context");
    }

    [Fact]
    public void DuplicatesInExpectedAreIgnored()
    {
        new[] { 1, 2 }.ShouldBeSupersetOf([1, 1, 2], "Some additional context");
    }
}