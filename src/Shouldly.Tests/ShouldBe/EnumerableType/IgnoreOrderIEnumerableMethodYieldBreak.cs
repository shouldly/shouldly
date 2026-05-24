namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderIEnumerableMethodYieldBreak
{
    [Fact]
    public void IgnoreOrderIEnumerableMethodYieldBreakShouldFail()
    {
        Verify.ShouldFail(() =>
            GetEmptyEnumerable().ShouldBe([2, 4], "Some additional context", ignoreOrder: true));
    }

    [Fact]
    public void ShouldPass()
    {
        GetEmptyEnumerable().ShouldBe(new int[0], ignoreOrder: true);
    }

    private static IEnumerable<int> GetEmptyEnumerable()
    {
        yield break;
    }
}