#if net40
using System;
using System.Threading.Tasks;
using Xunit;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskWhichThrowsDirectlyScenario
    {
        [Fact]
        public void ShouldPass()
        {
            // ReSharper disable once RedundantDelegateCreation
            var task = new Func<Task>(() => { throw new InvalidOperationException(); });
            task.ShouldThrow<InvalidOperationException>();
        }
    }
}
#endif