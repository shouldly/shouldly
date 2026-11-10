namespace Shouldly.Tests.ShouldBe.EnumerableType;

/// <summary>
/// Elements that implement <see cref="IEquatable{T}"/> without overriding <see cref="object.Equals(object)"/> are
/// compared with their typed <c>Equals</c> wherever Shouldly compares them, not only when the enumerable
/// <c>ShouldBe</c> overload knows the element type statically. See issue #825.
/// </summary>
public class EquatableElementScenario
{
    private static List<Equatable> List(params int[] values) => values.Select(v => new Equatable(v)).ToList();

    [Fact]
    public void ListsOfEqualElementsAreEqual()
    {
        List(1, 2, 3).ShouldBe(List(1, 2, 3));
        List(1, 2, 3).ToArray().ShouldBe(List(1, 2, 3).ToArray());
    }

    [Fact]
    public void ShouldNotBeFailsForListsOfEqualElements() =>
        Should.Throw<ShouldAssertException>(() => List(1, 2, 3).ShouldNotBe(List(1, 2, 3)));

    [Fact]
    public void ShouldNotBePassesForListsOfDifferentElements() =>
        List(1, 2, 3).ShouldNotBe(List(1, 2, 4));

    [Fact]
    public void NestedListsOfEqualElementsAreEqual() =>
        new List<List<Equatable>> { List(1), List(2, 3) }.ShouldBe([List(1), List(2, 3)]);

    [Fact]
    public void ObjectTypedListsOfEqualElementsAreEqual() =>
        ((object)List(1, 2)).ShouldBe(List(1, 2));

    [Fact]
    public void ObjectTypedElementsAreEqual() =>
        ((object)new Equatable(1)).ShouldBe(new Equatable(1));

    [Fact]
    public void IgnoringOrderListsOfEqualElementsAreEqual() =>
        List(1, 2, 3).ShouldBe(List(3, 1, 2), ignoreOrder: true);

    [Fact]
    public void EquatableElementScenarioShouldFail()
    {
        // Only the element that differs is highlighted.
        Verify.ShouldFail(() =>
            List(1, 2, 3).ShouldBe(List(1, 2, 4), "Some additional context"));
    }

    [Fact]
    public void EquatableElementIgnoringOrderScenarioShouldFail()
    {
        // Only the unmatched elements are reported as missing.
        Verify.ShouldFail(() =>
            List(1, 2, 3).ShouldBe(List(3, 1, 4), ignoreOrder: true, "Some additional context"));
    }

    [Fact]
    public void TypedEqualsTakesPrecedenceOverObjectEquals()
    {
        // The typed Equals replaces the object.Equals fallback, so when the two disagree the typed one wins.
        new List<object> { new DisagreeingEquatable() }.ShouldNotBe([new DisagreeingEquatable()]);
    }

    [Fact]
    public void ComparableElementsStillCompareWithCompareTo()
    {
        // IComparable is consulted before the typed Equals, as it was before #825.
        new List<object> { new ComparableEquatable() }.ShouldBe([new ComparableEquatable()]);
    }

    [Fact]
    public void MostDerivedEquatableWinsWhenInterfacesAreDeclaredBaseFirst() =>
        MostDerivedEquatableWins(() => new BaseFirstJudge());

    [Fact]
    public void MostDerivedEquatableWinsWhenInterfacesAreDeclaredDerivedFirst() =>
        MostDerivedEquatableWins(() => new DerivedFirstJudge());

    private static void MostDerivedEquatableWins(Func<object> judge)
    {
        // Each judge (the expected value) implements IEquatable<Animal> answering false and IEquatable<Dog>
        // answering true, so the result shows which one was chosen for each runtime type of the actual value.
        // The same judge type is paired with each argument type in turn, so the lookups are cached separately.
        ((object)new Animal()).ShouldNotBe(judge());
        ((object)new Dog()).ShouldBe(judge());
        ((object)new Puppy()).ShouldBe(judge());
        ((object)new Animal()).ShouldNotBe(judge());
    }

#pragma warning disable CA1067 // These types deliberately implement IEquatable<T> without overriding Equals(object)
    private sealed class Equatable(int value) : IEquatable<Equatable>
    {
        public bool Equals(Equatable? other) => other is not null && value == other.Value;

        private int Value => value;

        public override string ToString() => $"Equatable({value})";
    }

    private sealed class DisagreeingEquatable : IEquatable<DisagreeingEquatable>
    {
        public bool Equals(DisagreeingEquatable? other) => false;

        public override bool Equals(object? obj) => true;

        public override int GetHashCode() => 0;
    }

    private sealed class ComparableEquatable : IEquatable<ComparableEquatable>, IComparable
    {
        public bool Equals(ComparableEquatable? other) => false;

        public int CompareTo(object? obj) => 0;
    }

    private class Animal;

    private class Dog : Animal;

    private sealed class Puppy : Dog;

    private sealed class BaseFirstJudge : IEquatable<Animal>, IEquatable<Dog>
    {
        public bool Equals(Animal? other) => false;

        public bool Equals(Dog? other) => true;
    }

    private sealed class DerivedFirstJudge : IEquatable<Dog>, IEquatable<Animal>
    {
        public bool Equals(Dog? other) => true;

        public bool Equals(Animal? other) => false;
    }
#pragma warning restore CA1067
}
