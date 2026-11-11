namespace Shouldly.Tests.ShouldBe.EnumerableType;

/// <summary>
/// A sequence that can be read only once must still produce a failure message when compared with a comparer.
/// </summary>
public class SinglePassSequenceWithComparerScenario
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ShouldFailWithAMessage(bool ignoreOrder)
    {
        var actual = new SinglePass<string>("a", "b");
        var expected = new SinglePass<string>("a", "c");

        var exception = Should.Throw<ShouldAssertException>(() =>
            actual.ShouldBe(expected, StringComparer.OrdinalIgnoreCase, ignoreOrder));

        exception.Message.ShouldContain("\"b\"");
        exception.Message.ShouldContain("\"c\"");
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void ShouldPass(bool ignoreOrder)
    {
        new SinglePass<string>("a", "B").ShouldBe(new SinglePass<string>("A", "b"), StringComparer.OrdinalIgnoreCase, ignoreOrder);
    }
}
