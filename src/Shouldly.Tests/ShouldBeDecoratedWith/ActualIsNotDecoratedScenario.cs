namespace Shouldly.Tests.ShouldBeDecoratedWith;

public class ActualIsNotDecoratedScenario
{
    [Fact]
    public void ActualIsNotDecoratedScenarioShouldFail()
    {
        var myThingType = typeof(MyThing);

        // ReSharper disable once ExpressionIsAlwaysNull
        Verify.ShouldFail(() =>
                myThingType.ShouldBeDecoratedWith<UseCultureAttribute>("Some additional context"),
            errorWithSource:
            """
            myThingType
                should be decorated with 
            "UseCultureAttribute"
                but does not

            Additional Info:
                Some additional context
            """,
            errorWithoutSource:
            """
            null
                should be decorated with 
            "UseCultureAttribute"
                but does not

            Additional Info:
                Some additional context
            """);
    }
}