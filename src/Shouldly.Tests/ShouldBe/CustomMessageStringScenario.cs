using System;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class CustomMessageStringScenario : ShouldlyShouldTestScenario
    {
        private const string ThisIsACustomMessage = "Custom";
        private const string ThisOtherString = "this other string";
        private const string ThisString = "this string";

        protected override void ShouldPass()
        {
            ThisString.ShouldBe(ThisString, () => ThisIsACustomMessage);
        }

        protected override void ShouldThrowAWobbly()
        {
            ThisString.ShouldBe(ThisOtherString, () => ThisIsACustomMessage);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return ThisIsACustomMessage + Environment.NewLine + "ThisString should be \"this other string\" but was \"this string\""; }
        }
    }
}
