using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeSameAs
{
    public class SameListDifferentInstanceScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var list = new List<int> { 1, 2, 3 };
            var equalListWithDifferentRef = new List<int> { 1, 2, 3 };
            list.ShouldBeSameAs(equalListWithDifferentRef);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"list should be same as [1, 2, 3]
    but was
[1, 2, 3] difference [1, 2, 3]"; }
        }
    }
}