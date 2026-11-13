namespace Shouldly.Internals;

/// <summary>
/// Shouldly's single definition of when an actual value equals an expected one. The assertions that compare values
/// (<c>ShouldBe</c>, <c>ShouldNotBe</c>, sequences, membership, dictionary values and key/value pairs) ask it rather
/// than assembling their own comparer, so the rules (null equivalence, equality rules registered with
/// <see cref="ShouldlyConfiguration.UseEquality{T}(Func{T, T, bool})"/>, numerics, key/value pairs and tuples,
/// <see cref="IEquatable{T}"/>, <c>Equals</c> on the expected value) are decided in one place.
/// </summary>
/// <remarks>
/// <para>
/// An assertion holds an Equality for how it compares: Shouldly's own rules (<see cref="Equality{T}.Default"/>), a
/// comparer of the caller's (<see cref="Using{T}"/>), the element comparer a tolerance or case option stands for
/// (<see cref="WithinTolerance{T}(decimal)"/>, <see cref="ForCase"/>), and for two sequences whether their elements
/// are matched in any order (<see cref="OfSequences{T}"/>). The check asks <see cref="Equality{T}.Equal"/>, and the
/// failure message reads the same Equality through this class, so it describes the comparison that failed.
/// </para>
/// <para>
/// Every comparison calls the comparer as <c>comparer.Equals(expected, actual)</c>, so the default rules call
/// <c>Equals</c> on the expected value: a broken <c>Equals</c> on the value under test can't make an assertion pass
/// (issue #1076).
/// </para>
/// </remarks>
abstract class Equality
{
    private protected Equality(object? tolerance, Case? caseSensitivity, bool usesCustomComparer, bool ignoresOrder, Equality? elements)
    {
        Tolerance = tolerance;
        Case = caseSensitivity;
        UsesCustomComparer = usesCustomComparer;
        IgnoresOrder = ignoresOrder;
        Elements = elements;
    }

    /// <summary>The tolerance it compares numbers within, when the assertion took one.</summary>
    public object? Tolerance { get; }

    /// <summary>The case option it compares strings under, when the assertion took one.</summary>
    public Case? Case { get; }

    /// <summary>Whether the caller supplied the comparer, so the rules it compares by are theirs and not Shouldly's.</summary>
    public bool UsesCustomComparer { get; }

    /// <summary>Whether the caller asked for the elements of two sequences to be matched in any order.</summary>
    public bool IgnoresOrder { get; }

    /// <summary>
    /// How it compares the elements of two sequences, when it compares sequences; null when it compares two values
    /// whole.
    /// </summary>
    public Equality? Elements { get; }

    /// <summary>Whether a comparer of the caller's compared two values whole, so none of Shouldly's rules decided.</summary>
    public bool ComparesValuesItself => UsesCustomComparer && Elements is null;

    /// <summary>Whether a comparer of the caller's compared the elements of two sequences.</summary>
    public bool ComparesElementsItself => UsesCustomComparer && Elements is not null;

    /// <summary>
    /// Whether two values of the type it compares are equal, for code that holds them as objects, such as a failure
    /// message.
    /// </summary>
    public abstract bool EqualObjects(object? actual, object? expected);

    /// <summary>
    /// Whether two items of the values it compares are equal: by <see cref="Elements"/> when it compares sequences,
    /// and otherwise by Shouldly's own rules.
    /// </summary>
    public bool ElementsEqual(object? actual, object? expected) =>
        Elements is { } elements
            ? elements.EqualObjects(actual, expected)
            : Equality<object?>.Default.Equal(actual, expected);

    /// <summary>
    /// What tells <paramref name="actual"/> apart from <paramref name="expected"/> as this compares them, for the
    /// failure message of a check that found them unequal, or equal when they shouldn't have been.
    /// </summary>
    public Differences DifferencesBetween(object? actual, object? expected) => new(this, actual, expected);

    /// <summary>
    /// Compares by <paramref name="comparer"/>, a comparer of the caller's, alone.
    /// </summary>
    public static Equality<T> Using<T>(IEqualityComparer<T> comparer) =>
        new(comparer, usesCustomComparer: true);

