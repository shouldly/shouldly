using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Extension methods for Task completion assertions
/// </summary>
[DebuggerStepThrough]
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldBeCompletedSuccessfullyExtensions
{
    /// <summary>
    /// Asserts that the task has already completed successfully (<see cref="TaskStatus.RanToCompletion"/>).
    /// </summary>
    public static void ShouldBeCompletedSuccessfully(this Task task,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
    {
        if (task.Status != TaskStatus.RanToCompletion)
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(TaskStatus.RanToCompletion, task.Status, customMessage, actualExpression: actualExpression).ToString());
    }

    /// <summary>
    /// Asserts that the task has already completed successfully (<see cref="TaskStatus.RanToCompletion"/>) and returns its result.
    /// </summary>
    public static T ShouldBeCompletedSuccessfully<T>(this Task<T> task,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
    {
        if (task.Status != TaskStatus.RanToCompletion)
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(TaskStatus.RanToCompletion, task.Status, customMessage, actualExpression: actualExpression).ToString());

        return task.Result;
    }
}
