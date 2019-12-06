using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldAllBeEqual
{
    public class IntegerArrayScenario
    {
        [Fact]
        public void IntegerArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new[] { 1, 2, 2 }.ShouldAllBeEqual("Some additional context"),

errorWithSource:
@"new[] { 1, 2, 2 }
  should have all items equal but had:
1 occurrence of [1]
2 occurrences of [2]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 2]
  should have all items equal but had:
1 occurrence of [1]
2 occurrences of [2]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new[] { 1, 1, 1 }.ShouldAllBeEqual();
        }
    }
}

