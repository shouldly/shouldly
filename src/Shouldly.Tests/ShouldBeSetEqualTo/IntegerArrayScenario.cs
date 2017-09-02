using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeSetEqualTo
{
    public class IntegerArrayScenario
    {

        [Fact]
        public void IntegerArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    new[] { 99, 2, 3, 5 }.ShouldBeSetEqualTo(new[] { 1, 2, 3, 4 }, "Some additional context"),

    errorWithSource:
    @"new[] { 99, 2, 3, 5 }
    should be (ignoring order)
[1, 2, 3, 4]
    but
new[] { 99, 2, 3, 5 }
    is missing
[1, 4]
    and
[1, 2, 3, 4]
    is missing
[99, 5]

Additional Info:
    Some additional context",

    errorWithoutSource:
    @"[99, 2, 3, 5]
    should be (ignoring order)
[1, 2, 3, 4]
    but
[99, 2, 3, 5]
    is missing
[1, 4]
    and
[1, 2, 3, 4]
    is missing
[99, 5]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new[] { 1, 2, 3, 4 }.ShouldBeSetEqualTo(new[] { 4, 3, 2, 1 });
        }
    }
}