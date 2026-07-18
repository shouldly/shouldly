namespace Shouldly.Tests.Strings;

public class ShouldContainAny
{
    [Fact]
    public void ShouldPassWhenAtLeastOneValuePresent()
    {
        "my name is slim shady".ShouldContainAny(["name", "slim", "eminem"]);
    }

    [Fact]
    public void ShouldFailWhenNoValuesPresent()
    {
        var target = "my name is slim shady";
        Verify.ShouldFail(() =>
            target.ShouldContainAny(["marshall", "mathers", "eminem"]));
    }

    [Fact]
    public void CaseSensitiveShouldFailIfCaseDoesNotMatch()
    {
        var target = "Hello World";
        Verify.ShouldFail(() =>
            target.ShouldContainAny(["hello"], Case.Sensitive));
    }

    [Fact]
    public void CaseInsensitiveShouldPassIfCaseDoesNotMatch()
    {
        "Hello World".ShouldContainAny(["hello"], Case.Insensitive);
    }

    [Fact]
    public void ShouldIncludeCustomMessageWhenProvided()
    {
        var target = "my name is slim shady";
        Verify.ShouldFail(() =>
            target.ShouldContainAny(["marshall", "mathers", "eminem"], Case.Insensitive, "Custom error message"));
    }
}
