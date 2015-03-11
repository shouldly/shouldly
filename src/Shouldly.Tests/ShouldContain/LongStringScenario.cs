using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class LongStringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var longString = new string('a', 110) + "zzzz";
            longString.ShouldContain("fff");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            //TODO This should really show an elipsis if it is truncated
            get { return "longString should contain \"fff\" " +
                         "but did not"; }
        }

        protected override void ShouldPass()
        {
            var longString = new string('a', 110) + "zzzz";
            longString.ShouldContain("zzzz");
        }
    }
}