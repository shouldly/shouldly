using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBe
{
    public class ComparingObjectWithString : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            new object().ShouldBe("this string");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "new object() should be \"this string\" but was System.Object"; }
        }
    }
}