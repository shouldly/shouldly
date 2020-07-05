using System.Collections.Generic;

namespace Shouldly.Tests.TestHelpers
{
    internal class CustomComparer<T> : IComparer<T>
    {
        public int Compare(T x, T y)
        {
            var x1 = x as Custom;
            var x2 = y as Custom;
            if (x1.Val == x2.Val)
                return 0;
            return x1.Val > x2.Val ? 1 : -1;
        }
    }

    internal class Custom
    {
        public int Val { get; set; }
    }
}
