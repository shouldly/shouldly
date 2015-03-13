using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class StringArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[]{"a", "b", "c"}.ShouldContain("d");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return " new[]{\"a\", \"b\", \"c\"} should contain \"d\" but does not"; }
        }

        protected override void ShouldPass()
        {
            new[] { "a", "b", "c" }.ShouldContain("b");
        }
    }
}