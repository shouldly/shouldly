namespace Shouldly.Tests.DynamicShould;

public class MultiLineHavePropertyScenario
{
    [Fact]
    public void MultiLineHavePropertyScenarioShouldFail()
    {
        dynamic testDynamicObject = new ExpandoObject();
        testDynamicObject.Bar = "BarPropertyValue";
        Verify.ShouldFail(() =>
            Shouldly.DynamicShould
                .HaveProperty(testDynamicObject, "foo", "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        dynamic testDynamicObject = new ExpandoObject();
        testDynamicObject.Foo = "FooPropertyValue";
        Shouldly.DynamicShould
            .HaveProperty(testDynamicObject, "Foo");
    }
}