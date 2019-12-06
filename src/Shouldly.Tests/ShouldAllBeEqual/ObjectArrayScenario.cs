using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldAllBeEqual
{
    public class ObjectArrayScenario
    {

        [Fact]
        public void ObjectArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new object[] { 1, 2, 3, 4, 2 }.ShouldAllBeEqual("Some additional context"),

errorWithSource:
@"new object[] { 1, 2, 3, 4, 2 }
    should have all items equal but had:
1 occurrence of [1]
2 occurrences of [2]
1 occurrence of [3]
1 occurrence of [4]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3, 4, 2]
  should have all items equal but had:
1 occurrence of [1]
2 occurrences of [2]
1 occurrence of [3]
1 occurrence of [4]

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new object[] { 1, 1, 1, 1, 1 }.ShouldAllBeEqual();
        }
    }
}

