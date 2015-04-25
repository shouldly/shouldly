using Shouldly.Tests.TestHelpers;
using System;

namespace Shouldly.Tests.ShouldNotContain
{
    public class ObjectScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            var target = new[] {a, b, c};
            target.ShouldNotContain(c, "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get
            {
                return @"
    target
        should not contain
    System.Object
        but was actually
    [System.Object, System.Object, System.Object]
    Additional Info:
    Some additional context";
            }

        }

        protected override void ShouldPass()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            var d = new Object();
            var target = new[] { a, b, c };
            target.ShouldNotContain(d);
        }
    }
}