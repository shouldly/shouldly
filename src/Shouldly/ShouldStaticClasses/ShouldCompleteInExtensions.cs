using System.Runtime.ExceptionServices;

namespace Shouldly;

public static partial class Should
{
    /// <summary>
    /// Asserts that the given action completes within the specified timeout.
    /// </summary>
    /// <param name="action">The action to execute.</param>
    /// <param name="timeout">The maximum time allowed for the action to complete.</param>
    /// <param name="customMessage">Optional custom message to use if the assertion fails.</param>
    /// <exception cref="ShouldCompleteInException">Thrown when the action does not complete within the specified timeout.</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void CompleteIn(Action action, TimeSpan timeout, string? customMessage = null)
    {
        var actual = Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);
        CompleteIn(actual, timeout, customMessage, "Delegate");
    }

    /// <summary>
    /// Asserts that the given function completes within the specified timeout and returns its result.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the function.</typeparam>
    /// <param name="function">The function to execute.</param>
    /// <param name="timeout">The maximum time allowed for the function to complete.</param>
    /// <param name="customMessage">Optional custom message to use if the assertion fails.</param>
    /// <returns>The result of the function if it completes within the timeout.</returns>
    /// <exception cref="ShouldCompleteInException">Thrown when the function does not complete within the specified timeout.</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T CompleteIn<T>(Func<T> function, TimeSpan timeout, string? customMessage = null)
    {
        var actual = Task.Factory.StartNew(function, CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);
        CompleteIn(actual, timeout, customMessage, "Delegate");
        return actual.Result;
    }

    /// <summary>
    /// Asserts that the given asynchronous function completes within the specified timeout.
    /// </summary>
    /// <param name="actual">The asynchronous function to execute.</param>
    /// <param name="timeout">The maximum time allowed for the function to complete.</param>
    /// <param name="customMessage">Optional custom message to use if the assertion fails.</param>
    /// <exception cref="ShouldCompleteInException">Thrown when the function does not complete within the specified timeout.</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void CompleteIn(Func<Task> actual, TimeSpan timeout, string? customMessage = null)
    {
        CompleteIn(actual(), timeout, customMessage, "Task");
    }

    /// <summary>
    /// Asserts that the given asynchronous function completes within the specified timeout and returns its result.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the asynchronous function.</typeparam>
    /// <param name="actual">The asynchronous function to execute.</param>
    /// <param name="timeout">The maximum time allowed for the function to complete.</param>
    /// <param name="customMessage">Optional custom message to use if the assertion fails.</param>
    /// <returns>The result of the asynchronous function if it completes within the timeout.</returns>
    /// <exception cref="ShouldCompleteInException">Thrown when the function does not complete within the specified timeout.</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T CompleteIn<T>(Func<Task<T>> actual, TimeSpan timeout, string? customMessage = null)
    {
        var task = actual();
        CompleteIn(task, timeout, customMessage, "Task");
        return task.Result;
    }

    /// <summary>
    /// Asserts that the given task completes within the specified timeout.
    /// </summary>
    /// <param name="actual">The task to wait for completion.</param>
    /// <param name="timeout">The maximum time allowed for the task to complete.</param>
    /// <param name="customMessage">Optional custom message to use if the assertion fails.</param>
    /// <exception cref="ShouldCompleteInException">Thrown when the task does not complete within the specified timeout.</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void CompleteIn(Task actual, TimeSpan timeout, string? customMessage = null)
    {
        CompleteIn(actual, timeout, customMessage, "Task");
    }

    /// <summary>
    /// Asserts that the given task completes within the specified timeout and returns its result.
    /// </summary>
    /// <typeparam name="T">The type of the result returned by the task.</typeparam>
    /// <param name="actual">The task to wait for completion.</param>
    /// <param name="timeout">The maximum time allowed for the task to complete.</param>
    /// <param name="customMessage">Optional custom message to use if the assertion fails.</param>
    /// <returns>The result of the task if it completes within the timeout.</returns>
    /// <exception cref="ShouldCompleteInException">Thrown when the task does not complete within the specified timeout.</exception>
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static T CompleteIn<T>(Task<T> actual, TimeSpan timeout, string? customMessage = null)
    {
        CompleteIn(actual, timeout, customMessage, "Task");
        return actual.Result;
    }

    private static void CompleteIn(Task actual, TimeSpan timeout, string? customMessage, string what)
    {
        try
        {
            actual.TimeoutAfter(timeout).Wait();
        }
        catch (AggregateException ae)
        {
            var flattened = ae.Flatten();
            if (flattened.InnerExceptions.Count != 1)
                throw;

            var inner = flattened.InnerException!;
            // When exception is a timeout exception we can provide a better error, otherwise rethrow
            if (inner is ShouldlyTimeoutException exception)
            {
                var message = new CompleteInShouldlyMessage(what, timeout, customMessage).ToString();
                throw new ShouldCompleteInException(message, exception);
            }

            ExceptionDispatchInfo.Capture(inner).Throw();
        }
    }
}