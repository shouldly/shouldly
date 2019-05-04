using System;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Shouldly.Tests.ShouldThrow
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

            var task = new Func<Task>(() =>
            {
                SynchronizationContext.Current.ShouldBe(null);

                throw new InvalidOperationException();
            });

            task.ShouldThrow<InvalidOperationException>();
        }

[Fact]
        public void DelegateShouldDropSynchronisationContext_ExceptionTypePassedIn()
        {
            // The await keyword will automatically capture synchronization context
            // Because shouldly uses .Wait() we cannot let continuations run on the sync context without a deadlock
            var synchronizationContext = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synchronizationContext);
            SynchronizationContext.Current.ShouldNotBe(null);

            var task = new Func<Task>(() =>
            {
                SynchronizationContext.Current.ShouldBe(null);

                throw new InvalidOperationException();
            });

            task.ShouldThrow(typeof(InvalidOperationException));
        }
    }
}
