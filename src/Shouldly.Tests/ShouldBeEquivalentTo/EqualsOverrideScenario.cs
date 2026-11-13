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

    // A type with no public members has nothing to walk. When it defines its own equality, that is what it is
    // compared by, rather than failing the vacuous-comparison guard.
    [Fact]
    public void ShouldCompareATypeWithNoPublicMembersByItsOwnEquals()
    {
        new Opaque("a").ShouldBeEquivalentTo(new Opaque("a"));
        new { Value = new Opaque("a") }.ShouldBeEquivalentTo(new { Value = new Opaque("a") });
        new OpaqueHolder { Value = new Opaque("a") }.ShouldBeEquivalentTo(new OpaqueHolder { Value = new Opaque("a") });
        new[] { new Opaque("a") }.ShouldBeEquivalentTo(new[] { new Opaque("a") });
        new DerivedOpaque("a").ShouldBeEquivalentTo(new DerivedOpaque("a"));

        Should.Throw<ShouldAssertException>(() => new Opaque("a").ShouldBeEquivalentTo(new Opaque("b")));
        Should.Throw<ShouldAssertException>(() => new[] { new Opaque("a") }.ShouldBeEquivalentTo(new[] { new Opaque("b") }));
    }

    [Fact]
    public void ShouldCompareAStructWithNoPublicMembersByItsIEquatable()
    {
        new OpaqueInstant(1).ShouldBeEquivalentTo(new OpaqueInstant(1));
        new { When = new OpaqueInstant(1) }.ShouldBeEquivalentTo(new { When = new OpaqueInstant(1) });
        new { When = (OpaqueInstant?)new OpaqueInstant(1) }.ShouldBeEquivalentTo(new { When = (OpaqueInstant?)new OpaqueInstant(1) });

        Should.Throw<ShouldAssertException>(() => new OpaqueInstant(1).ShouldBeEquivalentTo(new OpaqueInstant(2)));
    }

    [Fact]
    public void ShouldFailWhenAMemberWithNoPublicMembersDiffersByItsOwnEquals()
    {
        var subject = new OpaqueHolder { Value = new Opaque("a") };
        var expected = new OpaqueHolder { Value = new Opaque("b") };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    // Equals is used only when there is nothing else to compare: a type with public members is still compared by
    // them, and one with no members and no equality of its own still trips the guard.
    [Fact]
    public void ShouldStillFailTheGuardForATypeWithNoPublicMembersAndNoEqualityOfItsOwn()
    {
        Should.Throw<ShouldAssertException>(() => new PrivateStateStruct(1).ShouldBeEquivalentTo(new PrivateStateStruct(1)))
            .Message.ShouldContain("Found no public members to compare on");
        Should.Throw<ShouldAssertException>(() => new KeyEquatable { Key = "k", Data = "one" }.ShouldBeEquivalentTo(new KeyEquatable { Key = "k", Data = "two" }))
            .Message.ShouldContain("Data");
    }

    [Fact]
    public void ShouldFailWhenTheActualValueIsADifferentTypeFromAnExpectationWithNoPublicMembers()
    {
        Should.Throw<ShouldAssertException>(() => new { Value = "a" }.ShouldBeEquivalentTo(new Opaque("a")));
    }
}

public class Opaque(string value)
{
    private readonly string _value = value;

    public override bool Equals(object? obj) => obj is Opaque other && other._value == _value;

    public override int GetHashCode() => _value.GetHashCode();

    public override string ToString() => $"Opaque({_value})";
}

public class DerivedOpaque(string value) : Opaque(value);

public class OpaqueHolder
{
    public Opaque? Value { get; set; }
}

public readonly struct OpaqueInstant(long ticks) : IEquatable<OpaqueInstant>
{
    private readonly long _ticks = ticks;

    public bool Equals(OpaqueInstant other) => _ticks == other._ticks;

    public override string ToString() => $"Instant({_ticks})";
}

public readonly struct PrivateStateStruct(int value)
{
    private readonly int _value = value;

    public override string ToString() => $"PrivateState({_value})";
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
