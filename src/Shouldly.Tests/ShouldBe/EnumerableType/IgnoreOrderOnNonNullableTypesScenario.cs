using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderOnNonNullableTypesScenario : ShouldlyShouldTestScenario
    {
        // testing against non-ICollection IEnumerable, so we're not falling into the ICollection.Count short-circuit
        public IEnumerable<NonNullableType> Actual
        {
            get { yield return new NonNullableType(1); yield return new NonNullableType(2); }
        }

        protected override void ShouldPass()
        {
            var expected = new[]
            {
                new NonNullableType(2), 
                new NonNullableType(1)
            };

            Actual.ShouldBe(expected, ignoreOrder: true);
        }

        protected override void ShouldThrowAWobbly()
        {
            var expected = new[] 
            {
                new NonNullableType(2), 
                new NonNullableType(3), 
            };

            Actual.ShouldBe(expected, ignoreOrder: true);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"Actual should be [2, 3] (ignoring order) but Actual is missing [3] and [2, 3] is missing [1]"; }
        }

        public struct NonNullableType
        {
            readonly int identity;

            public NonNullableType(int identity)
            {
                this.identity = identity;
            }

            public bool Equals(NonNullableType other)
            {
                return identity == other.identity;
            }

            public override bool Equals(object obj)
            {
                if (ReferenceEquals(null, obj)) return false;
                return obj is NonNullableType && Equals((NonNullableType) obj);
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