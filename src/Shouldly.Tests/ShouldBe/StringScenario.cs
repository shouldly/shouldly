using NUnit.Framework;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class StringScenario : ShouldlyShouldTestScenario
    {
        private const string ThisOtherString = "this other string";
        private const string ThisString = "this string";

        protected override void ShouldPass()
        {
            ThisString.ShouldBe(ThisString);
        }

        protected override void ShouldThrowAWobbly()
        {
            ThisString.ShouldBe(ThisOtherString);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "ThisString should be \"this other string\" but was \"this string\""; }
        }
    }
}