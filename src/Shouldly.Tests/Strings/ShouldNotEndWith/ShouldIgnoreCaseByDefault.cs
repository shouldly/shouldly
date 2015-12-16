using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotEndWith
{
    public class ShouldIgnoreCaseByDefault
    {
        [Fact]
        public void ShouldIgnoreCaseByDefaultShouldFail()
        {
            Verify.ShouldFail(() =>
    "Cheese".ShouldNotEndWith("SE", "Some additional context"),

errorWithSource:
@"""Cheese""
    should not end with
""SE""
    but was

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Cheese""
    should not end with
""SE""
    but was

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