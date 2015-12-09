using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldContain
{
    public class EmptyArrayScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var target = new int[0];
            target.ShouldContain(1, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"target should contain 1
    but was
actually
[]

Additional Info:
    Some additional context";
            }
        }
    }
}