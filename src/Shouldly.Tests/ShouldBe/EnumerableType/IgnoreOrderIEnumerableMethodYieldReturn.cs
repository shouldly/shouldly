namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderIEnumerableMethodYieldReturn
{
    [Fact]
    public void IgnoreOrderIEnumerableMethodYieldReturnShouldFail()
    {
        Verify.ShouldFail(() =>
            GetEnumerable().ShouldBe([1, 2], "Some additional context", ignoreOrder: true));
    }

    [Fact]
    public void ShouldPass()
    {
        GetEnumerable().ShouldBe([1], ignoreOrder: true);
    }

    private static IEnumerable<int> GetEnumerable()
    {
        yield return 1;
    }
}