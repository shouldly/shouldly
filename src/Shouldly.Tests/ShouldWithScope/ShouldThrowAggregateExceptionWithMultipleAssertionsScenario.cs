using Shouldly.ShouldlyExtensionMethods;

namespace Shouldly.Tests.ShouldWithScope;

public class ShouldThrowAggregateExceptionWithMultipleAssertionsScenario
{
    [Fact]
    public void ShouldPass()
    {
        var actualInt = 5;
        var expectedInt = 6;
        var actualString = "hello";
        var expectedString = "world";

        var action = new Action(() =>
        {
            using (new AssertionScope())
            {
                // Use the generic extension method for any assertion.
                actualInt.ShouldWithScope(x => x.ShouldBe(expectedInt));
                actualString.ShouldWithScope(x => x.ShouldBe(expectedString));
            }
        });

        var ex = action.ShouldThrow<AggregateShouldlyAssertionException>();
        ex.ShouldBeOfType<AggregateShouldlyAssertionException>();
        ex.ShouldNotBe(null);
        ex.Errors.Count.ShouldBe(2);
    }
}