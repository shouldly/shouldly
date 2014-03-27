#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskExactExceptionScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.ThrowExact<NullReferenceException>(() =>
            {
                var task = Task.Factory.StartNew(() => { }, 
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);
                return task;
            });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Should throw exact System.NullReferenceException but does not"; }
        }

        protected override void ShouldPass()
        {
            var ex = Should.ThrowExact<InvalidOperationException>(() =>
            {
                var task = Task.Factory.StartNew(() => { throw new InvalidOperationException(); },
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);
                return task;
            });
            ex.ShouldNotBe(null);
            ex.ShouldBeOfType<InvalidOperationException>();
        }
    }
}
#endif