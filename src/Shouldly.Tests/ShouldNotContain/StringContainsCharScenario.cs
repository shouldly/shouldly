namespace Shouldly.Tests.ShouldNotContain
{
    public class StringContainsCharScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Foo".ShouldNotContain('F');
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Foo\" should not contain F but was \"Foo\""; }
        }

        protected override void ShouldPass()
        {
            "Foo".ShouldNotContain('B');
        }
    }
}