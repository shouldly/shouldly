using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderIEnumerableMethodYieldReturn : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            GetEnumerable().ShouldBe(new[] {1}, true);
        }

        protected override void ShouldThrowAWobbly()
        {
            GetEnumerable().ShouldBe(new[] {1, 2}, true);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "GetEnumerable() should be [1, 2] (ignoring order) but GetEnumerable() is missing [2]";
            }
        }

        private static IEnumerable<int> GetEnumerable()
        {
            yield return 1;
        }
    }
}
