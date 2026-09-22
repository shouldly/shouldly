namespace Shouldly.Tests.ShouldBeSubsetOf;

public class EquatableScenario
{
    [Fact]
    public void EquatableScenarioShouldFail()
    {
        var actual = new[] { new Equatable(1), new Equatable(2) };
        var expected = new[] { new Equatable(1) };

        Verify.ShouldFail(() =>
            actual.ShouldBeSubsetOf(expected, "Some additional context"));
    }

    // Implements IEquatable<T> without overriding Equals(object), so only EqualityComparer<T>.Default treats equal values as equal
    class Equatable(int value) : IEquatable<Equatable>
    {
        public int Value { get; } = value;

        public bool Equals(Equatable? other) => other != null && other.Value == Value;

        public override int GetHashCode() => Value;

        public override string ToString() => $"Equatable({Value})";
    }
}