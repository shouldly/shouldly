using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldBeEmpty
{
    public class ShouldBeEmptyBasicScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "a".ShouldBeEmpty();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"a\" should be empty but was \"a\""; }
        }

        protected override void ShouldPass()
        {
            "".ShouldBeEmpty();
        }
    }
}