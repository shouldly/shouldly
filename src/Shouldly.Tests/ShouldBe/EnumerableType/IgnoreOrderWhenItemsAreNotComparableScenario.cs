namespace Shouldly.Tests.ShouldBe.EnumerableType;

public class IgnoreOrderWhenItemsAreNotComparableScenario
{
    // testing against non-ICollection IEnumerable, so we're not falling into the ICollection.Count short-circuit
    public IEnumerable<YourAverageNonComparableType> Actual
    {
        get
        {
            yield return new(1);
            yield return new(2);
        }
    }

    [Fact]
    public void IgnoreOrderWhenItemsAreNotComparableScenarioShouldFail()
    {
        var expected = new YourAverageNonComparableType[]
        {
            new(2),
            new(3)
        };
        Verify.ShouldFail(() =>

            Actual.ShouldBe(expected, true, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var expected = new YourAverageNonComparableType[]
        {
            new(2),
            new(1)
        };
        Actual.ShouldBe(expected, ignoreOrder: true);
    }

    public class YourAverageNonComparableType
    {
        private readonly int identity;

        public YourAverageNonComparableType(int identity) =>
            this.identity = identity;

        protected bool Equals(YourAverageNonComparableType? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(identity, other.identity);
        }

        public override bool Equals(object? obj) =>
            Equals(obj as YourAverageNonComparableType);

        public override int GetHashCode() => identity.GetHashCode();

        public override string ToString() => identity.ToString();
    }
}