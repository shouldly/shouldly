using System.Collections;
using System.Reflection;

namespace Shouldly.Tests.ConventionTests;

/// <summary>
/// Every assertion that takes a sequence reads it once: the check and the failure message see the same items, so a
/// sequence that can only be enumerated once passes and fails exactly as an array of the same items does.
/// </summary>
public class SequencesAreReadOnce
{
    public static TheoryData<string> Assertions => new(Cases.Select(c => c.Assertion));

    [Theory]
    [MemberData(nameof(Assertions))]
    public void PassesOnASinglePassSequence(string assertion)
    {
        var c = Cases.Single(c => c.Assertion == assertion);

        c.Passes(new Sequences(singlePass: true));
    }

    [Theory]
    [MemberData(nameof(Assertions))]
    public void FailsOnASinglePassSequenceAsOnAnArray(string assertion)
    {
        var c = Cases.Single(c => c.Assertion == assertion);
        var onArrays = Should.Throw<ShouldAssertException>(() => c.Fails(new Sequences(singlePass: false))).Message;

        var onSinglePass = Should.Throw<ShouldAssertException>(() => c.Fails(new Sequences(singlePass: true))).Message;

        onSinglePass.ShouldBe(c.SinglePassMessage ?? onArrays);
    }

    [Fact]
    public void EveryAssertionThatTakesASequenceIsListed()
    {
        var assertions =
            from type in typeof(Should).Assembly.GetExportedTypes()
            where type is { IsAbstract: true, IsSealed: true }
            from method in type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
            where method.GetParameters().Any(p => IsSequence(p.ParameterType))
            select Describe(method);

        var listed = Cases.Select(c => c.Assertion);

        assertions.Except(listed).OrderBy(a => a).ShouldBeEmpty("Add a case below for each of these");
        listed.Except(assertions).OrderBy(a => a).ShouldBeEmpty("These cases name no assertion");
    }

    private static bool IsSequence(Type type) =>
        type == typeof(IEnumerable) ||
        (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(IEnumerable<>));

    private static string Describe(MethodInfo method) =>
        $"{method.DeclaringType!.Name}.{method.Name}({string.Join(", ", method.GetParameters().Select(p => Describe(p.ParameterType)))})";

    private static string Describe(Type type) =>
        type.IsGenericType
            ? $"{type.Name.Substring(0, type.Name.IndexOf('`'))}<{string.Join(", ", type.GetGenericArguments().Select(Describe))}>"
            : type.Name;

    /// <param name="SinglePassMessage">
    /// The failure message for the single-pass sequence, where it can't match the one for the array: a message
    /// describes an empty sequence by its own type.
    /// </param>
    private sealed record SequenceCase(string Assertion, Action<Sequences> Passes, Action<Sequences> Fails, string? SinglePassMessage = null);

    /// <summary>
    /// Makes the sequences a case passes to its assertion: arrays, or sequences that throw if read a second time.
    /// </summary>
    private sealed class Sequences(bool singlePass)
    {
        public IEnumerable<T> Of<T>(params T[] items) => singlePass ? new SinglePass<T>(items) : items;
    }

    private static readonly IEqualityComparer<int> Comparer = EqualityComparer<int>.Default;

