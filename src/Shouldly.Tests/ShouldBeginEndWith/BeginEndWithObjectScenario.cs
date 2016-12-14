using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeginEndWith
{
    public class BeginEndWithObjectScenario
    {

        [Fact]
        public void BeginShouldFail()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            var d = new object();
            var target = new[] { a, b, c };

            Verify.ShouldFail(() =>
target.ShouldBeginWith(d, "Some additional context"),

errorWithSource:
@"target
    should begin with
System.Object (000000)
    but was
[System.Object (000000), System.Object (000000), System.Object (000000)]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[System.Object (000000), System.Object (000000), System.Object (000000)]
    should begin with
System.Object (000000)
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void BeginShouldPass()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            var target = new[] { a, b, c };
            target.ShouldBeginWith(a);
        }
    }
}