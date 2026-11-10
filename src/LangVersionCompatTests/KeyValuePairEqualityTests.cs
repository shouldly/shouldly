using Shouldly;
using Xunit;

namespace LangVersionCompatTests;

/// <summary>
/// Regression guard for https://github.com/shouldly/shouldly/issues/1408 under <c>LangVersion 12</c>.
///
/// <c>[OverloadResolutionPriority]</c> is a no-op before C# 13, so <c>Dictionary&lt;K, V&gt;.ShouldBe(Dictionary&lt;K, V&gt;)</c>
/// binds to the scalar <c>ShouldBe&lt;T&gt;(T?, T?)</c> overload here, and the entries reach the comparer as boxed
/// <see cref="KeyValuePair{TKey, TValue}"/>s. Their collection-typed values must still be compared element-wise,
/// so the result doesn't depend on the consumer's LangVersion.
/// </summary>
public class KeyValuePairEqualityTests
{
    [Fact]
    public void Dictionaries_with_collection_values_are_compared_structurally()
    {
        var actual = new Dictionary<string, int[]> { ["a"] = new[] { 1 } };

        actual.ShouldBe(new Dictionary<string, int[]> { ["a"] = new[] { 1 } });
        actual.ShouldNotBe(new Dictionary<string, int[]> { ["a"] = new[] { 2 } });
    }

    [Fact]
    public void ShouldNotBe_fails_for_structurally_equal_dictionaries() =>
        Should.Throw<ShouldAssertException>(() =>
            new Dictionary<string, int[]> { ["a"] = new[] { 1 } }
                .ShouldNotBe(new Dictionary<string, int[]> { ["a"] = new[] { 1 } }));
}
