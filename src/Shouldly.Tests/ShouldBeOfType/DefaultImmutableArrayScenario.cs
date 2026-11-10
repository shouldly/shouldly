using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace Shouldly.Tests.ShouldBeOfType;

// A default ImmutableArray<T> prints as null, but it still has a type to report (#1406).
public class DefaultImmutableArrayScenario
{
    [Fact]
    public void DefaultImmutableArrayScenarioShouldFail()
    {
        object actual = default(ImmutableArray<int>);
        Verify.ShouldFail(() =>
            actual.ShouldBeOfType<string>("Some additional context"),
            // The element type's assembly-qualified name differs between runtimes.
            message => Regex.Replace(message, @"\[\[System\.Int32, [^\]]+\]\]", "[[System.Int32]]"));
    }

    [Fact]
    public void ShouldPass()
    {
        object actual = default(ImmutableArray<int>);
        actual.ShouldBeOfType<ImmutableArray<int>>();
    }
}
