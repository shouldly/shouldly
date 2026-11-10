namespace Shouldly.Tests.ShouldBe.EnumerableType;

// Every enumerable echoed in a failure message is capped at
// ShouldlyConfiguration.MaxEnumerableItemsInMessages items, finite or not.
public class LongEnumerableScenario
{
    [Fact]
    public void LongArrayShouldFail()
    {
        var actual = Enumerable.Range(0, 10).ToArray();
        var expected = new[] { 0, 99 }.Concat(Enumerable.Range(2, 7)).ToArray();

        using (MaxEnumerableItemsScope.Of(3))
        {
            Verify.ShouldFail(() => actual.ShouldBe(expected));
        }
    }

    [Fact]
    public void LongExpectedIgnoringOrderShouldFail()
    {
        // Only the expected side is cut short, so the items missing from the actual value are
        // still known, though more may follow; nothing can be said about the expected side.
        var expected = Enumerable.Range(0, 10).ToArray();

        using (MaxEnumerableItemsScope.Of(3))
        {
            Verify.ShouldFail(() => new[] { 1, 0 }.ShouldBe(expected, ignoreOrder: true));
        }
    }

    [Fact]
    public void BothLongIgnoringOrderShouldFail()
    {
        // Both sides are cut short, so neither side's missing items can be known.
        var actual = Enumerable.Range(0, 10).ToArray();
        var expected = Enumerable.Range(1, 10).ToArray();

        using (MaxEnumerableItemsScope.Of(3))
        {
            Verify.ShouldFail(() => actual.ShouldBe(expected, ignoreOrder: true));
        }
    }

    [Fact]
    public void BothLongIgnoringOrderWithCaseShouldFail()
    {
        // The fallback for two cut-short sides still says how strings were compared.
        string[] actual = ["A", "x"];

        using (MaxEnumerableItemsScope.Of(1))
        {
            Verify.ShouldFail(() => actual.ShouldBe(["a", "y"], Case.Insensitive, ignoreOrder: true));
        }
    }

    [Fact]
    public void EnumerableAtTheLimitIsNotCapped()
    {
        using (MaxEnumerableItemsScope.Of(3))
        {
            var message = Should.Throw<ShouldAssertException>(() => new[] { 0, 1, 2 }.ShouldBe([0, 1, 3])).Message;

            message.ShouldNotContain("...");
        }
    }

    [Fact]
    public void TheLimitMustBePositive()
    {
        Should.Throw<ArgumentOutOfRangeException>(() => ShouldlyConfiguration.MaxEnumerableItemsInMessages = 0);
    }
}
