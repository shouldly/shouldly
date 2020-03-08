using Shouldly.Tests.Strings;
using Shouldly.Tests.TestHelpers;
using Xunit;

namespace Shouldly.Tests.ShouldBeDecoratedWith
{
    public class ActualIsNullScenario
    {
        [Fact]
        public void ActualIsNullScenarioShouldFail()
        {
            // ReSharper disable once ExpressionIsAlwaysNull
            Verify.ShouldFail(() =>
typeof(MyThing).ShouldBeDecoratedWith<UseCultureAttribute>("Some additional context"),
errorWithSource:
@"typeof(MyThing)
    should be decorated with 
""UseCultureAttribute""
    but does not

Additional Info:
    Some additional context",
errorWithoutSource:
@"null
    should be decorated with 
""UseCultureAttribute""
    but does not

Additional Info:
    Some additional context");
        }
    }
}