    /// <summary>
    /// The element equality of an assertion that takes a <see cref="decimal"/> tolerance: two decimals are equal when
    /// they are within <paramref name="tolerance"/> of each other. Any other pair is compared by Shouldly's own rules,
    /// as a custom assertion's message may hold values of another type than its tolerance. It can't hash, and it
    /// isn't transitive, which <see cref="OfSequences{T}"/> allows for when it matches in any order.
    /// </summary>
    public static Equality<T> WithinTolerance<T>(decimal tolerance)
    {
        var otherwise = Equality<T>.Default;
        return new(
            new ElementComparer<T>((expected, actual) =>
                expected is decimal expectedNumber && actual is decimal actualNumber
                    ? Is.Equal(actualNumber, expectedNumber, tolerance)
                    : otherwise.Equal(actual, expected)),
            tolerance: tolerance);
    }

    /// <summary>
    /// The element equality of an assertion that takes a <see cref="double"/> tolerance: two doubles or floats are
    /// equal when they are within <paramref name="tolerance"/> of each other, a float widened to a double. Like
    /// <see cref="WithinTolerance{T}(decimal)"/>, it compares any other pair by Shouldly's own rules, can't hash and
    /// isn't transitive.
    /// </summary>
    public static Equality<T> WithinTolerance<T>(double tolerance)
    {
        var otherwise = Equality<T>.Default;
        return new(
            new ElementComparer<T>((expected, actual) =>
                AsDouble(expected) is { } expectedNumber && AsDouble(actual) is { } actualNumber
                    ? Is.Equal(actualNumber, expectedNumber, tolerance)
                    : otherwise.Equal(actual, expected)),
            tolerance: tolerance);
    }

    /// <summary>
    /// The equality of an assertion on dates and times that takes a <see cref="TimeSpan"/> tolerance: two
    /// <see cref="DateTime"/>s, <see cref="DateTimeOffset"/>s or <see cref="TimeSpan"/>s are equal when they are within
    /// <paramref name="tolerance"/> of each other. Any other pair is compared by Shouldly's own rules.
    /// </summary>
    public static Equality<T> WithinTolerance<T>(TimeSpan tolerance)
    {
        var otherwise = Equality<T>.Default;
        return new(
            new ElementComparer<T>((expected, actual) =>
                (expected, actual) switch
                {
                    (DateTime expectedTime, DateTime actualTime) => Is.Equal(actualTime, expectedTime, tolerance),
                    (DateTimeOffset expectedTime, DateTimeOffset actualTime) => Is.Equal(actualTime, expectedTime, tolerance),
                    (TimeSpan expectedSpan, TimeSpan actualSpan) => Is.Equal(actualSpan, expectedSpan, tolerance),
                    _ => otherwise.Equal(actual, expected)
                }),
            tolerance: tolerance);
    }

    /// <summary>
    /// The element equality of an assertion on strings that takes a <see cref="Shouldly.Case"/> option: ordinal,
    /// ignoring case or not.
    /// </summary>
    public static Equality<string> ForCase(Case caseSensitivity) =>
        new(caseSensitivity == Shouldly.Case.Insensitive ? StringComparer.OrdinalIgnoreCase : StringComparer.Ordinal,
            caseSensitivity: caseSensitivity);

    /// <summary>
    /// Compares two sequences: equal when they hold equal elements, compared by <paramref name="elements"/>, in the same
    /// order, or, with <paramref name="ignoreOrder"/>, or when they are dictionaries or sets whose order isn't compared
    /// (see <see cref="UnorderedCollection"/>), matched one for one in any order. A default
    /// <c>ImmutableArray&lt;T&gt;</c> counts as null, and null equals only null.
    /// </summary>
    /// <remarks>
    /// With Shouldly's own rules for the elements, an equality rule that applies to both sequences as whole values
    /// decides instead, so a type such as <c>JToken</c>, which enumerates its children, isn't compared by shape alone.
    /// With <paramref name="ignoreOrder"/> the caller has asked for the elements to be matched in any order, which a
    /// whole-value rule (such as the order-sensitive <c>JToken.DeepEquals</c>) can't honour, so the elements are
    /// matched instead, each by its rules.
    /// </remarks>
    public static Equality<IEnumerable<T>> OfSequences<T>(Equality<T> elements, bool ignoreOrder = false) =>
        ReferenceEquals(elements, Equality<T>.Default) && !ignoreOrder
            ? DefaultSequences<T>.Instance
            : new(new SequenceComparer<T>(elements, ignoreOrder),
                elements.Tolerance, elements.Case, elements.UsesCustomComparer, ignoreOrder, elements);

