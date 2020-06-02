using System;
using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldStartEndWith
{
    public class StartEndWithObjectScenario
    {

        [Fact]
        public void StartShouldFail()
        {
            var a = new object();
            var b = new object();
            var c = new object();
            var d = new object();
            var target = new[] { a, b, c };

            Verify.ShouldFail(() =>
target.ShouldStartWith(d, "Some additional context"),

errorWithSource:
@"target
    should start with
System.Object (000000)
    but was
[System.Object (000000), System.Object (000000), System.Object (000000)]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[System.Object (000000), System.Object (000000), System.Object (000000)]
    should start with
System.Object (000000)
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void StartShouldPass()
        {
            var a = new Object();
            var b = new Object();
            var c = new Object();
            var target = new[] { a, b, c };
            target.ShouldStartWith(a);
        }
    }
}