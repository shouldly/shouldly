namespace Shouldly;

public static partial class Should
{
    /*** CompleteIn(Action) ***/
    public static void CompleteIn(Action action, TimeSpan timeout, string? customMessage = null)
    {
        var actual = Task.Factory.StartNew(
            action,
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskScheduler.Default);
        CompleteInAsync(actual, timeout, customMessage, "Delegate")
            .GetAwaiter().GetResult();
    }

    /*** CompleteIn(Func<T>) ***/
    public static T CompleteIn<T>(Func<T> function, TimeSpan timeout, string? customMessage = null)
    {
        var actual = Task.Factory.StartNew(function, CancellationToken.None, TaskCreationOptions.None,
            TaskScheduler.Default);
        return CompleteInAsync(actual, timeout, customMessage, "Delegate")
            .GetAwaiter().GetResult();
    }

    /*** CompleteIn(Func<Task>) ***/
    public static void CompleteIn(Func<Task> actual, TimeSpan timeout, string? customMessage = null)
    {
        CompleteInAsync(actual(), timeout, customMessage, "Task")
            .GetAwaiter().GetResult();
    }

    /*** CompleteIn(Func<Task<T>>) ***/
    public static T CompleteIn<T>(Func<Task<T>> actual, TimeSpan timeout, string? customMessage = null)
    {
        var task = actual();
        return CompleteInAsync(task, timeout, customMessage, "Task")
            .GetAwaiter().GetResult();
    }

    /*** CompleteIn(Task<T>) ***/
    public static void CompleteIn(Task actual, TimeSpan timeout, string? customMessage = null)
    {
        CompleteInAsync(actual, timeout, customMessage, "Task")
            .GetAwaiter().GetResult();
    }

    /*** CompleteIn(Task<T>) ***/
    public static T CompleteIn<T>(Task<T> actual, TimeSpan timeout, string? customMessage = null)
    {
        return CompleteInAsync(actual, timeout, customMessage, "Task")
            .GetAwaiter().GetResult();
    }

    private static async Task CompleteInAsync(Task actual, TimeSpan timeout, string? customMessage, string what)
    {
        var winner = await Task.WhenAny(actual, Task.Delay(timeout));
        if (winner == actual)
        {
            switch (actual.Status)
            {
                case TaskStatus.Faulted:
                    throw BuildInnerException(actual);
                case TaskStatus.Canceled:
                    throw new TaskCanceledException();
                case TaskStatus.RanToCompletion:
                    return;
                default:
                    throw new("Unreachable");
            }
        }

        throw BuildCompleteInException(timeout, customMessage, what);
    }

    static async Task<T> CompleteInAsync<T>(Task<T> actual, TimeSpan timeout, string? customMessage, string what)
    {
        var winner = await Task.WhenAny(actual, Task.Delay(timeout));
        if (winner == actual)
        {
            switch (actual.Status)
            {
                case TaskStatus.Faulted:
                    throw BuildInnerException(actual);
                case TaskStatus.Canceled:
                    throw new TaskCanceledException();
                case TaskStatus.RanToCompletion:
                    return actual.Result;
                default:
                    throw new("Unreachable");
            }
        }

        throw BuildCompleteInException(timeout, customMessage, what);
    }

    static Exception BuildInnerException(Task actual)
    {
        var aggregateException = actual.Exception!;
        var inner = aggregateException.InnerExceptions[0];
        PreserveStackTrace(inner);
        return inner;
    }

    static ShouldCompleteInException BuildCompleteInException(TimeSpan timeout, string? customMessage, string what)
    {
        var message = new CompleteInShouldlyMessage(what, timeout, customMessage).ToString();
        return new ShouldCompleteInException(message, null);
    }

    private static void PreserveStackTrace(Exception exception)
    {
        // TODO Need to sort this out for core
        var preserveStackTrace = typeof(Exception).GetMethod("InternalPreserveStackTrace",
            BindingFlags.Instance | BindingFlags.NonPublic);

        preserveStackTrace?.Invoke(exception, null);
    }
}