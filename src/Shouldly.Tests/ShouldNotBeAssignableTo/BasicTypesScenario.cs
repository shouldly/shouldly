using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotBeAssignableTo
{
    public class BasicTypesScenario
    {

        [Fact]
        public void BasicTypesScenarioShouldFail()
        {
            Verify.ShouldFail(() =>
2.ShouldNotBeAssignableTo<int>("Some additional context"),

errorWithSource:
@"2 should not be assignable to System.Int32
    but was
2",

errorWithoutSource:
@"2 should not be assignable to System.Int32
    but was
2");
        }

        [Fact]
        public void ShouldPass()
        {
            1.ShouldNotBeAssignableTo<string>();
        }
    }
}