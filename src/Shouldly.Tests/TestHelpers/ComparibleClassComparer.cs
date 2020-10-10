using System.Collections.Generic;

namespace Shouldly.Tests.TestHelpers
{
    public class ComparibleClassComparer : IEqualityComparer<ComparibleClass>
    {
        public bool Equals(ComparibleClass x, ComparibleClass y)
        {
            return x.Property == y.Property;
        }

        public int GetHashCode(ComparibleClass obj)
        {
            return obj.Property.GetHashCode();
        }
    }
}
