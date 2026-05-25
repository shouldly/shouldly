namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderFalseIEnumerableMethodYieldReturn
{
    [Fact]
    public void IgnoreOrderFalseIEnumerableMethodYieldReturnShouldFail()
    {
        Verify.ShouldFail(() =>
            GetEnumerable().ShouldBe([1, 2], false, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        GetEnumerable().ShouldBe([1]);
    }

    private static IEnumerable<int> GetEnumerable()
    {
        yield return 1;
    }
}