using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotContain
{
    public class StringArrayScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new[]{"a", "b", "c"}.ShouldNotContain("c");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return " new[]{\"a\", \"b\", \"c\"} should not contain \"c\" but does"; }
        }

        protected override void ShouldPass()
        {
            new[] { "a", "b", "c" }.ShouldNotContain("d");
        }
    }
}