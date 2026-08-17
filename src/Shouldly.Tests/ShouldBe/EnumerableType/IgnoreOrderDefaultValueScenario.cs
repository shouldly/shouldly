namespace Shouldly.Tests.ShouldBe.EnumerableType;

/// <summary>
/// Regression coverage for the order-insensitive comparison matching an unmatched element against <c>default(T)</c>.
/// </summary>
public class IgnoreOrderDefaultValueScenario
{
    [Fact]
    public void ShouldFailWhenAnUnmatchedElementCollidesWithDefaultInt()
    {
        int[] actual = [5, 5];
        int[] expected = [5, 0];

        Should.Throw<ShouldAssertException>(() => actual.ShouldBe(expected, ignoreOrder: true));
    }

    [Fact]
    public void ShouldFailForSingleElementMismatchWhenExpectedIsDefaultInt()
    {
        int[] actual = [1];
        int[] expected = [0];

        Should.Throw<ShouldAssertException>(() => actual.ShouldBe(expected, ignoreOrder: true));
    }

    [Fact]
    public void ShouldFailWhenAnUnmatchedElementCollidesWithNull()
    {
        string?[] actual = ["a"];
        string?[] expected = [null];

        Should.Throw<ShouldAssertException>(() => actual.ShouldBe(expected, ignoreOrder: true));
    }

    [Fact]
    public void ShouldFailWhenBothContainDefaultButRemainingElementsDiffer()
    {
        int[] actual = [0, 0];
        int[] expected = [0, 5];

        Should.Throw<ShouldAssertException>(() => actual.ShouldBe(expected, ignoreOrder: true));
    }

    [Fact]
    public void ShouldPassWhenMultisetsAreEqualIncludingDefault()
    {
        int[] actual = [5, 0];
        int[] expected = [0, 5];

        actual.ShouldBe(expected, ignoreOrder: true);
    }

    [Fact]
    public void ShouldPassWhenDefaultAppearsMultipleTimesInEqualMultisets()
    {
        int[] actual = [0, 0, 7];
        int[] expected = [7, 0, 0];

        actual.ShouldBe(expected, ignoreOrder: true);
    }

    [Fact]
    public void ShouldPassWhenNullsMatchAcrossEqualMultisets()
    {
        string?[] actual = ["a", null, "b"];
        string?[] expected = [null, "b", "a"];

        actual.ShouldBe(expected, ignoreOrder: true);
    }
}
