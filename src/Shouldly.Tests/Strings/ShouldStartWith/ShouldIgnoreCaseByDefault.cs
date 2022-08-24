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
    but was not",

errorWithoutSource:
@"""Cheese""
    should start with
""Ce""
    but was not");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldStartWith("CH");
        }
    }
}