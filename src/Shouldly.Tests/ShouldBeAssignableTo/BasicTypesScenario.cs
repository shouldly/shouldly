using Shouldly.Tests.Strings;
using Xunit;

namespace Shouldly.Tests.ShouldBeAssignableTo
{
    public class BasicTypesScenario
    {

    [Fact]
    public void BasicTypesScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
2.ShouldBeAssignableTo<double>("Some additional context"),

errorWithSource:
@"2
    should be assignable to
System.Double
    but was
System.Int32

Additional Info:
    Some additional context",

errorWithoutSource:
@"System.Int32
    should be assignable to
System.Double
    but was not

Additional Info:
    Some additional context");
    }

    [Fact]
    public void ShouldPass()
    {
        1.ShouldBeAssignableTo<int>();
    }
}
}