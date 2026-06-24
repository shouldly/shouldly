using Shouldly.ShouldlyExtensionMethods;

namespace Shouldly.Tests.ShouldWithScope;

public class ShouldThrowAggregateExceptionWithSingleAssertionScenario
{
    [Fact]
    public void ShouldPass()
    {
        var actualString = "hello";
        var expectedString = "world";

        var action = new Action(() =>
        {
            using (new AssertionScope())
            {
                // Use the generic extension method for any assertion.
                actualString.ShouldWithScope(x => x.ShouldBe(expectedString));
            }
        });

        var ex = action.ShouldThrow<AggregateShouldlyAssertionException>();
        ex.ShouldBeOfType<AggregateShouldlyAssertionException>();
        ex.ShouldNotBe(null);
        ex.Errors.Count.ShouldBe(1);
    }
}