namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderOnNonNullableTypesScenario
{
    public IEnumerable<NonNullableType> Actual
    {
        get
        {
            yield return new(1);
            yield return new(2);
        }
    }

    [Fact]
    public void IgnoreOrderOnNonNullableTypesScenarioShouldFail()
    {
        var expected = new NonNullableType[]
        {
            new(2),
            new(3),
        };
        Verify.ShouldFail(() =>
            Actual.ShouldBe(expected, true, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var expected = new NonNullableType[]
        {
            new(2),
            new(1)
        };

        Actual.ShouldBe(expected, ignoreOrder: true);
    }

    public struct NonNullableType
    {
        private readonly int identity;

        public NonNullableType(int identity)
        {
            this.identity = identity;
        }

        public bool Equals(NonNullableType other) =>
            identity == other.identity;

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            return obj is NonNullableType type &&
                   Equals(type);
        }

        public override int GetHashCode() => identity;

        public override string ToString() => identity.ToString();
    }
}