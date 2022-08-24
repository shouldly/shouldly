namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderOnNonNullableTypesScenario
    {
        public IEnumerable<NonNullableType> Actual
        {
            get
            {
                yield return new NonNullableType(1);
                yield return new NonNullableType(2);
            }
        }

        [Fact]
        public void IgnoreOrderOnNonNullableTypesScenarioShouldFail()
        {
            var expected = new[]
            {
                new NonNullableType(2),
                new NonNullableType(3),
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
            var expected = new[]
            {
                new NonNullableType(2),
                new NonNullableType(1)
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

            public bool Equals(NonNullableType other)
            {
                return identity == other.identity;
            }

            public override bool Equals(object? obj)
            {
                if (obj is null) return false;
                return obj is NonNullableType type &&
                       Equals(type);
            }

            public override int GetHashCode()
            {
                return identity;
            }

            public override string ToString()
            {
                return identity.ToString();
            }
        }
    }
}