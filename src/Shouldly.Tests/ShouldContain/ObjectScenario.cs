using Shouldly.Tests.TestHelpers;
using System;

namespace Shouldly.Tests.ShouldContain
{
    public class ObjectScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            var d = new Object();
            new[] { a, b, c }.ShouldContain(d);
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"
    new[] { a, b, c }
        should contain
    System.Object
        but was
    [System.Object, System.Object, System.Object]"; }
            
        }

        protected override void ShouldPass()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            new[] { a, b, c }.ShouldContain(b);
        }
    }
}