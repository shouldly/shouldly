using System.Globalization;
using System.Net;
using System.Text;

namespace Shouldly.Tests.ShouldBeEquivalentTo;

// A type that ships with .NET and defines its own equality is a value, compared by that equality: its public
// members are views of it, which can differ between equal values, repeat one difference, or throw.
public class FrameworkValueScenario
{
    [Fact]
    public void ShouldPassWhenEqualValuesDifferInAViewOfThem()
    {
        // A cached culture is read-only; a new one isn't. They are the same culture.
        new { Culture = CultureInfo.GetCultureInfo("en-GB") }
            .ShouldBeEquivalentTo(new { Culture = new CultureInfo("en-GB") });
    }

    [Fact]
    public void ShouldFailOnceWhenValuesDiffer()
    {
        // Two cultures differ in dozens of members; they are reported as one difference. (A culture is used because its
        // text is its name on every runtime, which Complex's isn't.)
        var subject = new { Culture = CultureInfo.GetCultureInfo("en-GB") };
        var expected = new { Culture = CultureInfo.GetCultureInfo("fr-FR") };

        Verify.ShouldFail(() =>
            subject.ShouldBeEquivalentTo(expected, "Some additional context"));
    }

    [Fact]
    public void ShouldCompareValuesWhoseMembersThrow()
    {
        // IPAddress.ScopeId throws for an IPv4 address; Encoding's members include ones that aren't supported.
        IPAddress.Parse("10.0.0.1").ShouldBeEquivalentTo(IPAddress.Parse("10.0.0.1"));
        Should.Throw<ShouldAssertException>(() => IPAddress.Parse("10.0.0.1").ShouldBeEquivalentTo(IPAddress.Parse("10.0.0.2")));
        new { Encoding = Encoding.UTF8 }.ShouldBeEquivalentTo(new { Encoding = new UTF8Encoding(true) });
        Should.Throw<ShouldAssertException>(() => new { Encoding = Encoding.UTF8 }.ShouldBeEquivalentTo(new { Encoding = Encoding.ASCII }));
    }

    [Fact]
    public void ShouldStillCompareTuplesItemByItem()
    {
        // A tuple's own Equals compares a collection-typed item by reference.
        (1, new List<int> { 1, 2 }).ShouldBeEquivalentTo((1, new List<int> { 1, 2 }));
        Tuple.Create(1, new List<int> { 1, 2 }).ShouldBeEquivalentTo(Tuple.Create(1, new List<int> { 1, 2 }));
        Should.Throw<ShouldAssertException>(() => (1, new List<int> { 1, 2 }).ShouldBeEquivalentTo((1, new List<int> { 1, 3 })));
    }
}
