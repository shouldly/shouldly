using System.Collections.Generic;
using Xunit;

namespace Shouldly.Tests.ShouldBeGreaterThan
{
    public class CustomObjectScenario
    {
        [Fact]
        public void CompareCustom()
        {
            var customA = new Custom { Val = 2 };
            var customB = new Custom { Val = 1 };
            var comparer = new CustomComparer<Custom>();
            customA.ShouldBeGreaterThan(customB, comparer);
        }

        public class CustomComparer<T> : IComparer<T>
        {
            public int Compare(T x, T y)
            {
                Custom x1 = x as Custom;
                Custom x2 = y as Custom;
                return x1.Val > x2.Val ? 1 : -1;
            }
        }

        class Custom
        {
            public int Val { get; set; }
        }
    }
}
