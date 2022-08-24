namespace Shouldly.Tests.DynamicShould
{
    public class HavePropertyNonDynamicScenario
    {
        private class Foo
        {
            public string? Bar { get; set; }
        }

        [Fact]
        public void HavePropertyNonDynamicScenarioShouldFail()
        {
            dynamic testDynamicObject = new Foo();
            testDynamicObject.Bar = "BarPropertyValue";

            Verify.ShouldFail(() =>
            Shouldly.DynamicShould
                .HaveProperty(testDynamicObject, "foo", "Some additional context"),

errorWithSource:
@"Dynamic object ""testDynamicObject"" should contain property ""foo"" but does not." + @"

Additional Info:
    Some additional context",

errorWithoutSource:
@"Dynamic object should contain property ""foo"" but does not." + @"

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            dynamic testDynamicObject = new Foo();
            Shouldly.DynamicShould.HaveProperty(testDynamicObject, "Bar");
        }
    }
}