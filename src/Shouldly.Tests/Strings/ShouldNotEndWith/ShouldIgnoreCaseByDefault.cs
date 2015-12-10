using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotEndWith
{
    public class ShouldIgnoreCaseByDefault
    {
        [Fact]
        public void ShouldIgnoreCaseByDefaultShouldFail()
        {
            Verify.ShouldFail(() =>
    "Cheese".ShouldNotEndWith("SE"),

errorWithSource:
@"""Cheese""
    should not end with
""SE""
    but was
""Cheese""",

errorWithoutSource:
@"""Cheese""
    should not end with
""SE""
    but was
""Cheese""");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldNotEndWith("ze");
        }
    }
}