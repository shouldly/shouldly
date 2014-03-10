#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.Throw<InvalidOperationException>(() =>
            {
                var task = Task.Factory.StartNew(() => { }, 
                    CancellationToken.None, TaskCreationOptions.None,
                    TaskScheduler.Default);
                return task;
            });
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Should throw System.InvalidOperationException but does not"; }
        }

        protected override void ShouldPass()
        {
            var ex = Should.Throw<InvalidOperationException>(() =>
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