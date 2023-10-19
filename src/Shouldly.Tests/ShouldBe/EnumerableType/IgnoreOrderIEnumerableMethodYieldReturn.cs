namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderIEnumerableMethodYieldReturn
{
    [Fact]
    public void IgnoreOrderIEnumerableMethodYieldReturnShouldFail()
    {
        Verify.ShouldFail(() =>
                GetEnumerable().ShouldBe(new[] { 1, 2 }, true, "Some additional context"),

            errorWithSource:
            """
            GetEnumerable()
                should be (ignoring order)
            [1, 2]
                but
            GetEnumerable()
                is missing
            [2]

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            [1]
                should be (ignoring order)
            [1, 2]
                but
            [1]
                is missing
            [2]

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        GetEnumerable().ShouldBe(new[] { 1 }, true);
    }

    private static IEnumerable<int> GetEnumerable()
    {
        yield return 1;
    }
}