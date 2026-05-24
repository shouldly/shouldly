namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderFalseIEnumerableMethodYieldBreak
{
    [Fact]
    public void IgnoreOrderFalseIEnumerableMethodYieldBreakShouldFail()
    {
        Verify.ShouldFail(() =>
            GetEmptyEnumerable().ShouldBe([2, 4], "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        GetEmptyEnumerable().ShouldBe(new int[0]);
    }

    private static IEnumerable<int> GetEmptyEnumerable()
    {
        yield break;
    }
}