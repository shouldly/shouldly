using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotContain
{
    public class ObjectScenario
    {

        [Fact]
        public void ObjectScenarioShouldFail()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            var target = new[] { a, b, c };
            Verify.ShouldFail(() =>
target.ShouldNotContain(c, "Some additional context"),

errorWithSource:
@"target
    should not contain
System.Object (000000)
    but was actually
[System.Object (000000), System.Object (000000), System.Object (000000)]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[System.Object (000000), System.Object (000000), System.Object (000000)]
    should not contain
System.Object (000000)
    but did

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
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