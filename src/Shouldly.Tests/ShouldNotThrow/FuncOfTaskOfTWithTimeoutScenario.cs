using static Shouldly.Tests.CommonWaitDurations;

namespace Shouldly.Tests.ShouldNotThrow;

public class FuncOfTaskOfTWithTimeoutScenario
{
    [Fact]
    public void ShouldThrowAWobbly()
    {
        Func<Task<string>> action = () => Task.Run(async () =>
        {
            await Task.Delay(LongWait);
            return "foo";
        });

        var ex = Should.Throw<ShouldCompleteInException>(() =>
            action.ShouldNotThrow(ShortWait, "Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(ChuckedAWobblyErrorMessage);
    }

    private string ChuckedAWobblyErrorMessage =
        $"""
        Task
                should complete in
            {ShortWait}
                but did not
            Additional Info:
            Some additional context
        """;

    [Fact]
    public void ShouldPass()
    {
        Func<Task<string>> action = () => Task.Run(() => "foo");

        var result = action.ShouldNotThrow(LongWait);
        result.ShouldBe("foo");
    }
}
