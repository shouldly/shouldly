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
            var myThingType = typeof(MyThing);

            // ReSharper disable once ExpressionIsAlwaysNull
            Verify.ShouldFail(() =>
myThingType.ShouldBeDecoratedWith<UseCultureAttribute>("Some additional context"),
errorWithSource:
@"myThingType
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