#if net40
using System;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class TaskThrowsDifferentExceptionScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var task = Task.Factory.StartNew(() => { throw new RankException(); });

            task.ShouldThrow<InvalidOperationException>("Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"`task` should throw System.InvalidOperationException but threw System.RankException
Additional Info:
Some additional context"; }
        }

        protected override void ShouldPass()
        {
            var task = Task.Factory.StartNew(() => { throw new InvalidOperationException(); });

            var ex = task.ShouldThrow<InvalidOperationException>();
            ex.ShouldNotBe(null);
            ex.ShouldBeOfType<InvalidOperationException>();
        }
    }
}
#endif