    private static double? AsDouble<T>(T value) =>
        value switch
        {
            double number => number,
            float number => number,
            _ => null
        };

    private static class DefaultSequences<T>
    {
        public static readonly Equality<IEnumerable<T>> Instance =
            new(new SequenceComparer<T>(Equality<T>.Default, ignoreOrder: false), elements: Equality<T>.Default);
    }

    // Called as equal(expected, actual), as every comparer here is (#1076).
    private sealed class ElementComparer<T>(Func<T?, T?, bool> equal) : IEqualityComparer<T>
    {
        public bool Equals(T? x, T? y) => equal(x, y);

        public int GetHashCode(T obj) =>
            throw new NotSupportedException("Values within a tolerance of each other can't be hashed alike.");
    }

    private sealed class SequenceComparer<T>(Equality<T> elements, bool ignoreOrder) : IEqualityComparer<IEnumerable<T>>
    {
        public bool Equals(IEnumerable<T>? expected, IEnumerable<T>? actual)
        {
            actual = NullEquivalence.Normalize(actual);
            expected = NullEquivalence.Normalize(expected);

            if (actual == null || expected == null)
                return actual == null && expected == null;

            if (ReferenceEquals(elements, Equality<T>.Default) && !ignoreOrder &&
                EqualityRules.TryEquals(expected, actual, out var ruleResult))
                return ruleResult;

            // The order a dictionary or a set enumerates in isn't part of its value (#1430).
            return ignoreOrder || UnorderedCollection.IgnoresOrder(actual, expected)
                ? UnorderedCollection.MatchOneForOne(actual, expected, (a, e) => elements.Equal(a, e))
                : MatchInOrder(actual, expected);
        }

        public int GetHashCode(IEnumerable<T> obj) =>
            throw new NotSupportedException("Sequences compared by Shouldly's equality can't be hashed.");

        private bool MatchInOrder(IEnumerable<T> actual, IEnumerable<T> expected)
        {
            using var expectedEnum = expected.GetEnumerator();
            using var actualEnum = actual.GetEnumerator();

            while (true)
            {
                var expectedHasData = expectedEnum.MoveNext();
                var actualHasData = actualEnum.MoveNext();

                if (!expectedHasData || !actualHasData)
                    return expectedHasData == actualHasData;

                if (!elements.Equal(actualEnum.Current, expectedEnum.Current))
                    return false;
            }
        }
    }
}

/// <summary>
/// How an assertion compares two values of type <typeparamref name="T"/>. See <see cref="Equality"/>.
/// </summary>
sealed class Equality<T> : Equality
{
    private readonly IEqualityComparer<T> _comparer;

    internal Equality(IEqualityComparer<T> comparer, object? tolerance = null, Case? caseSensitivity = null,
        bool usesCustomComparer = false, bool ignoresOrder = false, Equality? elements = null)
        : base(tolerance, caseSensitivity, usesCustomComparer, ignoresOrder, elements) =>
        _comparer = comparer;

    /// <summary>
    /// Shouldly's own rules.
    /// </summary>
    public static Equality<T> Default { get; } = new(new EqualityComparer<T>());

    /// <summary>
    /// Whether <paramref name="actual"/> equals <paramref name="expected"/>.
    /// </summary>
    public bool Equal(T? actual, T? expected) => _comparer.Equals(expected!, actual!);

    /// <summary>
    /// The comparer behind <see cref="Equal"/>, for code that hands one on. Shouldly's own can't hash:
    /// <see cref="IEqualityComparer{T}.GetHashCode(T)"/> throws.
    /// </summary>
    public IEqualityComparer<T> Comparer => _comparer;

    public override bool EqualObjects(object? actual, object? expected) => Equal((T?)actual, (T?)expected);
}
