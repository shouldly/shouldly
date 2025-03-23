namespace Shouldly.Tests.ShouldBeNull;

public class NullScenario
{
    [Fact]
    public void ShouldFailForNonNullReference()
    {
        var myNullRef = "Hello World";
        Verify.ShouldFail(() =>
                myNullRef.ShouldBeNull("Some additional context"),

            errorWithSource:
            """
            myNullRef
                should be null but was
            "Hello World"

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            "Hello World"
                should be null but was

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPassForNullReference()
    {
        ((string?)null).ShouldBeNull();
    }

    [Fact]
    public void ShouldFailForSystemNullableWithValue()
    {
        var myNullRef = (int?)0;
        Verify.ShouldFail(() =>
                myNullRef.ShouldBeNull("Some additional context"),

            errorWithSource:
            """
            myNullRef
                should be null but was
            0

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            0
                should be null but was

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPassForSystemNullableWithoutValue()
    {
        ((int?)null).ShouldBeNull();
    }
}