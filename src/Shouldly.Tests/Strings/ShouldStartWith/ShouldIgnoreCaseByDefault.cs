using Xunit;

namespace Shouldly.Tests.Strings.ShouldStartWith
{
    public class ShouldIgnoreCaseByDefault
    {
        [Fact]
        public void ShouldIgnoreCaseByDefaultShouldFail()
        {
            Verify.ShouldFail(() =>
    "Cheese".ShouldStartWith("Ce"),

errorWithSource:
@"""Cheese"" 
    should start with
""Ce""
    but was
""Cheese""",

errorWithoutSource:
@"""Cheese"" 
    should start with
""Ce""
    but was
""Cheese""");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldStartWith("CH");
        }
    }
}