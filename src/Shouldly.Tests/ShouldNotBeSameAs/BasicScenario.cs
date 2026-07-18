namespace Shouldly.Tests.ShouldNotBeSameAs;

public class BasicScenario
{
    [Fact]
    public void BasicScenarioShouldFail()
    {
        var apple = new object();
        Verify.ShouldFail(() =>
            apple.ShouldNotBeSameAs(apple, "Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var apple = new object();
        var orange = new object();

        apple.ShouldNotBeSameAs(orange);
    }
}