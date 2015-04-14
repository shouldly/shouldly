using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings
{
    public class ShouldNotBeNullOrEmpty : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "".ShouldNotBeNullOrEmpty(() => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "\"\" should not be null or empty " +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            " ".ShouldNotBeNullOrEmpty();
        }
    }
}