#if net40
using System;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskWhichThrowsDirectlyScenario
    {
        [Test]
        public void ShouldPass()
        {
            // ReSharper disable once RedundantDelegateCreation
            Should.Throw<InvalidOperationException>(new Func<Task>(() => { throw new InvalidOperationException(); }));
        }
    }
}
#endif