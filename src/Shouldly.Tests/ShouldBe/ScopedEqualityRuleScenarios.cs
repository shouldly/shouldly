using System.Collections.Immutable;
using Shouldly.Internals;

namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// Equality rules for types too common to register for the whole test run (<see cref="string"/>, <see cref="char"/>),
/// or that need a rule which would throw if handed the wrong value. Each test registers its rule only while it runs,
/// alone, in <see cref="GlobalConfigCollection"/>.
/// </summary>
[Collection(GlobalConfigCollection.Name)]
public class ScopedEqualityRuleScenarios
{
    [Fact]
    public void StringShouldBeHonoursARuleWithoutOptions()
    {
        using var _ = EqualityRules.RestoreAfterwards();
        ShouldlyConfiguration.UseEquality<string>(StringComparer.OrdinalIgnoreCase);

        "a".ShouldBe("A");
        "a".ShouldNotBe("b");
        Should.Throw<ShouldAssertException>(() => "a".ShouldBe("b"));
        Should.Throw<ShouldAssertException>(() => "a".ShouldNotBe("A"));
    }

    [Fact]
    public void StringShouldBeOptionsTakePrecedenceOverARule()
    {
        using var _ = EqualityRules.RestoreAfterwards();
        ShouldlyConfiguration.UseEquality<string>(StringComparer.OrdinalIgnoreCase);

        Should.Throw<ShouldAssertException>(() => "a".ShouldBe("A", StringCompareShould.IgnoreLineEndings));
    }

    [Fact]
    public void PrimitiveMemoryHonoursARule()
    {
        using var _ = EqualityRules.RestoreAfterwards();
        ShouldlyConfiguration.UseEquality<char>((expected, actual) =>
            char.ToUpperInvariant(expected) == char.ToUpperInvariant(actual));

        new Memory<char>("ab".ToCharArray()).ShouldBe(new Memory<char>("AB".ToCharArray()));
        new ReadOnlyMemory<char>("ab".ToCharArray()).ShouldBe(new ReadOnlyMemory<char>("AB".ToCharArray()));
        Should.Throw<ShouldAssertException>(() =>
            new Memory<char>("ab".ToCharArray()).ShouldNotBe(new Memory<char>("AB".ToCharArray())));
    }

    [Fact]
    public void MemoryShouldBeHonoursARuleForTheMemoryItself()
    {
        using var _ = EqualityRules.RestoreAfterwards();
        ShouldlyConfiguration.UseEquality<Memory<int>>((expected, actual) => expected.Length == actual.Length);
        ShouldlyConfiguration.UseEquality<ReadOnlyMemory<int>>((expected, actual) => expected.Length == actual.Length);

        new Memory<int>([1]).ShouldBe(new Memory<int>([2]));
        new ReadOnlyMemory<int>([1]).ShouldBe(new ReadOnlyMemory<int>([2]));
        new List<Memory<int>> { new([1]) }.ShouldBe([new Memory<int>([2])]);
        Should.Throw<ShouldAssertException>(() => new Memory<int>([1]).ShouldNotBe(new Memory<int>([2])));
        Should.Throw<ShouldAssertException>(() => new ReadOnlyMemory<int>([1]).ShouldNotBe(new ReadOnlyMemory<int>([2])));
        Should.Throw<ShouldAssertException>(() => new Memory<int>([1]).ShouldBe(new Memory<int>([1, 2])));
    }

    [Fact]
    public void KeyValuePairNeverHandsARuleANullEquivalentValue()
    {
        using var _ = EqualityRules.RestoreAfterwards();
        // Reading Length throws on a default ImmutableArray<T>, so this rule fails if it is called with one.
        ShouldlyConfiguration.UseEquality<ImmutableArray<int>>((expected, actual) => expected.Length == actual.Length);

        var withDefault = new Dictionary<string, ImmutableArray<int>> { ["k"] = default };

        withDefault.ShouldBe(new Dictionary<string, ImmutableArray<int>> { ["k"] = default });
        Should.Throw<ShouldAssertException>(() =>
            withDefault.ShouldBe(new Dictionary<string, ImmutableArray<int>> { ["k"] = [1] }));
    }
}
