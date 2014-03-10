using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotBeSameAs
{
    public class BasicScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var zulu = new object();

            zulu.ShouldNotBeSameAs(zulu);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "zulu should not be same as System.Object but was System.Object"; }
        }

        protected override void ShouldPass()
        {
            var zulu = new object();
            var tutsie = new object();

            zulu.ShouldNotBeSameAs(tutsie);
        }
    }
}