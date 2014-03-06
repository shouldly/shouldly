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
            get { return " new[]{\"a\", \"b\", \"c\"} should not contain \"c\" but was [\"a\", \"b\", \"c\"]"; }
        }

        protected override void ShouldPass()
        {
            new[] { "a", "b", "c" }.ShouldNotContain("d");
        }
    }
}