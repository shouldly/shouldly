using System;
using System.Diagnostics.CodeAnalysis;

namespace Shouldly
{
    [Serializable]
    class ObjectEqualityComparer<T> : System.Collections.Generic.EqualityComparer<T>
    {
        public override bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            if (x != null)
            {
                return y != null && x.Equals(y);
            }
            return y == null;
        }

        public override int GetHashCode([AllowNull] T obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return obj.GetHashCode();
        }

        public override bool Equals(object? obj) => obj is ObjectEqualityComparer<T>;

        public override int GetHashCode()
        {
            return GetType().Name.GetHashCode();
        }
    }
}