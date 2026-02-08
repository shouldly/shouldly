namespace Shouldly.Tests.ShouldNotThrow;

public class FuncOfTaskOfStringScenario
{
    [Fact]
    [UseCulture("en-US")]
    public void FuncOfTaskOfStringScenarioShouldFail()
    {
        var task = Task.Run(() => throw new RankException(), TestContext.Current.CancellationToken);
        Verify.ShouldFail(() =>
            task.ShouldNotThrow("Some additional context"));
    }

    [Fact]
    public void ShouldPass()
    {
        var task = Task.Run(() => "Foo");

        var result = task.ShouldNotThrow();
        result.ShouldBe("Foo");
    }
}