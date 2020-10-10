using System.Collections.Generic;

namespace Shouldly.Tests.TestHelpers
{
    public class ComparableClassComparer : IEqualityComparer<ComparableClass>
    {
        public bool Equals(ComparableClass x, ComparableClass y)
        {
            return x.Property == y.Property;
        }

        public int GetHashCode(ComparableClass obj)
        {
            return obj.Property.GetHashCode();
        }
    }
}
