namespace Shouldly.Tests.ShouldBeEquivalentTo;

public class StringScenario
{
    [Fact]
    public void ShouldFailWhenStringIsDifferent()
    {
        const string subject = "Hello";
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo("Goodbye", "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            subject [System.String]
            
                Expected value to be
            "Goodbye"
                but was
            "Hello"

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.String]
            
                Expected value to be
            "Goodbye"
                but was
            "Hello"

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldFailWhenCaseIsDifferent()
    {
        const string subject = "Hello";
        Verify.ShouldFail(() =>
                subject.ShouldBeEquivalentTo("HELLO", "Some additional context"),

            errorWithSource:
            """
            Comparing object equivalence, at path:
            subject [System.String]
            
                Expected value to be
            "HELLO"
                but was
            "Hello"

            Additional Info:
                Some additional context
            """,

            errorWithoutSource:
            """
            Comparing object equivalence, at path:
            <root> [System.String]
            
                Expected value to be
            "HELLO"
                but was
            "Hello"

            Additional Info:
                Some additional context
            """);
    }

    [Fact]
    public void ShouldPassWhenCaseMatches()
    {
        const string subject = "Hello";
        subject.ShouldBeEquivalentTo("Hello");
    }
}