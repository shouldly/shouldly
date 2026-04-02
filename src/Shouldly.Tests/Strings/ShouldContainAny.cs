namespace Shouldly.Tests.Strings;

public class ShouldContainAny
{
    [Fact]
    public void ShouldContainAny_ShouldPass_WhenAtLeastOneValuePresent()
    {
        "my name is slim shady".ShouldContainAny(["name", "slim", "eminem"]);
    }

    [Fact]
    public void ShouldContainAny_ShouldThrow_WhenNoValuesPresent()
    {
        Verify.ShouldFail(
            () => "my name is slim shady".ShouldContainAny(["marshall", "mathers", "eminem"]),

            errorWithSource:
            """
            "my name is slim shady"
                should contain any (case insensitive comparison)
            "marshall, mathers, eminem"
                but did not
            """,

            errorWithoutSource:
            """
            "my name is slim shady"
                should contain any (case insensitive comparison)
            "marshall, mathers, eminem"
                but did not
            """);
    }

    [Fact]
    public void ShouldContainAny_WithCaseSensitive_ShouldFailIfCaseDoesNotMatch()
    {
        Verify.ShouldFail(
            () => "Hello World".ShouldContainAny(["hello"], Case.Sensitive),

            errorWithSource:
            """
            "Hello World"
                should contain any
            "hello"
                but did not
            """,

            errorWithoutSource:
            """
            "Hello World"
                should contain any
            "hello"
                but did not
            """);
    }

    [Fact]
    public void ShouldContainAny_WithCaseInsensitive_ShouldPassIfCaseDoesNotMatch()
    {
        "Hello World".ShouldContainAny(["hello"], Case.Insensitive);
    }

    [Fact]
    public void ShouldContainAny_ShouldIncludeCustomMessage_WhenProvided()
    {
        Verify.ShouldFail(
            () => "my name is slim shady".ShouldContainAny(
                ["marshall", "mathers", "eminem"], Case.Insensitive, "Custom error message"),

            errorWithSource:
            """
            "my name is slim shady"
                should contain any (case insensitive comparison)
            "marshall, mathers, eminem"
                but did not

            Additional Info:
                Custom error message
            """,

            errorWithoutSource:
            """
            "my name is slim shady"
                should contain any (case insensitive comparison)
            "marshall, mathers, eminem"
                but did not

            Additional Info:
                Custom error message
            """);
    }
}