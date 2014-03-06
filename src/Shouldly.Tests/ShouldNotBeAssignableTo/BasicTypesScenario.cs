namespace Shouldly.Tests.ShouldNotBeAssignableTo
{
    public class BasicTypesScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            2.ShouldNotBeAssignableTo<int>();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "2 should not be assignable to System.Int32 but was 2"; }
        }

        protected override void ShouldPass()
        {
            1.ShouldNotBeAssignableTo<string>();
        }
    }
}