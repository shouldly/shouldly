namespace Shouldly.MessageGenerators;

class ShouldBeCompletedSuccessfullyMessageGenerator : ShouldlyMessageGenerator
{
    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context.ShouldMethod == "ShouldBeCompletedSuccessfully";

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        var task = context.Actual as Task;
        var codePart = context.CodePart == "null" ? "Task" : context.CodePart;

        var outcome = task?.Status switch
        {
            null => "but was null",
            TaskStatus.Faulted => $"""
                                   but faulted with
                                   {FaultDescription(task!.Exception!)}
                                   """,
            TaskStatus.Canceled => "but was canceled",
            _ => $"but had not completed (status: {task!.Status})"
        };

        return $"""
                {codePart}
                    should be completed successfully {outcome}
                """;
    }

    // A task faulted by a single exception (the usual case) reports that exception rather than
    // the AggregateException wrapping it. Several are listed one by one, rather than through
    // AggregateException.ToString(), whose format differs between runtimes.
    static string FaultDescription(AggregateException exception)
    {
        var inner = exception.InnerExceptions;
        return inner.Count == 1
            ? inner[0].ToString()
            : string.Join(Environment.NewLine, inner.Select((e, i) => $"[{i}] {e}"));
    }
}
