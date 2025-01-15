using static Shouldly.Tests.CommonWaitDurations;

namespace Shouldly.Tests.ShouldNotThrow;

public class FuncOfTaskOfTWithTimeoutScenario
{
    [Fact]
    public void ShouldThrowAWobbly()
    {
        var task = Task.Run(async () =>
        {
            await Task.Delay(LongWait);
            return "foo";
        });

        var ex = Should.Throw<ShouldCompleteInException>(() =>
            task.ShouldNotThrow(ShortWait, "Some additional context"));

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
        var task = Task.Run(() => "foo");

        var result = task.ShouldNotThrow(LongWait);
        result.ShouldBe("foo");
    }
}