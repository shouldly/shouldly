#if net40
using System;
using System.Threading;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldNotThrow
{
    public class TaskOfTScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var task = Task.Factory.StartNew<string>(() => { throw new RankException(); },
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);
            Should.NotThrow(task, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"Should not throw System.RankException but does
Additional Info:
Some additional context";
            }
        }

        protected override void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => "foo",
                CancellationToken.None, TaskCreationOptions.None,
                TaskScheduler.Default);
            var result = Should.NotThrow(task);
            result.ShouldBe("foo");
        }
    }
}
#endif