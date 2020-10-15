using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotStartWith
{
    public class BasicScenario
    {
        [Fact]
        public void BasicScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    "Cheese".ShouldNotStartWith("Ch", customMessage: "Some additional context"),

errorWithSource:
@"""Cheese""
    should not start with
""Ch""
    but was

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Cheese""
    should not start with
""Ch""
    but was

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldNotStartWith("Ce");
        }
    }
}