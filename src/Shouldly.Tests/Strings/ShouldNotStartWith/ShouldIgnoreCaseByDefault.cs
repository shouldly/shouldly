using NUnit.Framework;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.Strings.ShouldNotStartWith
{
    public class ShouldIgnoreCaseByDefault : ShouldlyShouldTestScenario
    {

        protected override void ShouldThrowAWobbly()
        {
            "Cheese".ShouldNotStartWith("cH", "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return "\"Cheese\" should not start with \"cH\" but was \"Cheese\"" +
                "Additional Info: " +
                "Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            "Cheese".ShouldNotStartWith("Ce");
        }
    }
}