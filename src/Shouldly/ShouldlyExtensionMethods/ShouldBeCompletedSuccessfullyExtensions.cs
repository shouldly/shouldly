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
    /// This checks the task's current status and does not wait for it to complete;
    /// use <see cref="Should.CompleteIn(Task, TimeSpan, string, string)"/> to wait.
    /// </summary>
    /// <param name="task">The task to check.</param>
    /// <param name="customMessage">Optional custom message to use if the assertion fails.</param>
    /// <param name="actualExpression">The source-level expression of the task argument captured at the call site via <see cref="CallerArgumentExpressionAttribute"/>.</param>
    public static void ShouldBeCompletedSuccessfully(this Task task,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
    {
        if (task is null)
            throw new ShouldAssertException(new ActualShouldlyMessage(null, customMessage, actualExpression: actualExpression).ToString());

        // Read the status once, so a task completing concurrently can't contradict the failure message.
        var status = task.Status;
        if (status != TaskStatus.RanToCompletion)
        {
            var snapshot = new MessageGenerators.TaskStatusSnapshot(status, status == TaskStatus.Faulted ? task.Exception : null);
            throw new ShouldAssertException(new ActualShouldlyMessage(snapshot, customMessage, actualExpression: actualExpression).ToString());
        }
    }

    /// <summary>
    /// Asserts that the task has already completed successfully (<see cref="TaskStatus.RanToCompletion"/>) and returns its result.
    /// This checks the task's current status and does not wait for it to complete;
    /// use <see cref="Should.CompleteIn{T}(Task{T}, TimeSpan, string, string)"/> to wait.
    /// </summary>
    /// <typeparam name="T">The type of the task's result.</typeparam>
    /// <param name="task">The task to check.</param>
    /// <param name="customMessage">Optional custom message to use if the assertion fails.</param>
    /// <param name="actualExpression">The source-level expression of the task argument captured at the call site via <see cref="CallerArgumentExpressionAttribute"/>.</param>
    /// <returns>The task's result.</returns>
    public static T ShouldBeCompletedSuccessfully<T>(this Task<T> task,
        string? customMessage = null,
        [CallerArgumentExpression(nameof(task))] string? actualExpression = null)
    {
        ((Task)task).ShouldBeCompletedSuccessfully(customMessage, actualExpression);

        return task.Result;
    }
}
