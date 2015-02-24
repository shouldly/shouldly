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
            new[] { a, b, c }.ShouldNotContain(o => o.GetType().FullName.Equals("System.Object"));
        }

        protected override string ChuckedAWobblyErrorMessage
        {
            get { return @"
    new[] { a, b, c }
        should not contain
    o.GetType().FullName.Equals(""System.Object"")
        but does"; }
            
        }

        protected override void ShouldPass()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            new[] { a, b, c }.ShouldNotContain(o => o.GetType().FullName.Equals(""));

        }
    }
}