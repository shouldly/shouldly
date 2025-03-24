namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderIEnumerableMethodYieldBreak
{
    [Fact]
    public void IgnoreOrderIEnumerableMethodYieldBreakShouldFail()
    {
        Verify.ShouldFail(() =>
                GetEmptyEnumerable().ShouldBe([2, 4], true, "Some additional context"),

            errorWithSource:
            """
            GetEmptyEnumerable()
                should be (ignoring order)
            [2, 4]
                but
            GetEmptyEnumerable()
                is missing
            [2, 4]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            []
                should be (ignoring order)
            [2, 4]
                but
            []
                is missing
            [2, 4]

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        GetEmptyEnumerable().ShouldBe(new int[0], true);
    }

    private static IEnumerable<int> GetEmptyEnumerable()
    {
        yield break;
    }
}