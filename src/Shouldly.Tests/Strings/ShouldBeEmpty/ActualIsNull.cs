using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldBeEmpty
{
    public class ActualIsNull : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            ((string)null).ShouldBeEmpty();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "((string)null) should be empty but was null"; }
        }
    }
}