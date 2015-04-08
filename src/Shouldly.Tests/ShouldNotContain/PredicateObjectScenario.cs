using Shouldly.Tests.TestHelpers;
using System;

namespace Shouldly.Tests.ShouldNotContain
{
    public class PredicateObjectScenario : ShouldlyShouldTestScenario
    {
        protected override void ShouldThrowAWobbly()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            new[] {a, b, c}.ShouldNotContain(o => o.GetType().FullName.Equals("System.Object"),
                () => "Some additional context");
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"
    new[] { a, b, c }
        should not contain an element satisfying the condition
    o.GetType().FullName.Equals(""System.Object"")
        but does
    Additional Info:
    Some additional context"; }
        }

        protected override void ShouldPass()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            new[] {a, b, c}.ShouldNotContain(o => o.GetType().FullName.Equals(""), () => "Some additional context");

        }
    }
}