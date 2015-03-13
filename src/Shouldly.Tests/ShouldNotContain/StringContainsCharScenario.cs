using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsCharScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Foo".ShouldNotContain('F');
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Foo\" should not contain F but does"; }
        }

        protected override void ShouldPass()
        {
            "Foo".ShouldNotContain('B');
        }
    }
}