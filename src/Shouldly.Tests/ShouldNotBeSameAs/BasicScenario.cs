using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeSameAs
{
    public class BasicScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var zulu = new object();

            zulu.ShouldNotBeSameAs(zulu, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "zulu should not be same as System.Object but was System.Object" +
                         "Additional Info:" +
                         "Some additional context"; }
        }

        protected override void ShouldPass()
        {
            var zulu = new object();
            var tutsie = new object();

            zulu.ShouldNotBeSameAs(tutsie);
        }
    }
}