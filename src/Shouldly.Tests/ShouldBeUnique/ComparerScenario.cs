namespace Shouldly.Tests.ShouldBe.ShouldBeUnique;

public class ComparerScenario
{
    [Fact]
    public void ComparerEqualsShouldPass()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Elephant", IgnoredProperty = "Duck" },
            new ComparableClass { Property = "Lion", IgnoredProperty = "Whale" }
        };

        comparison1.ShouldBeUnique(new ComparableClassComparer());
    }

    [Fact]
    public void ComparerNotEqualsShouldFail()
    {
        var comparison1 = new[]
        {
            new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Whale" },
            new ComparableClass { Property = "Kangaroo", IgnoredProperty = "Salmon" }
        };

        Verify.ShouldFail(() =>
            comparison1.ShouldBeUnique(new ComparableClassComparer(), "Some additional context"));
    }

    [Fact]
    public void AComparerIsUsedThroughItsHashCode()
    {
        // Documented: a large collection can be matched quickly by passing a comparer, which is hashed.
        var comparer = new HashCountingComparer();

        new[] { "a", "b" }.ShouldBeUnique(comparer);

        comparer.HashCodes.ShouldBeGreaterThan(0);
    }

    private sealed class HashCountingComparer : IEqualityComparer<string>
    {
        public int HashCodes { get; private set; }

        public bool Equals(string? x, string? y) => x == y;

        public int GetHashCode(string obj)
        {
            HashCodes++;
            return obj.GetHashCode();
        }
    }
}
