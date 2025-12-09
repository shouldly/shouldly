namespace Shouldly.Tests.Strings;

public class ShouldNotContainAll
{
    [Fact]
    public void ShouldNotContainAll_ShouldPass_WhenNotAllValuesPresent()
    {
        "my name is slim shady".ShouldNotContainAll(["name", "slim", "eminem"]);
    }

    [Fact]
    public void ShouldNotContainAll_ShouldThrow_WhenAllValuesPresent()
    {
        Verify.ShouldFail(
            () => "my name is slim shady".ShouldNotContainAll(["name", "slim", "shady"]),

            errorWithSource:
            """
            "my name is slim shady"
                should not contain all (case insensitive comparison)
            "name, slim, shady"
                but did
            """,

            errorWithoutSource:
            """
            "my name is slim shady"
                should not contain all (case insensitive comparison)
            "name, slim, shady"
                but did
            """);
    }

    [Fact]
    public void ShouldNotContainAll_WithCaseSensitive_ShouldFailIfAllMatch()
    {
        Verify.ShouldFail(
            () => "Hello World".ShouldNotContainAll(["Hello"], Case.Sensitive),

            errorWithSource:
            """
            "Hello World"
                should not contain all
            "Hello"
                but did
            """,

            errorWithoutSource:
            """
            "Hello World"
                should not contain all
            "Hello"
                but did
            """);
    }

    [Fact]
    public void ShouldNotContainAll_WithCaseSensitive_ShouldPassIfCaseDoesNotMatch()
    {
        "Hello World".ShouldNotContainAll(["hello"], Case.Sensitive);
    }

    [Fact]
    public void ShouldNotContainAll_WithCaseInsensitive_ShouldFailIfAllPresentIgnoringCase()
    {
        Verify.ShouldFail(
            () => "Hello World".ShouldNotContainAll(["hello"], Case.Insensitive),

            errorWithSource:
            """
            "Hello World"
                should not contain all (case insensitive comparison)
            "hello"
                but did
            """,

            errorWithoutSource:
            """
            "Hello World"
                should not contain all (case insensitive comparison)
            "hello"
                but did
            """);
    }

    [Fact]
    public void ShouldNotContainAll_WithCaseInsensitive_ShouldPassIfNotAllPresentIgnoringCase()
    {
        "Hello World".ShouldNotContainAll(["hello", "mars"], Case.Insensitive);
    }

    [Fact]
    public void ShouldNotContainAll_ShouldIncludeCustomMessage_WhenProvided()
    {
        Verify.ShouldFail(
            () => "my name is slim shady".ShouldNotContainAll(["name", "slim", "shady"], Case.Insensitive, "Custom error message"),

            errorWithSource:
            """
            "my name is slim shady"
                should not contain all (case insensitive comparison)
            "name, slim, shady"
                but did

            Additional Info:
                Custom error message
            """,

            errorWithoutSource:
            """
            "my name is slim shady"
                should not contain all (case insensitive comparison)
            "name, slim, shady"
                but did

            Additional Info:
                Custom error message
            """);
    }
}