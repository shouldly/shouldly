using static Shouldly.Tests.CommonWaitDurations;

namespace Shouldly.Tests;

public static partial class StackTraceTests
{
    [Theory]
    [MemberData(nameof(ExceptionThrowers))]
    public static void Top_stack_frame_is_user_code(ExceptionThrower exceptionThrower)
    {
        var exception = exceptionThrower.Catch()!;

        var stackTraceLines = exception.StackTrace!.Split(['\r', '\n'], StringSplitOptions.RemoveEmptyEntries);

        stackTraceLines.First().ShouldContain(exceptionThrower.ThrowingAction.Method.Name);
    }

    [Theory]
    [MemberData(nameof(ExceptionThrowers))]
    public static void Stack_trace_is_trimmed_the_same_as_default_exception_stack_traces(ExceptionThrower exceptionThrower)
    {
        var shouldlyException = exceptionThrower.Catch();
        var defaultException = new ExceptionThrower(typeof(Exception), false, () => throw new()).Catch();

        var shouldlyEndingWhitespace = GetEndingWhitespace(shouldlyException.StackTrace!);
        var defaultEndingWhitespace = GetEndingWhitespace(defaultException.StackTrace!);

        shouldlyEndingWhitespace.ShouldBe(defaultEndingWhitespace);
    }

    private static string GetEndingWhitespace(string value) =>
        value[value.TrimEnd().Length..];

    public static TheoryData<ExceptionThrower> ExceptionThrowers()
    {
        return new TheoryData<ExceptionThrower>(new ExceptionThrowerCollectionBuilder()
            .Add<ShouldAssertException>(
                throwDirectly: () => throw new ShouldAssertException(null),
                throwInShouldlyAssembly:
                [
                    FailingUserCode_ShouldBeTrue,
                    FailingUserCode_ShouldContain
                ])

            .Add<ShouldlyTimeoutException>(
                throwDirectly: () => throw new ShouldlyTimeoutException(null, null))

            .Add<ShouldCompleteInException>(
                throwDirectly: () => throw new ShouldCompleteInException(null, null),
                throwInShouldlyAssembly: FailingUserCode_CompleteIn)

            .Add<ShouldMatchApprovedException>(
                throwDirectly: () => throw new ShouldMatchApprovedException(null, null, null))

            .Build());
    }

    private static void FailingUserCode_ShouldBeTrue()
    {
        false.ShouldBeTrue();
    }

    private static void FailingUserCode_ShouldContain()
    {
        // Causes a few more frames that need to be filtered
        "".ShouldContain("42");
    }

    private static void FailingUserCode_CompleteIn()
    {
        // Throws a different exception type
        Should.CompleteIn(Task.Delay(LongWait), TimeSpan.Zero);
    }
}