using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeAssignableTo
{
    public class BasicTypesScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            2.ShouldBeAssignableTo<double>();
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "2 should be assignable to System.Double but was System.Int32"; }
        }

        protected override void ShouldPass()
        {
            1.ShouldBeAssignableTo<int>();
        }
    }
}