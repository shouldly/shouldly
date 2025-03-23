namespace Shouldly.Tests.ShouldBeBoolean;

public class TrueScenario
{
    [Fact]
    public void TrueScenarioShouldFail()
    {
        const bool myValue = false;
        Verify.ShouldFail(() =>
                myValue.ShouldBeTrue("Some additional context"),

            errorWithSource:
            """
            myValue
                should be
            True
                but was
            False

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            False
                should be
            True
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void NullableBooleanNullShouldFail()
    {
        bool? myValue = null;

        Verify.ShouldFail(() =>
                myValue.ShouldBeTrue("Some additional context"),

            errorWithSource:
            """
            myValue
                should be
            True
                but was
            null

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            null
                should be
            True
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void NullableBooleanFalseShouldFail()
    {
        bool? myValue = false;

        Verify.ShouldFail(() =>
                myValue.ShouldBeTrue("Some additional context"),

            errorWithSource:
            """
            myValue
                should be
            True
                but was
            False

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            False
                should be
            True
                but was not

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPass()
    {
        true.ShouldBeTrue();
    }
}