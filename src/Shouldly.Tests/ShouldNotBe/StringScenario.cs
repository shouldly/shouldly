using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBe
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        const string ThisOtherString = "this other string";
        const string ThisString = "this string";

        protected override void ShouldPass()
        {
            ThisString.ShouldNotBe(ThisOtherString);
        }

        protected override void ShouldThrowAWobbly()
        {
            ThisString.ShouldNotBe(ThisString);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "ThisString should not be \"this string\" but was"; }
        }
    }
}