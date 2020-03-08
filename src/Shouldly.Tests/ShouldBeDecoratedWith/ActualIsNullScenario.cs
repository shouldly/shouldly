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
@"Shouldly.Tests.TestHelpers.MyThing
    should be decorated with
Shouldly.Tests.TestHelpers.UseCultureAttribute
    but was
not

Additional Info:
    Some additional context",

errorWithoutSource:
@"null
    should be of type
Shouldly.Tests.TestHelpers.MyBase
    but was not

Additional Info:
    Some additional context");
        }
    }
}