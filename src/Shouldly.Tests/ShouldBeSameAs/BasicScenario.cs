namespace Shouldly.Tests.ShouldBeSameAs;

public class BasicScenario
{
    [Fact]
    public void BasicScenarioShouldFail()
    {
        var apple = new object();
        var orange = new object();
        Verify.ShouldFail(() =>
            apple.ShouldBeSameAs(orange, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var apple = new object();

        apple.ShouldBeSameAs(apple);
    }
}