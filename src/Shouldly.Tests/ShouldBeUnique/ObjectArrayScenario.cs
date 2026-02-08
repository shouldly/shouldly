namespace Shouldly.Tests.ShouldBeUnique;

public class ObjectArrayScenario
{
    [Fact]
    public void ObjectArrayScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            new object[] { 1, 2, 3, 4, 2 }.ShouldBeUnique("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        new object[] { 1, 2, 3, 4, 7 }.ShouldBeUnique();
    }
}