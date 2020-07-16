using System;
using System.Threading.Tasks;
using System.Threading;
using Xunit;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class NestedAwaitsDoNotDeadlockScenario
    {
        [Fact]
        public void DelegateShouldDropSynchronisationContext()
        {
            // The await keyword will automatically capture synchronization context
            // Because shouldly uses .Wait() we cannot let continuations run on the sync context without a deadlock
            var synchronizationContext = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synchronizationContext);
            SynchronizationContext.Current.ShouldNotBe(null);

            var syncFunc1 = new Func<Task<object?>>(() =>
            {
                SynchronizationContext.Current.ShouldBe(null);

                var taskCompletionSource = new TaskCompletionSource<object?>();
                taskCompletionSource.SetResult(null);
                return taskCompletionSource.Task;
            });
            syncFunc1.ShouldNotThrow();

            var syncFunc2 = new Func<Task>(() =>
            {
                SynchronizationContext.Current.ShouldBe(null);

                var taskCompletionSource = new TaskCompletionSource<object?>();
                taskCompletionSource.SetResult(null);
                return taskCompletionSource.Task;
            });

            syncFunc2.ShouldNotThrow();
        }
    }
}