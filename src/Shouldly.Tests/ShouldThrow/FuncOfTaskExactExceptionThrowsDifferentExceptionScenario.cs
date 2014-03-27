#if net40
using System;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskExactExceptionThrowsDifferentExceptionScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.ThrowExact<SystemException>(() => Task.Factory.StartNew(() => { throw new InvalidOperationException(); }));
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Should throw exact System.SystemException but was System.InvalidOperationException"; }
        }
    }
}
#endif