using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class LongStringScenario : ShouldlyShouldTestScenario
    {
        protected string target = new string('a', 110) + "zzzz";

        protected override void ShouldThrowAWobbly()
        {
            target.ShouldContain("fff");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "target should contain \"fff\" (case insensitive comparison) " +
                       "but was actually \"aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa" +
                       "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa...\"";
            }
        }

        protected override void ShouldPass()
        {
            target.ShouldContain("zzzz");
        }
    }
}