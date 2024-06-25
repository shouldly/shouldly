namespace Shouldly.Tests;

public class ShouldCompleteInTests
{
    private static readonly TimeSpan ShortWait = TimeSpan.FromSeconds(0.5);
    private static readonly TimeSpan LongWait = TimeSpan.FromSeconds(15);
    private static readonly TimeSpan ImmediateTaskTimeout = TimeSpan.FromSeconds(2);
    
    [Fact]
    public void ShouldCompleteIn_WhenFinishBeforeTimeout()
    {
        Should.NotThrow(
            () => Should.CompleteIn(
                () => Thread.Sleep(ShortWait),
                LongWait));
    }

    [Fact]
    public void ShouldCompleteIn_WhenFinishAfterTimeout()
    {
        var ex = Should.Throw<ShouldlyTimeoutException>(
            () => Should.CompleteIn(
                () => Thread.Sleep(LongWait),
                ShortWait,
                "Some additional context"));

      ex.Message.ShouldContainWithoutWhitespace(
            """
            Delegate
                should complete in
            00:00:00.5000000
                but did not
            Additional Info:
            Some additional context
            """);
    }

    [Fact]
    public void ShouldCompleteInTask_WhenFinishAfterTimeout()
    {
        var ex = Should.Throw<ShouldlyTimeoutException>(() =>
            Should.CompleteIn(
                () => Task.Run(
                    () => Thread.Sleep(LongWait)),
                ShortWait,
                "Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(
            """
            Task
                should complete in
            00:00:00.5000000
                but did not
            Additional Info:
            Some additional context
            """);
    }

    [Fact]
    public void ShouldCompleteIn_WhenThrowsNonTimeoutException()
    {
        Should.Throw<NotImplementedException>(
            () => Should.CompleteIn(
                () => throw new NotImplementedException(),
                ImmediateTaskTimeout));
    }

    [Fact]
    public void ShouldCompleteInT_WhenFinishBeforeTimeout()
    {
        Should.NotThrow(
            () => Should.CompleteIn(
            () =>
            {
                Thread.Sleep(ShortWait);
                return "";
            },
            LongWait));
    }

    [Fact]
    public void ShouldCompleteInT_WhenFinishAfterTimeout()
    {
        var ex = Should.Throw<ShouldlyTimeoutException>(
            () => Should.CompleteIn(
                () =>
                {
                    Thread.Sleep(LongWait);
                    return "";
                },
                ShortWait,
                "Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(
            """
            Delegate
                should complete in
            00:00:00.5000000
                but did not
            Additional Info:
            Some additional context
            """);
    }

    [Fact]
    public void ShouldCompleteInTaskT_WhenFinishAfterTimeout()
    {
        var ex = Should.Throw<ShouldlyTimeoutException>(
            () => Should.CompleteIn(() =>
                {
                    return Task.Run(
                        () =>
                        {
                            Thread.Sleep(LongWait);
                            return "";
                        });
                },
                ShortWait,
                "Some additional context"));

        ex.Message.ShouldContainWithoutWhitespace(
            """
            Task
                should complete in
            00:00:00.5000000
                but did not
            Additional Info:
            Some additional context
            """);
    }

    [Fact]
    public void ShouldCompleteInT_WhenThrowsNonTimeoutException()
    {
        Should.Throw<NotImplementedException>(
            () => Should.CompleteIn(
                new Func<string>(
                    () => throw new NotImplementedException()),
                ImmediateTaskTimeout));
    }
}