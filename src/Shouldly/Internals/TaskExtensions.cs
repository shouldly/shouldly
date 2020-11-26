using System;
using System.Threading;
using System.Threading.Tasks;

namespace Shouldly
{
    // http://blogs.msdn.com/b/pfxteam/archive/2011/11/10/10235834.aspx
    static class TaskExtensions
    {
        private struct VoidTypeStruct { }

        private static void MarshalTaskResults<TResult>(Task source, TaskCompletionSource<TResult?> proxy)
        {
            switch (source.Status)
            {
                case TaskStatus.Faulted:
                    proxy.TrySetException(source.Exception!);
                    break;
                case TaskStatus.Canceled:
                    proxy.TrySetCanceled();
                    break;
                case TaskStatus.RanToCompletion:
                    proxy.TrySetResult(!(source is Task<TResult> castedSource)
                        ? default // source is a Task
                        : castedSource.Result); // source is a Task<TResult>
                    break;
            }
        }

        public static Task TimeoutAfter(this Task task, TimeSpan timeout)
        {
            // tcs.Task will be returned as a proxy to the caller
            var tcs = new TaskCompletionSource<VoidTypeStruct>();

            // Set up a timer to complete after the specified timeout period
            var timer = new Timer(_ =>
            {
                // Fault our proxy Task with a ShouldlyTimeoutException
                tcs.TrySetException(new ShouldlyTimeoutException());
            }, null, (int)timeout.TotalMilliseconds, Timeout.Infinite);

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