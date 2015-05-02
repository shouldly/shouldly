using NUnit.Framework;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldEndWith
{
    public class ShouldIgnoreCaseByDefault: ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldEndWith("ze");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "\"Cheese\" should end with \"ze\" but was \"Cheese\""; }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldEndWith("SE");
        }
    }
}