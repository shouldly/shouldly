using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe.EnumerableType
{
    public class IgnoreOrderFalseIEnumerableMethodYieldReturn : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            GetEnumerable().ShouldBe(new[] {1}, false);
        }

        protected override void ShouldThrowAWobbly()
        {
            GetEnumerable().ShouldBe(new[] {1, 2}, false);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "GetEnumerable() should be [1, 2] but was [1] difference [1, *]";
            }
        }

        private static IEnumerable<int> GetEnumerable()
        {
            yield return 1;
        }
    }
}
