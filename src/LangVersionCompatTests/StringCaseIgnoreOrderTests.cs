using Shouldly;
using Xunit;

namespace LangVersionCompatTests;

/// <summary>
/// Guards https://github.com/shouldly/shouldly/issues/1412 under <c>LangVersion 12</c>: a string collection's
/// <see cref="Case"/> overload combines with <c>ignoreOrder</c>, and adding that overload leaves the existing
/// <see cref="Case"/> and <c>ignoreOrder</c> calls unambiguous. Compiling this file is the primary check.
/// </summary>
public class StringCaseIgnoreOrderTests
{
    [Fact]
    public void Case_combines_with_ignoreOrder()
    {
        IEnumerable<string> actual = new[] { "A", "b" }.Select(s => s);

        actual.ShouldBe(new[] { "B", "a" }, Case.Insensitive, ignoreOrder: true);
        actual.ShouldBe(new[] { "B", "a" }, Case.Insensitive, true, "strings should match");
        new[] { "A", "b" }.ShouldBe(new[] { "B", "a" }, Case.Insensitive, ignoreOrder: true);
        new List<string> { "A", "b" }.ShouldBe(new List<string> { "B", "a" }, Case.Insensitive, ignoreOrder: true);

        Assert.Throws<ShouldAssertException>(() =>
            actual.ShouldBe(new[] { "B", "a" }, Case.Sensitive, ignoreOrder: true));
    }

    [Fact]
    public void Existing_case_and_ignoreOrder_overloads_still_bind()
    {
        IEnumerable<string> actual = new[] { "A", "b" }.Select(s => s);

        actual.ShouldBe(new[] { "a", "B" }, Case.Insensitive);
        actual.ShouldBe(new[] { "a", "B" }, Case.Insensitive, "strings should match");
        actual.ShouldBe(new[] { "b", "A" }, ignoreOrder: true);
        actual.ShouldBe(new[] { "b", "A" }, true, "strings should match");
    }
}
