﻿namespace Shouldly.Tests.ShouldBe.EnumerableType;

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

                Actual.ShouldBe(expected, true, "Some additional context"),

            errorWithSource:
            @"Actual
    should be (ignoring order)
[2, 3]
    but
Actual
    is missing
[3]
    and
[2, 3]
    is missing
[1]

Additional Info:
    Some additional context",

            errorWithoutSource:
            @"[1, 2]
    should be (ignoring order)
[2, 3]
    but
[1, 2]
    is missing
[3]
    and
[2, 3]
    is missing
[1]

Additional Info:
    Some additional context");
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

        public YourAverageNonComparableType(int identity)
        {
            this.identity = identity;
        }

        protected bool Equals(YourAverageNonComparableType? other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(identity, other.identity);
        }

        public override bool Equals(object? obj)
        {
            return Equals(obj as YourAverageNonComparableType);
        }

        public override int GetHashCode()
        {
            return identity.GetHashCode();
        }

        public override string ToString()
        {
            return identity.ToString();
        }
    }
}