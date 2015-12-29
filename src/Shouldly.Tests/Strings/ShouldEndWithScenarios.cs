using Xunit;

namespace Shouldly.Tests.Strings
{
    public class ShouldEndWithScenarios
    {
        [Fact]
        public void ShouldEndWithCaseInsensitiveShouldFail()
        {
            // ReSharper disable once RedundantArgumentDefaultValue
            var cheeseVar = "Cheese";
            Verify.ShouldFail(() =>
cheeseVar.ShouldEndWith("ze", "Some additional context", Case.Insensitive),

errorWithSource:
@"cheeseVar
    should end with
""ze""
    but was
""Cheese""

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Cheese""
    should end with
""ze""
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldEndWithCaseSensitiveShouldFail()
        {
            var cheeseVar = "Cheese";
            Verify.ShouldFail(() =>
cheeseVar.ShouldEndWith("Se", "Some additional context", Case.Sensitive),

errorWithSource:
@"cheeseVar
    should end with
""Se""
    but was
""Cheese""

Additional Info:
    Some additional context",

errorWithoutSource:
@"""Cheese""
    should end with
""Se""
    but did not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            "Cheese".ShouldEndWith("se");
            "Cheese".ShouldEndWith("SE");
            "Cheese".ShouldEndWith("Se", Case.Insensitive);
            "Cheese".ShouldEndWith("se", Case.Sensitive);
        }
    }
}