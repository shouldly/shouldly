using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shouldly
{
    // http://blogs.msdn.com/b/pfxteam/archive/2011/11/10/10235834.aspx
    public static class TaskExtensions
    {
        internal struct VoidTypeStruct { }

        internal static void MarshalTaskResults<TResult>(Task source, TaskCompletionSource<TResult> proxy)
        {
            switch (source.Status)
            {
                case TaskStatus.Faulted:
                    proxy.TrySetException(source.Exception);
                    break;
                case TaskStatus.Canceled:
                    proxy.TrySetCanceled();
                    break;
                case TaskStatus.RanToCompletion:
                    Task<TResult> castedSource = source as Task<TResult>;
                    proxy.TrySetResult(
                        castedSource == null
                            ? default(TResult)
                            : // source is a Task
                            castedSource.Result); // source is a Task<TResult>
                    break;
            }
        }

        public static Task TimeoutAfter(this Task task, TimeSpan timeout)
        {
            // tcs.Task will be returned as a proxy to the caller
            TaskCompletionSource<VoidTypeStruct> tcs =
                new TaskCompletionSource<VoidTypeStruct>();

            // Set up a timer to complete after the specified timeout period
            Timer timer = new Timer(_ =>
            {
                // Fault our proxy Task with a TimeoutException
                tcs.TrySetException(new TimeoutException());
            }, null, (long)timeout.TotalMilliseconds, Timeout.Infinite);

            // Wire up the logic for what happens when source task completes
            task.ContinueWith(antecedent =>
            {
                timer.Dispose(); // Cancel the timer
                MarshalTaskResults(antecedent, tcs); // Marshal results to proxy
            }, CancellationToken.None,
                TaskContinuationOptions.ExecuteSynchronously,
                TaskScheduler.Default);

            return tcs.Task;
        }
    }
}