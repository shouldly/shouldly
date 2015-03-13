using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings
{
    public class ShouldContainWithoutWhitespace : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Fun   with space   and \"quotes\"".ShouldContainWithoutWhitespace("Fun with space and missing quotes");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Fun   with space   and \\\"quotes\\\"\" " +
                         "should contain without whitespace \"Fun with space and missing quotes\" " +
                         "but does not"; }
        }

        protected override void ShouldPass()
        {
            "Fun   with space   and \"quotes\"".ShouldContainWithoutWhitespace("Fun with space and 'quotes'");
        }
    }
}