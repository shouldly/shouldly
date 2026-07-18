namespace Shouldly.Tests.ShouldBeSubsetOf;

public class ObjectArrayOfIntScenario
{
    [Fact]
    public void ObjectArrayOfIntScenarioShouldFail()
    {
        var arr = new object[] { 1, 2, 3 };
        var arr2 = new object[] { 1, 2 };

        Verify.ShouldFail(() =>
            arr.ShouldBeSubsetOf(arr2, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var arr = new object[] { 1, 2, 3 };
        var arr2 = new object[] { 1, 2, 3, 4 };

        arr.ShouldBeSubsetOf(arr2);
    }
}