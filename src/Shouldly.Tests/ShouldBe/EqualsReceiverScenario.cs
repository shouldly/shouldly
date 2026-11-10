namespace Shouldly.Tests.ShouldBe;

/// <summary>
/// <c>ShouldBe</c> calls <c>Equals</c> on the expected value, passing the actual one, so a broken <c>Equals</c> on
/// the value under test can't make an assertion pass. Custom comparers likewise receive
/// <c>(expected, actual)</c>. These tests pin that every path agrees, including <c>ignoreOrder: true</c> and the
/// span/memory overloads, which used to call <c>Equals</c> on the actual value. See issue #1076.
/// </summary>
public class EqualsReceiverScenario
{
    [Fact]
    public void ScalarCallsEqualsOnExpected() =>
        Should.Throw<ShouldAssertException>(() => ((Element)new Lenient()).ShouldBe(new Strict()));

    [Fact]
    public void EnumerableCallsEqualsOnExpected() =>
        Should.Throw<ShouldAssertException>(() => Actual().ShouldBe(Expected()));

    [Fact]
    public void IgnoringOrderCallsEqualsOnExpected() =>
        Should.Throw<ShouldAssertException>(() => Actual().ShouldBe(Expected(), ignoreOrder: true));

    [Fact]
    public void SpanCallsEqualsOnExpected() =>
        Should.Throw<ShouldAssertException>(() => Actual().AsSpan().ShouldBe(Expected().AsSpan()));

    [Fact]
    public void ReadOnlySpanCallsEqualsOnExpected() =>
        Should.Throw<ShouldAssertException>(() => ((ReadOnlySpan<Element>)Actual()).ShouldBe(Expected()));

    [Fact]
    public void MemoryCallsEqualsOnExpected() =>
        Should.Throw<ShouldAssertException>(() => Actual().AsMemory().ShouldBe(Expected().AsMemory()));

    [Fact]
    public void ReadOnlyMemoryCallsEqualsOnExpected() =>
        Should.Throw<ShouldAssertException>(() => ((ReadOnlyMemory<Element>)Actual()).ShouldBe(Expected()));

    [Fact]
    public void CustomComparerReceivesExpectedThenActual()
    {
        var comparer = new RecordingComparer();

        new Named("actual").ShouldBe(new Named("expected"), comparer);
        new[] { new Named("actual") }.ShouldBe([new Named("expected")], comparer);
        new[] { new Named("actual") }.ShouldBe([new Named("expected")], comparer, ignoreOrder: true);

        comparer.Calls.ShouldBe(["expected,actual", "expected,actual", "expected,actual"]);
    }

    [Fact]
    public void ShouldBeOfObjectCallsEqualsOnExpected()
    {
        // The shape from #1076: the actual value's Equals would accept the string, the string's doesn't.
        var path = new PathLike("C:\\file.txt");

        Should.Throw<ShouldAssertException>(() => path.ShouldBe<object>("C:\\file.txt"));
        "C:\\file.txt".ShouldBe<object>(path);
    }

    [Fact]
    public void EqualsReceiverIgnoringOrderScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            Actual().ShouldBe(Expected(), ignoreOrder: true, "Some additional context"));
    }

    private static Element[] Actual() => [new Lenient()];

    private static Element[] Expected() => [new Strict()];

    public abstract class Element;

    /// <summary>Claims to equal anything: a broken <c>Equals</c> on the value under test.</summary>
    public sealed class Lenient : Element
    {
        public override bool Equals(object? obj) => true;

        public override int GetHashCode() => 0;

        public override string ToString() => "Lenient";
    }

    /// <summary>Equal to nothing, itself included.</summary>
    public sealed class Strict : Element
    {
        public override bool Equals(object? obj) => false;

        public override int GetHashCode() => 0;

        public override string ToString() => "Strict";
    }

    private sealed class Named(string name)
    {
        public string Name { get; } = name;
    }

    private sealed class RecordingComparer : IEqualityComparer<Named>
    {
        public List<string> Calls { get; } = [];

        public bool Equals(Named? x, Named? y)
        {
            Calls.Add($"{x?.Name},{y?.Name}");
            return true;
        }

        public int GetHashCode(Named obj) => 0;
    }

    private readonly struct PathLike(string path) : IEquatable<PathLike>
    {
        private string Path { get; } = path;

        public bool Equals(PathLike other) => Path == other.Path;

        public override bool Equals(object? obj) => obj is PathLike other ? Equals(other) : obj is string s && s == Path;

        public override int GetHashCode() => Path.GetHashCode();
    }
}
