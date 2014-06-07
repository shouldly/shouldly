using System.Collections.Generic;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class StringListScenario : ShouldlyShouldTestScenario
    {
        private readonly List<string> _thisOtherStringList = new List<string>{ "1", "3" };
        private readonly List<string> _thisString = new List<string> { "1", "2" };

        protected override void ShouldPass()
        {
            _thisString.ToArray().ShouldBe(_thisString);
        }

        protected override void ShouldThrowAWobbly()
        {
            _thisString.ShouldBe(_thisOtherStringList);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "_thisString should be [\"1\", \"3\"] but was [\"1\", \"2\"] difference [\"1\", *\"2\"*]"; }
        }
    }
}