using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotEndWith
{
    public class ShouldIgnoreCaseByDefault
    {
        [Fact]
        public void ShouldIgnoreCaseByDefaultShouldFail()
        {
            var str = "Cheese";
            Verify.ShouldFail(() =>
    str.ShouldNotEndWith("SE", "Some additional context"),

errorWithSource:
@"str
    should not end with
""SE""
    but was
""Cheese""

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Cheese""
    should not end with
""SE""
    but did

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldNotEndWith("ze");
        }
    }
}