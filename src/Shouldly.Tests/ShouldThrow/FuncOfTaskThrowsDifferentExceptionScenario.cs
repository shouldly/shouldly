#if net40
using System;
using System.Threading.Tasks;
using Shouldly.Tests.TestHelpers;

namespace Shouldly.Tests.ShouldThrow
{
    public class FuncOfTaskThrowsDifferentExceptionScenario : ShouldlyShouldFailureTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            Should.Throw<InvalidOperationException>(() => Task.Factory.StartNew(() => { throw new RankException(); }));
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return "Should throw System.InvalidOperationException but was System.RankException"; }
        }
    }
}
#endif