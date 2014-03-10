#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Shouldly.Tests.ShouldThrow
{
    public class NestedAwaitsDoNotDeadlockScenario
    {
        [Test]
        public void DelegateShouldDropSynchronisationContext()
        {
            // The await keyword will automatically capture synchronisation context
            // Because shouldly uses .Wait() we cannot let continuations run on the sync context without a deadlock
            var synchronizationContext = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(synchronizationContext);
            SynchronizationContext.Current.ShouldNotBe(null);

            // ReSharper disable once RedundantDelegateCreation
            Should.Throw<InvalidOperationException>(new Func<Task>(() =>
            {
                SynchronizationContext.Current.ShouldBe(null);

                throw new InvalidOperationException();
            }));
        }
    }
}
#endif