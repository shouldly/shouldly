namespace Shouldly.Tests.ShouldBeAssignableTo
{
    public class IntAssignableToDoubleScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            2.ShouldBeAssignableTo<double>();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "2 should be assignable to System.Double but was System.Int32"; }
        }
    }
}