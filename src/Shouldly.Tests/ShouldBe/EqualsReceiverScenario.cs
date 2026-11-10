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

    [Fact]
    public void EquivalentToLeafCallsEqualsOnExpected()
    {
        // ShouldBeEquivalentTo compares a string-valued node as a leaf, so this is #1076's shape again.
        var path = new PathLike("C:\\file.txt");

        Should.Throw<ShouldAssertException>(() => path.ShouldBeEquivalentTo("C:\\file.txt"));
        Should.Throw<ShouldAssertException>(() =>
            new { File = (object)path }.ShouldBeEquivalentTo(new { File = (object)"C:\\file.txt" }));
    }

    [Fact]
    public void IgnoringOrderWithComparerScenarioShouldFail()
    {
        // The message must match elements with the comparer the assertion used, or it would report 1 and 11 as
        // missing even though the comparer matched them.
        int[] actual = [1, 2];

        Verify.ShouldFail(() =>
            actual.ShouldBe([11, 3], new LastDigitComparer(), ignoreOrder: true, "Some additional context"));
    }

    [Fact]
    public void StringsIgnoringOrderWithComparerScenarioShouldFail()
    {
        // String collections used to get the positional, case-sensitivity message instead of the ignore-order one,
        // which flagged "A" as a difference although the comparer matched it to "a".
        string[] actual = ["A", "b"];

        Verify.ShouldFail(() =>
            actual.ShouldBe(["a", "c"], StringComparer.OrdinalIgnoreCase, ignoreOrder: true, "Some additional context"));
    }

    [Fact]
    public void StringsWithComparerScenarioShouldFail()
    {
        // The comparer decides case sensitivity, so the message mustn't claim a case-sensitive comparison, and
        // mustn't highlight "A" as a difference when the comparer matched it to "a".
        string[] actual = ["A", "b"];

        Verify.ShouldFail(() =>
            actual.ShouldBe(["a", "c"], StringComparer.OrdinalIgnoreCase, customMessage: "Some additional context"));
    }

    [Fact]
    public void WithComparerScenarioShouldFail()
    {
        // The difference highlighting must use the comparer: 1 matches 11, only 2 vs 3 differs.
        int[] actual = [1, 2];

        Verify.ShouldFail(() =>
            actual.ShouldBe([11, 3], new LastDigitComparer(), customMessage: "Some additional context"));
    }

    [Fact]
    public void ScalarStringWithComparerScenarioShouldFail()
    {
        Verify.ShouldFail(() =>
            "A".ShouldBe("b", StringComparer.OrdinalIgnoreCase, "Some additional context"));
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

    private sealed class LastDigitComparer : IEqualityComparer<int>
    {
        public bool Equals(int x, int y) => x % 10 == y % 10;

        public int GetHashCode(int obj) => obj % 10;
    }

    private readonly struct PathLike(string path) : IEquatable<PathLike>
    {
        private string Path { get; } = path;

        public bool Equals(PathLike other) => Path == other.Path;

        public override bool Equals(object? obj) => obj is PathLike other ? Equals(other) : obj is string s && s == Path;

        public override int GetHashCode() => Path.GetHashCode();
    }
}
