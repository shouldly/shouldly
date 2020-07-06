using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldHaveSingleItem
{
    public class ArrayScenario
    {

        [Fact]
        public void ArrayScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
new[] { 1, 2 }.ShouldHaveSingleItem("Some additional context"),

errorWithSource:
@"new[] { 1, 2 }
    should have single item but had
2
    items and was
[1, 2]

Additional Info:
    Some additional context",

errorWithoutSource:
@"[1, 2]
    should have single item but had
2
    items

Additional Info:
    Some additional context");
        }

        [Fact]
        public void ShouldPass()
        {
            var result = new[] { 1 }.ShouldHaveSingleItem();
            result.ShouldBe(1);
        }
    }
}
