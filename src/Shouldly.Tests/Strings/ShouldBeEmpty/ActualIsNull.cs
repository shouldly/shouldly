using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldBeEmpty
{
    public class ActualIsNull : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            ((string) null).ShouldBeEmpty("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "(string)null should be empty but was null" +
                       "Additional Info:" +
                       "Some additional context";
            }
        }
    }
}