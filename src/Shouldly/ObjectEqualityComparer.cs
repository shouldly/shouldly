using System;
using System.Runtime;

namespace Shouldly
{
    [Serializable]
    class ObjectEqualityComparer<T> : System.Collections.Generic.EqualityComparer<T>
    {
        [TargetedPatchingOptOut("Performance critical to inline this type of method across NGen image boundaries")]
        public ObjectEqualityComparer()
        {
        }

        public override bool Equals(T x, T y)
        {
            if (x != null)
            {
                return y != null && x.Equals(y);
            }
            return y == null;
        }

        [TargetedPatchingOptOut("Performance critical to inline across NGen image boundaries")]
        public override int GetHashCode(T obj)
        {
            if (obj == null)
            {
                return 0;
            }
            return obj.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            var objectEqualityComparer = obj as ObjectEqualityComparer<T>;
            return objectEqualityComparer != null;
        }

        public override int GetHashCode()
        {
            return GetType().Name.GetHashCode();
        }
    }
}