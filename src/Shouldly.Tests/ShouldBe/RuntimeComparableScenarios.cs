namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// A type that is equal only through <see cref="IComparable{T}"/> of a type it shares with the other value, here an
/// interface, is compared with it whatever static type the assertion is made at: as the interface, as
/// <see cref="object"/>, or as the element of a sequence nested in another.
/// </summary>
public class RuntimeComparableScenarios
{
    [Fact]
    public void AtTheInterface() =>
        ((IShape)new Circle(1)).ShouldBe(new Circle(1));

    [Fact]
    public void AsObject()
    {
        ((object)new Circle(1)).ShouldBe(new Circle(1));
        ((object)new Circle(1)).ShouldNotBe(new Circle(2));
    }

    [Fact]
    public void ASequenceAsObject() =>
        ((object)Shapes(1, 2)).ShouldBe(Shapes(1, 2));

    [Fact]
    public void ANestedSequence()
    {
        new List<IShape[]> { Shapes(1, 2) }.ShouldBe([Shapes(1, 2)]);
        Should.Throw<ShouldAssertException>(() => new List<IShape[]> { Shapes(1, 2) }.ShouldBe([Shapes(1, 3)]));
    }

    [Fact]
    public void AnObjectSequence() =>
        new List<object> { new Circle(1) }.ShouldBe([new Circle(1)]);

    [Fact]
    public void RuntimeEquatableDecidesFirst() =>
        // IEquatable<IShape> says unequal, and is the type's own equality, so it decides before CompareTo (#1428).
        ((object)new EquatableCircle(1)).ShouldNotBe(new EquatableCircle(1));

    [Fact]
    public void TheMostDerivedComparableDecides()
    {
        // The expected judge's IComparable<IShape> says equal, and its IComparable<Circle> compares radii.
        ((object)new Circle(2)).ShouldNotBe(new Judge(1));
        ((object)new Circle(1)).ShouldBe(new Judge(1));
    }

    private static IShape[] Shapes(params int[] radii) => radii.Select(IShape (radius) => new Circle(radius)).ToArray();

    public interface IShape : IComparable<IShape>
    {
        int Radius { get; }
    }

    private class Circle(int radius) : IShape
    {
        public int Radius => radius;

        public int CompareTo(IShape? other) => Radius.CompareTo(other?.Radius);
    }

#pragma warning disable CA1067 // Deliberately disagrees with object.Equals to show which interface is consulted.
    private sealed class EquatableCircle(int radius) : Circle(radius), IEquatable<IShape>
    {
        public bool Equals(IShape? other) => false;
    }
#pragma warning restore CA1067

    private sealed class Judge(int radius) : IComparable<IShape>, IComparable<Circle>
    {
        public int CompareTo(IShape? other) => 0;

        public int CompareTo(Circle? other) => radius.CompareTo(other?.Radius);
    }
}
