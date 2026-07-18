using Shouldly;
using Xunit;

namespace LangVersionCompatTests;

/// <summary>
/// Regression guard for https://github.com/shouldly/shouldly/issues/1284.
///
/// This project is pinned to <c>LangVersion 12</c> (see the .csproj). The v5 dictionary
/// assertions previously shipped a pair of sibling-interface overloads —
/// <c>IDictionary&lt;,&gt;</c> and (on net9.0+) <c>IReadOnlyDictionary&lt;,&gt;</c> — with the
/// read-only one decorated <c>[OverloadResolutionPriority(1)]</c> to break the tie. Because a
/// concrete <c>Dictionary&lt;,&gt;</c> implements both interfaces, calling e.g.
/// <c>dict.ShouldContainKey(key)</c> on it failed with CS0121 on any <c>LangVersion &lt; 13</c>,
/// where <c>[OverloadResolutionPriority]</c> is silently a no-op. Unlike #1278 these are sibling
/// interfaces with no "more specific" tie-break, so equalising arity could not help.
///
/// The fix collapses each assertion to a single overload typed to the shared base,
/// <c>IEnumerable&lt;KeyValuePair&lt;TKey, TValue&gt;&gt;</c>. The <em>compilation</em> of this
/// file is the primary regression test; the runtime assertions confirm each call still binds and
/// dispatches correctly for the concrete, mutable-interface, and read-only-interface receivers.
/// </summary>
public class DictionaryAssertionAmbiguityTests
{
    [Fact]
    public void Concrete_dictionary_binds_without_ambiguity()
    {
        // The exact repro from the issue: a Dictionary<,> implements both dictionary interfaces.
        var dict = new Dictionary<string, int> { ["a"] = 1 };

        dict.ShouldContainKey("a");
        dict.ShouldNotContainKey("b");
        dict.ShouldContainKeyAndValue("a", 1);
        dict.ShouldNotContainValueForKey("a", 2);
    }

    [Fact]
    public void Mutable_dictionary_interface_binds()
    {
        IDictionary<string, int> dict = new Dictionary<string, int> { ["a"] = 1 };

        dict.ShouldContainKey("a");
        dict.ShouldNotContainKey("b");
        dict.ShouldContainKeyAndValue("a", 1);
        dict.ShouldNotContainValueForKey("a", 2);
    }

    [Fact]
    public void ReadOnly_dictionary_interface_binds()
    {
        IReadOnlyDictionary<string, int> dict = new Dictionary<string, int> { ["a"] = 1 };

        dict.ShouldContainKey("a");
        dict.ShouldNotContainKey("b");
        dict.ShouldContainKeyAndValue("a", 1);
        dict.ShouldNotContainValueForKey("a", 2);
    }
}
