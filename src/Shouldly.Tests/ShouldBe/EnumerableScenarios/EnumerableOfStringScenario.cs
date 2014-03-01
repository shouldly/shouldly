namespace Shouldly.Tests.ShouldBe.EnumerableScenarios
{
    public class EnumerableOfStringScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldPass()
        {
            new[] { "foo" }.ShouldBe<string>(new[] { "foo" });
        }

        protected override void ShouldThrowAWobbly()
        {
            new[] { "foo" }.ShouldBe<string>(new[] { "foo2" });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new[] { \"foo\" } should be [\"foo2\"] but was [\"foo\"] difference [*\"foo\"*]"; }
        }
    }
}