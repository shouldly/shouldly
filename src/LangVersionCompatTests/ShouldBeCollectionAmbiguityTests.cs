using Shouldly;
using Xunit;

namespace LangVersionCompatTests;

/// <summary>
/// Regression guard for https://github.com/shouldly/shouldly/issues/1278.
///
/// This project is pinned to <c>LangVersion 12</c> (see the .csproj). Before the fix, the
/// scalar <c>ShouldBe&lt;T&gt;(T?, T?, ...)</c> and the enumerable
/// <c>ShouldBe&lt;T&gt;(IEnumerable&lt;T&gt;?, ...)</c> overloads had different parameter
/// counts, which disabled the C# "more specific parameter type" tie-break. On any
/// <c>LangVersion &lt; 13</c> — where <c>[OverloadResolutionPriority]</c> is silently a no-op —
/// <c>IEnumerable&lt;T&gt;.ShouldBe(array)</c> then failed with CS0121 (ambiguous call).
///
/// Keeping the two overloads at equal arity restores the tie-break at every LangVersion.
/// The <em>compilation</em> of this file is the primary regression test; the runtime
/// assertions below additionally confirm each call binds to the intended overload.
/// </summary>
public class ShouldBeCollectionAmbiguityTests
{
    [Fact]
    public void Enumerable_typed_actual_binds_to_the_enumerable_overload()
    {
        // Lazily-typed IEnumerable<T>, exactly as in the issue's repro.
        IEnumerable<Type> actual = new[] { typeof(int), typeof(string) }.Select(t => t);

        // The exact line that regressed in v5 under LangVersion < 13.
        actual.ShouldBe(new[] { typeof(int), typeof(string) });

        // customMessage overload must remain reachable (collection comparison, not scalar).
        actual.ShouldBe(new[] { typeof(int), typeof(string) }, "types should match");
    }

    [Fact]
    public void IgnoreOrder_overload_is_reachable()
    {
        IEnumerable<int> actual = new[] { 3, 1, 2 }.Select(x => x);

        actual.ShouldBe(new[] { 1, 2, 3 }, ignoreOrder: true);
    }

    [Fact]
    public void Scalar_values_still_bind_to_the_scalar_overload()
    {
        1.ShouldBe(1);
        "cheese".ShouldBe("cheese");
    }
}
