using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeNegative
{
    public class ZeroDoubleWithFunctionContextScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            0.0.ShouldBeNegative(() => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get {
                return "0.0 was 0d and should be negative but wasn't" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }
    }
}