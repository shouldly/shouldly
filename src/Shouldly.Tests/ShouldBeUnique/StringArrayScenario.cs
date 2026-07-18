namespace Shouldly.Tests.ShouldBeUnique;

public class StringArrayScenario
{
    [Fact]
    public void StringArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new[] { "string2", "string1", "string42", "string2" }.ShouldBeUnique("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new[] { "string2", "string1", "string42", "string53" }.ShouldBeUnique();
    }
}