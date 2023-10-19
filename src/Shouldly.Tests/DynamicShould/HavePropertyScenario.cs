using System.Dynamic;

public class HavePropertyScenario
{
    [Fact]
    public void HavePropertyScenarioShouldFail()
    {
        dynamic testDynamicObject = new ExpandoObject();
        testDynamicObject.Bar = "BarPropertyValue";
        Verify.ShouldFail(() => DynamicShould.HaveProperty(testDynamicObject, "foo", "Some additional context"),

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
        dynamic testDynamicObject = new ExpandoObject();
        testDynamicObject.Foo = "FooPropertyValue";
        Shouldly.DynamicShould.HaveProperty(testDynamicObject, "Foo");
    }
}