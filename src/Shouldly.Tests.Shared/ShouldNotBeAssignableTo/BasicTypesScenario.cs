using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldNotBeAssignableTo
{
    public class BasicTypesScenario
    {

        [Fact]
        public void BasicTypesScenarioShouldFail()
        {
            var two = 2;
            Verify.ShouldFail(() =>
two.ShouldNotBeAssignableTo<int>("Some additional context"),

errorWithSource:
@"two
    should not be assignable to
System.Int32
    but was
2",

errorWithoutSource:
@"2
    should not be assignable to
System.Int32
    but was");
        }

        [Fact]
        public void ShouldPass()
        {
            1.ShouldNotBeAssignableTo<string>();
        }
    }
}