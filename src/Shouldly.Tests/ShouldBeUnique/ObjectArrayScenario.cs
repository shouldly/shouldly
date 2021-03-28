using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeUnique
{
    public class ObjectArrayScenario
    {
        [Fact]
        public void ObjectArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new object[] { 1, 2, 3, 4, 2 }.ShouldBeUnique("Some additional context"),

errorWithSource:
@"new object[] { 1, 2, 3, 4, 2 }
    should be unique but
[2]
    was duplicated

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2, 3, 4, 2]
    should be unique but
[2]
    was duplicated

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            new object[] { 1, 2, 3, 4, 7 }.ShouldBeUnique();
        }
    }
}

