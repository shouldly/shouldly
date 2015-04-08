using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldBeEmpty
{
    public class ShouldBeEmptyBasicScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "a".ShouldBeEmpty(() => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "\"a\" should be empty but was \"a\"" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "".ShouldBeEmpty(() => "Some additional context");
        }
    }
}