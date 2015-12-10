using Xunit;

namespace Shouldly.Tests.Strings.ShouldNotEndWith
{
    public class BasicScenario
    {
        [Fact]
        public void BasicScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
"Cheese".ShouldNotEndWith("se", "Some additional context"),

errorWithSource:
@"""Cheese"" should not end with ""se""
    but was
""Cheese""

Additional Info:
Some additional context",

errorWithoutSource:
@"""Cheese"" should not end with ""se""
    but was
""Cheese""

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