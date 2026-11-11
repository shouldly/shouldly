namespace Shouldly.Internals.Failures;

/// <summary>
/// A task should have completed successfully.
/// </summary>
sealed class CompletedSuccessfullyFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    /// <summary>
    /// The task was null.
    /// </summary>
    public FailureMessage WithNoTask() =>
        new(this, sentence => Word(sentence, null, null));

    /// <summary>
    /// The task had this status, read once so that a task completing concurrently can't contradict the message.
    /// </summary>
    public FailureMessage With(TaskStatus status, AggregateException? exception) =>
        new(this, sentence => Word(sentence, status, exception));

    internal static string Word(Sentence sentence, TaskStatus? status, AggregateException? exception)
    {
        var codePart = sentence.Subject == "null" ? "Task" : sentence.Subject;

        var outcome = status switch
        {
            null => "but was null",
            TaskStatus.Faulted => $"""
                                   but faulted with
                                   {FaultDescription(exception!)}
                                   """,
            TaskStatus.Canceled => "but was canceled",
            _ => $"but had not completed (status: {status})"
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
