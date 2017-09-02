using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeSequenceEqualTo
{
    public class IntegerArrayScenario
    {

        [Fact]
        public void IntegerArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    new[] { 1, 2, 3, 4 }.ShouldBeSequenceEqualTo(new[] { 4, 3, 2, 1 }, "Some additional context"),

    errorWithSource:
    @"new[] { 1, 2, 3, 4 }
    should be
[4, 3, 2, 1]
    but was
[1, 2, 3, 4]
    difference
[*1*, *2*, *3*, *4*]

Additional Info:
    Some additional context",

    errorWithoutSource:
    @"[1, 2, 3, 4]
    should be
[4, 3, 2, 1]
    but was not
    difference
[*1*, *2*, *3*, *4*]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new[] { 1, 2, 3, 4 }.ShouldBeSequenceEqualTo(new[] { 1, 2, 3, 4 });
        }
    }
}