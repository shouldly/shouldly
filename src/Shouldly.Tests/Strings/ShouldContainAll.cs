namespace Shouldly.Tests.Strings;

public class ShouldContainAll
{
    [Fact]
    public void ShouldContainAll_ShouldPass_WhenAllValuesPresent()
    {
        "my name is slim shady".ShouldContainAll(["name", "slim", "shady"]);
    }

    [Fact]
    public void ShouldContainAll_ShouldThrow_WhenAnyValueMissing()
    {
        Verify.ShouldFail(
            () => "my name is slim shady".ShouldContainAll(["name", "slim", "eminem"]),

            errorWithSource:
            """
            "my name is slim shady"
                should contain all (case insensitive comparison)
            "name, slim, eminem"
                but did not
            """,

            errorWithoutSource:
            """
            "my name is slim shady"
                should contain all (case insensitive comparison)
            "name, slim, eminem"
                but did not
            """);
    }

    [Fact]
    public void ShouldContainAll_WithCaseSensitive_ShouldFailIfCaseDoesNotMatch()
    {
        Verify.ShouldFail(
            () => "Hello World".ShouldContainAll(["hello"], Case.Sensitive),

            errorWithSource:
            """
            "Hello World"
                should contain all
            "hello"
                but did not
            """,

            errorWithoutSource:
            """
            "Hello World"
                should contain all
            "hello"
                but did not
            """);
    }

    [Fact]
    public void ShouldContainAll_WithCaseInsensitive_ShouldPassIfCaseDoesNotMatch()
    {
        "Hello World".ShouldContainAll(["hello"], Case.Insensitive);
    }

    [Fact]
    public void ShouldContainAll_ShouldIncludeCustomMessage_WhenProvided()
    {
        Verify.ShouldFail(
            () => "my name is slim shady".ShouldContainAll(["name", "slim", "eminem"], Case.Insensitive, "Custom error message"),

            errorWithSource:
            """
            "my name is slim shady"
                should contain all (case insensitive comparison)
            "name, slim, eminem"
                but did not

            Additional Info:
                Custom error message
            """,

            errorWithoutSource:
            """
            "my name is slim shady"
                should contain all (case insensitive comparison)
            "name, slim, eminem"
                but did not

            Additional Info:
                Custom error message
            """);
    }
}