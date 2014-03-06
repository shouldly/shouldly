namespace Shouldly.Tests.ShouldContain
{
    public class StringContainsCharScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Foo".ShouldContain('B');
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Foo\" should contain B but was \"Foo\""; }
        }

        protected override void ShouldPass()
        {
            "Foo".ShouldContain('F');
        }
    }
}