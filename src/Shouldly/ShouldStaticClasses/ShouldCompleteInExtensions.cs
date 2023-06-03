using System.Reflection;

namespace Shouldly;

public static partial class Should
{
    /*** CompleteIn(Action) ***/
    public static void CompleteIn(Action action, TimeSpan timeout, string? customMessage = null)
    {
        var actual = Task.Factory.StartNew(action, CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);
        CompleteIn(actual, timeout, customMessage, "Delegate");
    }

    /*** CompleteIn(Func<T>) ***/
    public static T CompleteIn<T>(Func<T> function, TimeSpan timeout, string? customMessage = null)
    {
        var actual = Task.Factory.StartNew(function, CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);
        CompleteIn(actual, timeout, customMessage, "Delegate");
        return actual.Result;
    }

    /*** CompleteIn(Func<Task>) ***/
    public static void CompleteIn(Func<Task> actual, TimeSpan timeout, string? customMessage = null)
    {
        CompleteIn(actual(), timeout, customMessage, "Task");
    }

    /*** CompleteIn(Func<Task<T>>) ***/
    public static T CompleteIn<T>(Func<Task<T>> actual, TimeSpan timeout, string? customMessage = null)
    {
        var task = actual();
        CompleteIn(task, timeout, customMessage, "Task");
        return task.Result;
    }

    /*** CompleteIn(Func<Task>) ***/
    public static void CompleteIn(Func<CancellationToken, Task> actual, TimeSpan timeout, string? customMessage = null)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        CompleteIn(actual(cancellationTokenSource.Token), timeout, customMessage, "Task", cancellationTokenSource);
    }

    /*** CompleteIn(Func<Task<T>>) ***/
    public static T CompleteIn<T>(Func<CancellationToken, Task<T>> actual, TimeSpan timeout, string? customMessage = null)
    {
        var cancellationTokenSource = new CancellationTokenSource();
        Task<T> task = actual(cancellationTokenSource.Token);
        CompleteIn(task, timeout, customMessage, "Task", cancellationTokenSource);
        return task.Result;
    }

    /*** CompleteIn(Task<T>) ***/
    public static void CompleteIn(Task actual, TimeSpan timeout, string? customMessage = null)
    {
        CompleteIn(actual, timeout, customMessage, "Task");
    }

    /*** CompleteIn(Task<T>) ***/
    public static T CompleteIn<T>(Task<T> actual, TimeSpan timeout, string? customMessage = null)
    {
        CompleteIn(actual, timeout, customMessage, "Task");
        return actual.Result;
    }

    private static void CompleteIn(Task actual, TimeSpan timeout, string? customMessage, string what, CancellationTokenSource? cancellationTokenSource = null)
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
                // Cancel the task first if a cancellation token is provided
                if (cancellationTokenSource != null)
                {
                    cancellationTokenSource.Cancel();
                    try
                    {
                        actual.Wait();
                    }
                    catch (AggregateException aggregateException)
                    {
                        if (aggregateException.InnerExceptions.Count != 1 ||
                            aggregateException.InnerException is not OperationCanceledException canceledException ||
                            canceledException.CancellationToken != cancellationTokenSource.Token)
                        {
                            throw;
                        }
                    }
                }

                var message = new CompleteInShouldlyMessage(what, timeout, customMessage).ToString();
                throw new ShouldCompleteInException(message, exception);
            }

            PreserveStackTrace(inner);
            throw inner;
        }
    }

    private static void PreserveStackTrace(Exception exception)
    {
        // TODO Need to sort this out for core
        var preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace",
            BindingFlags.Instance | BindingFlags.NonPublic);

        preserveStackTrace?.Invoke(exception, null);
    }
}