namespace Shouldly.Tests.Strings;

public class ShouldContainAll
{
    [Fact]
    public void ShouldPassWhenAllValuesPresent()
    {
        "my name is slim shady".ShouldContainAll(["name", "slim", "shady"]);
    }

    [Fact]
    public void ShouldFailWhenAnyValueMissing()
    {
        var target = "my name is slim shady";
        Verify.ShouldFail(() =>
            target.ShouldContainAll(["name", "slim", "eminem"]));
    }

    [Fact]
    public void CaseSensitiveShouldFailIfCaseDoesNotMatch()
    {
        var target = "Hello World";
        Verify.ShouldFail(() =>
            target.ShouldContainAll(["hello"], Case.Sensitive));
    }

    [Fact]
    public void CaseInsensitiveShouldPassIfCaseDoesNotMatch()
    {
        "Hello World".ShouldContainAll(["hello"], Case.Insensitive);
    }

    [Fact]
    public void ShouldIncludeCustomMessageWhenProvided()
    {
        var target = "my name is slim shady";
        Verify.ShouldFail(() =>
            target.ShouldContainAll(["name", "slim", "eminem"], Case.Insensitive, "Custom error message"));
    }
}
