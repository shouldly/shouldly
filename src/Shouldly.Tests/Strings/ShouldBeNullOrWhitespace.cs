#if net40
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings
{
    public class ShouldBeNullOrWhiteSpace : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "a".ShouldBeNullOrWhiteSpace("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage =>
            "\"a\" should be null or white space " +
            "Additional Info: " +
            "Some additional context";

        protected override void ShouldPass()
        {
            ((string)null).ShouldBeNullOrWhiteSpace();
            string.Empty.ShouldBeNullOrWhiteSpace();
            "   ".ShouldBeNullOrWhiteSpace();
        }
    }
}
#endif