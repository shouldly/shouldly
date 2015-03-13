using Shouldly.Tests.TestHelpers;
using System;

namespace Shouldly.Tests.ShouldContain
{
    public class PredicateObjectScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            new[] { a, b, c }.ShouldContain(o => o.GetType().FullName.Equals(""));
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"
    new[] { a, b, c }
        should contain
    o.GetType().FullName.Equals("""")
        but does not"; }
            
        }

        protected override void ShouldPass()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            new[] { a, b, c }.ShouldContain(o => o.GetType().FullName.Equals("System.Object"));

        }
    }
}