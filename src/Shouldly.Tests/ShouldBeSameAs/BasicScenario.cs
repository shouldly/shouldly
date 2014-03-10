using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldBeSameAs
{
    public class BasicScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var zulu = new object();
            var tutsie = new object();

            zulu.ShouldBeSameAs(tutsie);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                //TODO maybe include GetHashCode?
                return "zulu should be same as System.Object but was System.Object";
            }
        }

        protected override void ShouldPass()
        {
            var zulu = new object();

            zulu.ShouldBeSameAs(zulu);
        }
    }
}