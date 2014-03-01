namespace Shouldly.Tests.ShouldBe
{
    public class StringScenario : ShouldlyTestScenario
    {
        private const string ThisOtherString = "this other string";
        private const string ThisString = "this string";

        protected override void ShouldPass()
        {
            ThisString.ShouldBe(ThisString);
        }

        protected override void ShouldThrowAWobbly()
        {
            ThisString.ShouldBe(ThisOtherString);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "ThisString should be \"this other string\" but was \"this string\""; }
        }

        protected override void NotVersionShouldPass()
        {
            ThisString.ShouldNotBe(ThisOtherString);
        }

        protected override void NotVersionShouldThrowAWobbly()
        {
            ThisString.ShouldNotBe(ThisString);
        }

        protected override string NotVersionChuckedAWobblyErrorMessage
        {
            get { return "ThisString should not be \"this string\" but was \"this string\""; }
        }
    }
}