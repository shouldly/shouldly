namespace Shouldly.Tests.Strings;

public class ShouldNotContainAny
{
    [Fact]
    public void ShouldNotContainAny_ShouldPass_WhenNoValuesPresent()
    {
        "my name is slim shady".ShouldNotContainAny(["marshall", "mathers"]);
    }

    [Fact]
    public void ShouldNotContainAny_ShouldThrow_WhenAnyValuePresent()
    {
        Verify.ShouldFail(
            () => "my name is slim shady".ShouldNotContainAny(["name", "foo"]),

            errorWithSource:
            """
            "my name is slim shady"
                should not contain any (case insensitive comparison)
            "name, foo"
                but did
            """,

            errorWithoutSource:
            """
            "my name is slim shady"
                should not contain any (case insensitive comparison)
            "name, foo"
                but did
            """);
    }

    [Fact]
    public void ShouldNotContainAny_WithCaseSensitive_ShouldFailIfCaseMatches()
    {
        Verify.ShouldFail(
            () => "Hello World".ShouldNotContainAny(["Hello"], Case.Sensitive),

            errorWithSource:
            """
            "Hello World"
                should not contain any
            "Hello"
                but did
            """,

            errorWithoutSource:
            """
            "Hello World"
                should not contain any
            "Hello"
                but did
            """);
    }

    [Fact]
    public void ShouldNotContainAny_WithCaseInsensitive_ShouldPassIfValuesAbsentIgnoringCase()
    {
        "Hello World".ShouldNotContainAny(["mars"], Case.Insensitive);
    }

    [Fact]
    public void ShouldNotContainAny_ShouldIncludeCustomMessage_WhenProvided()
    {
        Verify.ShouldFail(
            () => "my name is slim shady".ShouldNotContainAny(["name", "slim"], Case.Insensitive, "Custom error message"),

            errorWithSource:
            """
            "my name is slim shady"
                should not contain any (case insensitive comparison)
            "name, slim"
                but did

            Additional Info:
                Custom error message
            """,

            errorWithoutSource:
            """
            "my name is slim shady"
                should not contain any (case insensitive comparison)
            "name, slim"
                but did

            Additional Info:
                Custom error message
            """);
    }
}
