namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class OwnEqualityNoteScenario
{
    // ShouldBeEquivalentTo compares members, not Equals. When the type's own equality would call two values equal
    // that differ member by member, the failure says so, and how to compare the type by its own equality.
    [Fact]
    public void ShouldSayWhenTheTypesOwnEqualityCallsTheValuesEqual()
    {
        var subject = new[] { new KeyEquatable { Key = "a", Data = "one" }, new KeyEquatable { Key = "b", Data = "one" } };
        var expected = new[] { new KeyEquatable { Key = "a", Data = "two" }, new KeyEquatable { Key = "b", Data = "two" } };

        // Two values of the type differ; the note names the type once.
        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    [Fact]
    public void ShouldSayNothingWhenTheTypesOwnEqualityAlsoCallsTheValuesUnequal()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            new KeyEquatable { Key = "a", Data = "one" }.ShouldBeEquivalentTo(new KeyEquatable { Key = "b", Data = "one" }));

        exception.Message.ShouldNotContain("own equality");
    }

    [Fact]
    public void ShouldSayNothingOfATypeWithNoEqualityOfItsOwn()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            new KeyEquatableHolder { First = new() { Key = "a", Data = "one" } }
                .ShouldBeEquivalentTo(new KeyEquatableHolder { First = new() { Key = "b", Data = "one" } }));

        exception.Message.ShouldNotContain("own equality");
    }

    [Fact]
    public void ShouldSayNothingOfAnAnonymousType()
    {
        // An anonymous type's Equals would call these equal, through KeyEquatable's; only KeyEquatable is named.
        var exception = Should.Throw<ShouldAssertException>(() =>
            new { Value = new KeyEquatable { Key = "a", Data = "one" } }
                .ShouldBeEquivalentTo(new { Value = new KeyEquatable { Key = "a", Data = "two" } }));

        exception.Message.ShouldContain("UseEquality<KeyEquatable>");
        exception.Message.ShouldNotContain("AnonymousType");
    }

    [Fact]
    public void ShouldSayNothingWhenTheTypesOwnEqualityThrows()
    {
        var exception = Should.Throw<ShouldAssertException>(() =>
            new ThrowingEquals { Name = "a" }.ShouldBeEquivalentTo(new ThrowingEquals { Name = "b" }));

        exception.Message.ShouldNotContain("own equality");
    }

    public class ThrowingEquals
    {
        public string? Name { get; set; }

        public override bool Equals(object? obj) => throw new InvalidOperationException("Equals isn't supported");

        public override int GetHashCode() => 0;
    }
}
