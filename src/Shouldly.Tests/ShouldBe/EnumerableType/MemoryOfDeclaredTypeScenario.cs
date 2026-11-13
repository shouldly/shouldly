namespace Shouldly.Tests.ShouldBe.EnumerableType;

/// <summary>
/// A memory's elements are compared as their declared type, here equal only through <see cref="IComparable{T}"/> of
/// an interface, and its failure message judges them as the check did.
/// </summary>
public class MemoryOfDeclaredTypeScenario
{
    [Fact]
    public void MemoryOfDeclaredTypeScenarioShouldFail()
    {
        var actual = new Memory<IShape>([new Circle(1), new Circle(2)]);
        var expected = new Memory<IShape>([new Circle(1), new Circle(3)]);

        Verify.ShouldFail(() =>
            actual.ShouldBe(expected, "Some additional context"));
    }

    [Fact]
    public void ShouldPass() =>
        new ReadOnlyMemory<IShape>([new Circle(1)]).ShouldBe(new ReadOnlyMemory<IShape>([new Circle(1)]));

    public interface IShape : IComparable<IShape>
    {
        int Radius { get; }
    }

    private sealed class Circle(int radius) : IShape
    {
        public int Radius => radius;

        public int CompareTo(IShape? other) => Radius.CompareTo(other?.Radius);

        public override string ToString() => $"Circle {Radius}";
    }
}
