using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderWhenItemsAreNotComparableScenario : ShouldlyShouldTestScenario
    {
        // testing against non-ICollection IEnumerable, so we're not falling into the ICollection.Count short-circuit
        public IEnumerable<YourAverageNonComparableType> Actual
        {
            get { yield return new YourAverageNonComparableType(1); yield return new YourAverageNonComparableType(2); }
        }

        protected override void ShouldPass()
        {
            var expected = new[]
            {
                new YourAverageNonComparableType(2), 
                new YourAverageNonComparableType(1)
            };

            Actual.ShouldBe(expected, ignoreOrder: true);
        }

        protected override void ShouldThrowAWobbly()
        {
            var expected = new[] 
            {
                new YourAverageNonComparableType(2), 
                new YourAverageNonComparableType(3)
            };

            Actual.ShouldBe(expected, ignoreOrder: true);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"Actual should be [2, 3] (ignoring order) but Actual is missing [3] and [2, 3] is missing [1]"; }
        }

        public class YourAverageNonComparableType
        {
            readonly int identity;

            public YourAverageNonComparableType(int identity)
            {
                this.identity = identity;
            }

            protected bool Equals(YourAverageNonComparableType other)
            {
                if (ReferenceEquals(null, other)) return false;
                if (ReferenceEquals(this, other)) return true;
                return string.Equals(identity, other.identity);
            }

            public override bool Equals(object obj)
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
}