namespace Shouldly.Tests.ShouldBeSupersetOf;

public class ComparerScenario
{
    [Fact]
    public void ShouldPassWithComparer()
    {
        new[] { "A", "b", "C" }.ShouldBeSupersetOf(["a", "B"], StringComparer.OrdinalIgnoreCase);
    }

    [Fact]
    public void ComparerPartialMatchShouldFail()
    {
        var comparison1 = new[] { "A" };
        var comparison2 = new[] { "a", "B" };

        Verify.ShouldFail(() =>
            comparison1.ShouldBeSupersetOf(comparison2, StringComparer.OrdinalIgnoreCase, "Some additional context"));
    }

    [Fact]
    public void ComparerPartialMatchWithoutSourceShouldFail()
    {
        var comparison1 = new[] { "A" };
        var comparison2 = new[] { "a", "B" };

        // Without a call-site expression the message leads with the actual value, which must be the whole collection
        using (ShouldlyConfiguration.DisableSourceInErrors())
        {
            Verify.ShouldFail(() =>
                comparison1.ShouldBeSupersetOf(comparison2, StringComparer.OrdinalIgnoreCase, actualExpression: null));
        }
    }

    [Fact]
    public void ComparerNotEqualsShouldFail()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Whale" }
        };
        var comparison2 = new[]
        {
            new ComparableClass { Property = "Snake", IgnoredProperty = "Platypus" },
            new ComparableClass { Property = "Cat", IgnoredProperty = "Ant" }
        };

        Verify.ShouldFail(() =>
            comparison1.ShouldBeSupersetOf(comparison2, new ComparableClassComparer(), "Some additional context"));
    }
}
