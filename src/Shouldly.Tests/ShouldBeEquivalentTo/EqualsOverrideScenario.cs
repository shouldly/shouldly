namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class EqualsOverrideScenario
{
    [Fact]
    public void ShouldFailWhenAnEqualsEqualPairMasksALaterStructurallyDifferentPair()
    {
        // First and Second are Equals-equal (same Key) but structurally different. Visited-pair
        // tracking must use reference identity: keyed on the members' own Equals/GetHashCode,
        // comparing First records the pair in a way that also "covers" Second, and the differing
        // Data on Second is silently never compared — a false pass.
        var subject = new KeyEquatableHolder
        {
            First = new KeyEquatable { Key = "k", Data = "one" },
            Second = new KeyEquatable { Key = "k", Data = "two" }
        };

        var expected = new KeyEquatableHolder
        {
            First = new KeyEquatable { Key = "k", Data = "one" },
            Second = new KeyEquatable { Key = "k", Data = "different" }
        };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    [Fact]
    public void ShouldPassWhenEqualsEqualPairsAreAlsoStructurallyEqual()
    {
        var subject = new KeyEquatableHolder
        {
            First = new KeyEquatable { Key = "k", Data = "one" },
            Second = new KeyEquatable { Key = "k", Data = "two" }
        };

        var expected = new KeyEquatableHolder
        {
            First = new KeyEquatable { Key = "k", Data = "one" },
            Second = new KeyEquatable { Key = "k", Data = "two" }
        };

        subject.ShouldBeEquivalentTo(expected);
    }
}

public class KeyEquatableHolder
{
    public KeyEquatable? First { get; set; }
    public KeyEquatable? Second { get; set; }
}

/// <summary>Equality is by <see cref="Key"/> only; <see cref="Data"/> is ignored by Equals.</summary>
public class KeyEquatable
{
    public string? Key { get; set; }
    public string? Data { get; set; }

    public override bool Equals(object? obj) =>
        obj is KeyEquatable other && string.Equals(Key, other.Key, StringComparison.Ordinal);

    public override int GetHashCode() =>
        Key?.GetHashCode() ?? 0;
}
