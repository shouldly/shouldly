using Xunit;

namespace Shouldly.Tests.Strings.ShouldBeEmpty
{
    public class ShouldBeEmptyBasicScenario
    {
        [Fact]
        public void ShouldBeEmptyBasicScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
    "a".ShouldBeEmpty(),

    errorWithSource:
    @"""a""
    should be empty but was
""a""",

    errorWithoutSource:
    @"""a""
    should be empty but was not empty");
        }

        [Fact]
        public void ShouldPass()
        {
            "".ShouldBeEmpty();
        }
    }
}