using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeAssignableTo
{
    public class BasicTypesScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            2.ShouldBeAssignableTo<double>(() => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "2 should be assignable to System.Double but was System.Int32" +
                       "Additional Info: " +
                       "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            1.ShouldBeAssignableTo<int>(() => "Some additional context");
        }
    }
}