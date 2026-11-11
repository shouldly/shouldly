namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// A type that defines its own equality is compared with it, even when it also implements <see cref="IComparable{T}"/>
/// or <see cref="IComparable"/> with a different idea of equality, so the answer doesn't depend on the static type the
/// assertion is made at. <c>CompareTo</c> decides only for a type that doesn't define equality. See issue #1428.
/// </summary>
public class ComparableWithOwnEqualsScenarios
{
    // Sorted by name, equal by name and age.
    private static readonly Person Bob30 = new("Bob", 30);
    private static readonly Person Bob40 = new("Bob", 40);

    [Fact]
    public void OverriddenEqualsDecidesOverGenericCompareTo()
    {
        Bob30.ShouldNotBe(Bob40);
        Should.Throw<ShouldAssertException>(() => Bob30.ShouldBe(Bob40));
    }

    [Fact]
    public void OverriddenEqualsDecidesOverNonGenericCompareTo()
    {
        new NonGenericPerson("Bob", 30).ShouldNotBe(new NonGenericPerson("Bob", 40));
        new NonGenericPerson("Bob", 30).ShouldBe(new NonGenericPerson("Bob", 30));
    }

    [Fact]
    public void StaticTypeDoesNotChangeTheAnswer()
    {
        ((object)Bob30).ShouldNotBe(Bob40);
        ((IComparable<Person>)Bob30).ShouldNotBe(Bob40);
    }

    [Fact]
    public void CollectionsCompareElementsWithTheirOwnEquals()
    {
        new[] { Bob30 }.ShouldNotBe([Bob40]);
        new List<Person> { Bob30 }.ShouldNotBe([Bob40]);
        Should.Throw<ShouldAssertException>(() => new[] { Bob30 }.ShouldBe([Bob40], ignoreOrder: true));
        new Dictionary<string, Person> { ["a"] = Bob30 }.ShouldNotBe(new Dictionary<string, Person> { ["a"] = Bob40 });
    }

    [Fact]
    public void MembershipAssertionsCompareWithTheirOwnEquals()
    {
        new[] { Bob30 }.ShouldNotContain(Bob40);
        new Dictionary<string, Person> { ["a"] = Bob30 }.ShouldNotContainValueForKey("a", Bob40);
    }

    [Fact]
    public void OverriddenEqualsSaysEqualWhenCompareToDoesNot()
    {
        new SortedByVersion("a", 1).ShouldBe(new SortedByVersion("a", 2));
        ((object)new SortedByVersion("a", 1)).ShouldBe(new SortedByVersion("a", 2));
        new[] { new SortedByVersion("a", 1) }.ShouldBe([new SortedByVersion("a", 2)]);
    }

    [Fact]
    public void CompareToStillDecidesForATypeThatDoesNotDefineEquality()
    {
        new OnlyComparable(1).ShouldBe(new OnlyComparable(1));
        new OnlyComparable(1).ShouldNotBe(new OnlyComparable(2));
        new[] { new OnlyComparable(1) }.ShouldBe([new OnlyComparable(1)]);
    }

    [Fact]
    public void CompareToStillDecidesForAStructThatDoesNotOverrideEquals()
    {
        // ValueType.Equals compares every field; the struct itself only says how to sort.
        new SortedById(1, "x").ShouldBe(new SortedById(1, "y"));
    }

    [Fact]
    public void RuntimeEquatableDecidesOverNonGenericCompareTo()
    {
        // ShouldBe<object> doesn't know the type, and finds its IEquatable<T> before its IComparable, as
        // ShouldBe<EquatableAndComparable> does.
        new EquatableAndComparable().ShouldNotBe(new EquatableAndComparable());
        ((object)new EquatableAndComparable()).ShouldNotBe(new EquatableAndComparable());
        new object[] { new EquatableAndComparable() }.ShouldNotBe([new EquatableAndComparable()]);
    }

    private sealed class Person(string name, int age) : IComparable<Person>
    {
        public string Name => name;

        public int Age => age;

        public int CompareTo(Person? other) => string.Compare(Name, other?.Name, StringComparison.Ordinal);

        public override bool Equals(object? obj) => obj is Person p && p.Name == Name && p.Age == Age;

        public override int GetHashCode() => HashCode.Combine(Name, Age);

        public override string ToString() => $"{Name} ({Age})";
    }

    private sealed class NonGenericPerson(string name, int age) : IComparable
    {
        public string Name => name;

        public int Age => age;

        public int CompareTo(object? obj) => string.Compare(Name, (obj as NonGenericPerson)?.Name, StringComparison.Ordinal);

        public override bool Equals(object? obj) => obj is NonGenericPerson p && p.Name == Name && p.Age == Age;

        public override int GetHashCode() => HashCode.Combine(Name, Age);
    }

    private sealed class SortedByVersion(string id, int version) : IComparable<SortedByVersion>
    {
        public string Id => id;

        public int Version => version;

        public int CompareTo(SortedByVersion? other) => other is null ? 1 : Version.CompareTo(other.Version);

        public override bool Equals(object? obj) => obj is SortedByVersion other && other.Id == Id;

        public override int GetHashCode() => Id.GetHashCode();
    }

    private sealed class OnlyComparable(int value) : IComparable<OnlyComparable>
    {
        public int Value => value;

        public int CompareTo(OnlyComparable? other) => other is null ? 1 : Value.CompareTo(other.Value);
    }

    private readonly struct SortedById(int id, string label) : IComparable<SortedById>
    {
        public int Id => id;

        public string Label => label;

        public int CompareTo(SortedById other) => Id.CompareTo(other.Id);
    }

#pragma warning disable CA1067 // Deliberately disagrees with object.Equals to show which interface is consulted.
    private sealed class EquatableAndComparable : IEquatable<EquatableAndComparable>, IComparable
    {
        public bool Equals(EquatableAndComparable? other) => false;

        public int CompareTo(object? obj) => 0;
    }
#pragma warning restore CA1067
}
