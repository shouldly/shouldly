namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderIEnumerableMethodYieldReturn
{
    [Fact]
    public void IgnoreOrderIEnumerableMethodYieldReturnShouldFail()
    {
        Verify.ShouldFail(() =>
            GetEnumerable().ShouldBe([1, 2], true, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        GetEnumerable().ShouldBe([1], true);
    }

    private static IEnumerable<int> GetEnumerable()
    {
        yield return 1;
    }
}