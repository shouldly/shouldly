using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeBoolean
{
    public class FalseScenario
    {
        [Fact]
        public void FalseScenarioShouldFail()
        {
            const bool myValue = true;
            Verify.ShouldFail(() =>
myValue.ShouldBeFalse("Some additional context"),

errorWithSource:
@"myValue
    should be
False
    but was
True

Additional Info:
    Some additional context",

    errorWithoutSource:
@"True
    should be
False
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            false.ShouldBeFalse();
        }

        [Fact]
        public void NullableFalseScenarioShouldFail() {
            bool? myValue = true;
            Verify.ShouldFail(() =>
myValue.ShouldBeFalse("Some additional context"),

errorWithSource:
@"myValue
    should be
False
    but was
True

Additional Info:
    Some additional context",

errorWithoutSource:
@"True
    should be
False
    but was not

Additional Info:
    Some additional context");
        }

        [Fact]
        public void NullableShouldPass() {
            bool? value = false;
            value.ShouldBeFalse();
        }
    }
}