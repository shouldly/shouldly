#if net40
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.DynamicShouldTests
{
    public class HavePropertyNonDynamicScenario
    {
        class Foo
        {
            public string Bar { get; set; }
        }

        [Fact]
        public void HavePropertyNonDynamicScenarioShouldFail()
        {
            dynamic testDynamicObject = new Foo();
            testDynamicObject.Bar = "BarPropertyValue";

            Verify.ShouldFail(() =>
            DynamicShould
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
            DynamicShould.HaveProperty(testDynamicObject, "Bar");
        }
    }
}
#endif