    private static readonly SequenceCase[] Cases =
    [
        // Dictionaries
        new("ShouldBeDictionaryTestExtensions.ShouldContainKey(IEnumerable<KeyValuePair<TKey, TValue>>, TKey, String, String)",
            s => s.Of(Pair("a", 1)).ShouldContainKey("a"),
            s => s.Of(Pair("a", 1)).ShouldContainKey("b")),
        new("ShouldBeDictionaryTestExtensions.ShouldNotContainKey(IEnumerable<KeyValuePair<TKey, TValue>>, TKey, String, String)",
            s => s.Of(Pair("a", 1)).ShouldNotContainKey("b"),
            s => s.Of(Pair("a", 1)).ShouldNotContainKey("a")),
        new("ShouldBeDictionaryTestExtensions.ShouldContainKeyAndValue(IEnumerable<KeyValuePair<TKey, TValue>>, TKey, TValue, String, String)",
            s => s.Of(Pair("a", 1)).ShouldContainKeyAndValue("a", 1),
            s => s.Of(Pair("a", 1)).ShouldContainKeyAndValue("a", 2)),
        new("ShouldBeDictionaryTestExtensions.ShouldNotContainValueForKey(IEnumerable<KeyValuePair<TKey, TValue>>, TKey, TValue, String, String)",
            s => s.Of(Pair("a", 1)).ShouldNotContainValueForKey("a", 2),
            s => s.Of(Pair("a", 1)).ShouldNotContainValueForKey("a", 1)),

        // Equality
        new("ShouldBeTestExtensions.ShouldBe(IEnumerable<T>, IEnumerable<T>, String, String)",
            s => s.Of(1, 2).ShouldBe(s.Of(1, 2)),
            s => s.Of(1, 2).ShouldBe(s.Of(1, 3))),
        new("ShouldBeTestExtensions.ShouldBe(IEnumerable<T>, IEnumerable<T>, Boolean, String, String)",
            s => s.Of(1, 2).ShouldBe(s.Of(2, 1), ignoreOrder: true),
            s => s.Of(1, 2).ShouldBe(s.Of(1, 3), ignoreOrder: true)),
        new("ShouldBeTestExtensions.ShouldBe(IEnumerable<T>, IEnumerable<T>, IEqualityComparer<T>, Boolean, String, String)",
            s => s.Of(1, 2).ShouldBe(s.Of(2, 1), Comparer, ignoreOrder: true),
            s => s.Of(1, 2).ShouldBe(s.Of(1, 3), Comparer, ignoreOrder: true)),
        new("ShouldBeTestExtensions.ShouldBe(IEnumerable<Decimal>, IEnumerable<Decimal>, Decimal, String, String)",
            s => s.Of(1m, 2m).ShouldBe(s.Of(1.05m, 2m), 0.1m),
            s => s.Of(1m, 2m).ShouldBe(s.Of(1.5m, 2m), 0.1m)),
        new("ShouldBeTestExtensions.ShouldBe(IEnumerable<Double>, IEnumerable<Double>, Double, String, String)",
            s => s.Of(1d, 2d).ShouldBe(s.Of(1.05d, 2d), 0.1),
            s => s.Of(1d, 2d).ShouldBe(s.Of(1.5d, 2d), 0.1)),
        new("ShouldBeTestExtensions.ShouldBe(IEnumerable<Single>, IEnumerable<Single>, Double, String, String)",
            s => s.Of(1f, 2f).ShouldBe(s.Of(1.05f, 2f), 0.1),
            s => s.Of(1f, 2f).ShouldBe(s.Of(1.5f, 2f), 0.1)),
        new("ShouldBeEnumerableTestExtensions.ShouldBe(IEnumerable<String>, IEnumerable<String>, Case, String, String)",
            s => s.Of("a", "b").ShouldBe(s.Of("A", "B"), Case.Insensitive),
            s => s.Of("a", "b").ShouldBe(s.Of("A", "C"), Case.Insensitive)),
        new("ShouldBeEnumerableTestExtensions.ShouldBe(IEnumerable<String>, IEnumerable<String>, Case, Boolean, String, String)",
            s => s.Of("a", "b").ShouldBe(s.Of("B", "A"), Case.Insensitive, ignoreOrder: true),
            s => s.Of("a", "b").ShouldBe(s.Of("A", "C"), Case.Insensitive, ignoreOrder: true)),

        // Membership
        new("ShouldBeEnumerableTestExtensions.ShouldContain(IEnumerable<T>, T, String, String)",
            s => s.Of(1, 2).ShouldContain(2),
            s => s.Of(1, 2).ShouldContain(3)),
        new("ShouldBeEnumerableTestExtensions.ShouldContain(IEnumerable<T>, T, IEqualityComparer<T>, String, String)",
            s => s.Of(1, 2).ShouldContain(2, Comparer),
            s => s.Of(1, 2).ShouldContain(3, Comparer)),
        new("ShouldBeEnumerableTestExtensions.ShouldNotContain(IEnumerable<T>, T, String, String)",
            s => s.Of(1, 2).ShouldNotContain(3),
            s => s.Of(1, 2).ShouldNotContain(2)),
        new("ShouldBeEnumerableTestExtensions.ShouldNotContain(IEnumerable<T>, T, IEqualityComparer<T>, String, String)",
            s => s.Of(1, 2).ShouldNotContain(3, Comparer),
            s => s.Of(1, 2).ShouldNotContain(2, Comparer)),
        new("ShouldBeEnumerableTestExtensions.ShouldContain(IEnumerable<Double>, Double, Double, String, String)",
            s => s.Of(1d, 2d).ShouldContain(2.05d, 0.1),
            s => s.Of(1d, 2d).ShouldContain(3d, 0.1)),
        new("ShouldBeEnumerableTestExtensions.ShouldContain(IEnumerable<Single>, Single, Double, String, String)",
            s => s.Of(1f, 2f).ShouldContain(2.05f, 0.1),
            s => s.Of(1f, 2f).ShouldContain(3f, 0.1)),
        new("ShouldBeEnumerableTestExtensions.ShouldContain(IEnumerable<T>, Expression<Func<T, Boolean>>, String, String)",
            s => s.Of(1, 2).ShouldContain(x => x == 2),
            s => s.Of(1, 2).ShouldContain(x => x == 3)),
        new("ShouldBeEnumerableTestExtensions.ShouldContain(IEnumerable<T>, Expression<Func<T, Boolean>>, Int32, String, String)",
            s => s.Of(1, 2).ShouldContain(x => x > 0, 2),
            s => s.Of(1, 2).ShouldContain(x => x > 0, 1)),
        new("ShouldBeEnumerableTestExtensions.ShouldNotContain(IEnumerable<T>, Expression<Func<T, Boolean>>, String, String)",
            s => s.Of(1, 2).ShouldNotContain(x => x == 3),
            s => s.Of(1, 2).ShouldNotContain(x => x == 2)),
        new("ShouldBeEnumerableTestExtensions.ShouldAllBe(IEnumerable<T>, Expression<Func<T, Boolean>>, String, String)",
            s => s.Of(1, 2).ShouldAllBe(x => x > 0),
            s => s.Of(1, 2).ShouldAllBe(x => x > 1)),
        new("ShouldBeEnumerableTestExtensions.ShouldBeSubsetOf(IEnumerable<T>, IEnumerable<T>, String, String)",
            s => s.Of(1, 2).ShouldBeSubsetOf(s.Of(1, 2, 3)),
            s => s.Of(1, 4).ShouldBeSubsetOf(s.Of(1, 2, 3))),
        new("ShouldBeEnumerableTestExtensions.ShouldBeSubsetOf(IEnumerable<T>, IEnumerable<T>, IEqualityComparer<T>, String, String)",
            s => s.Of(1, 2).ShouldBeSubsetOf(s.Of(1, 2, 3), Comparer),
            s => s.Of(1, 4).ShouldBeSubsetOf(s.Of(1, 2, 3), Comparer)),
        new("ShouldBeEnumerableTestExtensions.ShouldBeUnique(IEnumerable<T>, String, String)",
            s => s.Of(1, 2).ShouldBeUnique(),
            s => s.Of(1, 1).ShouldBeUnique()),
        new("ShouldBeEnumerableTestExtensions.ShouldBeUnique(IEnumerable<T>, IEqualityComparer<T>, String, String)",
            s => s.Of(1, 2).ShouldBeUnique(Comparer),
            s => s.Of(1, 1).ShouldBeUnique(Comparer)),

        // Emptiness and count
        new("ShouldBeEnumerableTestExtensions.ShouldBeEmpty(IEnumerable<T>, String, String)",
            s => s.Of<int>().ShouldBeEmpty(),
            s => s.Of(1, 2).ShouldBeEmpty()),
        new("ShouldBeEnumerableTestExtensions.ShouldNotBeEmpty(IEnumerable<T>, String, String)",
            s => s.Of(1, 2).ShouldNotBeEmpty(),
            s => s.Of<int>().ShouldNotBeEmpty()),
        new("ShouldBeEnumerableTestExtensions.ShouldBeNullOrEmpty(IEnumerable<T>, String, String)",
            s => s.Of<int>().ShouldBeNullOrEmpty(),
            s => s.Of(1, 2).ShouldBeNullOrEmpty()),
        new("ShouldBeEnumerableTestExtensions.ShouldNotBeNullOrEmpty(IEnumerable<T>, String, String)",
            s => s.Of(1, 2).ShouldNotBeNullOrEmpty(),
            s => s.Of<int>().ShouldNotBeNullOrEmpty(),
            """
            s.Of<int>() ([] (Shouldly.Tests.TestHelpers.SinglePass`1[System.Int32]))
                should not be null or empty
            """),
        new("ShouldBeEnumerableTestExtensions.ShouldHaveSingleItem(IEnumerable<T>, String, String)",
            s => s.Of(1).ShouldHaveSingleItem().ShouldBe(1),
            s => s.Of(1, 2).ShouldHaveSingleItem()),
        new("ShouldBeEnumerableTestExtensions.ShouldHaveCount(IEnumerable<T>, Int32, String, String)",
            s => s.Of(1, 2).ShouldHaveCount(2),
            s => s.Of(1, 2).ShouldHaveCount(3)),

        // Order and types
        new("ShouldBeEnumerableTestExtensions.ShouldBeInOrder(IEnumerable<T>, String, String)",
            s => s.Of(1, 2, 3).ShouldBeInOrder(),
            s => s.Of(1, 3, 2).ShouldBeInOrder()),
        new("ShouldBeEnumerableTestExtensions.ShouldBeInOrder(IEnumerable<T>, SortDirection, String, String)",
            s => s.Of(3, 2, 1).ShouldBeInOrder(SortDirection.Descending),
            s => s.Of(3, 1, 2).ShouldBeInOrder(SortDirection.Descending)),
        new("ShouldBeEnumerableTestExtensions.ShouldBeInOrder(IEnumerable<T>, SortDirection, IComparer<T>, String, String)",
            s => s.Of(1, 2, 3).ShouldBeInOrder(SortDirection.Ascending, Comparer<int>.Default),
            s => s.Of(1, 3, 2).ShouldBeInOrder(SortDirection.Ascending, Comparer<int>.Default)),
        new("ShouldBeEnumerableTestExtensions.ShouldBeOfTypes(IEnumerable<T>, Type[], String, String)",
            s => s.Of<object>(1, "a").ShouldBeOfTypes([typeof(int), typeof(string)]),
            s => s.Of<object>(1, "a").ShouldBeOfTypes([typeof(string), typeof(int)])),
    ];

    private static KeyValuePair<string, int> Pair(string key, int value) => new(key, value);
}
