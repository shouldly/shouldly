namespace Shouldly.Tests.ShouldNotThrow;

public class FuncOfTaskOfStringScenario
{
    [Fact]
    public void FuncOfTaskOfStringScenarioShouldFail()
    {
        Func<Task<string>> action = () => Task.FromException<string>(new RankException());

        Verify.ShouldFail(() =>
            action.ShouldNotThrow("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        Func<Task<string>> action = () => Task.Run(() => "Foo");

        var result = action.ShouldNotThrow();
        result.ShouldBe("Foo");
    }
}
