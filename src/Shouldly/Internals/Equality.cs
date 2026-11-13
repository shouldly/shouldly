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
        new(comparer, usesCustomComparer: true, hashes: true);

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
                    ? Numbers.EqualWithin(expectedNumber, actualNumber, tolerance)
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
                    ? Numbers.EqualWithin(expectedNumber, actualNumber, tolerance)
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
            caseSensitivity: caseSensitivity, hashes: true);

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

    /// <summary>
    /// Compares two <see cref="Memory{T}"/>s by what they hold: equal when their elements, compared by
    /// <paramref name="elements"/>, are equal in the same order. With Shouldly's own rules for the elements, an equality
    /// rule that covers both memories decides instead, as it does for memories inside a sequence.
    /// </summary>
    public static Equality<Memory<T>> OfMemories<T>(Equality<T> elements) =>
        new(new MemoryComparer<Memory<T>, T>(elements, static memory => memory),
            elements.Tolerance, elements.Case, elements.UsesCustomComparer, elements: elements);

    /// <summary>
    /// Compares two <see cref="ReadOnlyMemory{T}"/>s by what they hold, as <see cref="OfMemories{T}"/> compares two
    /// <see cref="Memory{T}"/>s.
    /// </summary>
    public static Equality<ReadOnlyMemory<T>> OfReadOnlyMemories<T>(Equality<T> elements) =>
        new(new MemoryComparer<ReadOnlyMemory<T>, T>(elements, static memory => memory),
            elements.Tolerance, elements.Case, elements.UsesCustomComparer, elements: elements);

    /// <summary>
    /// Whether two spans are equal by Shouldly's own rules: compared as the arrays of their elements, by
    /// <see cref="OfSequences{T}"/> with <see cref="Equality{T}.Default"/>, the Equality their failure message reads.
    /// </summary>
    /// <remarks>
    /// A span can't be boxed, so no equality rule can cover it, but one can cover the array of its elements, and then
    /// decides, in the check as in the message. Where none could, the spans are walked as they are, without copying.
    /// </remarks>
    public static bool SpansEqual<T>(ReadOnlySpan<T> actual, ReadOnlySpan<T> expected)
    {
        if (!EqualityRules.IsEmpty && EqualityRules.CouldApplyTo(typeof(T[])))
            return OfSequences(Equality<T>.Default).Equal(actual.ToArray(), expected.ToArray());

        return Equality<T>.Default.MatchInOrder(actual, expected);
    }

    /// <summary>
    /// Whether <paramref name="actual"/> and <paramref name="expected"/> hold equal items in the same order, by
    /// <paramref name="equal"/>, which is called as <c>equal(actual, expected)</c>. Each is read once, and no further
    /// than the first difference. The one walk of two sequences in order that a check takes, as
    /// <see cref="UnorderedCollection.MatchOneForOne{T}"/> is the one in any order.
    /// </summary>
    internal static bool MatchInOrder<T>(IEnumerable<T> actual, IEnumerable<T> expected, Func<T, T, bool> equal)
    {
        using var expectedEnum = expected.GetEnumerator();
        using var actualEnum = actual.GetEnumerator();

        while (true)
        {
            var expectedHasData = expectedEnum.MoveNext();
            var actualHasData = actualEnum.MoveNext();

            if (!expectedHasData || !actualHasData)
                return expectedHasData == actualHasData;

            if (!equal(actualEnum.Current, expectedEnum.Current))
                return false;
        }
    }

    /// <summary>
    /// Whether <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/> (and so its hashing) gives the same
    /// answers as Shouldly's own rules for values of <paramref name="type"/>, when no equality rule applies: true for a
    /// closed list of BCL types whose <c>Equals</c> the rules reach unchanged. Not for the floating-point types, which
    /// honour <see cref="ShouldlyConfiguration.DefaultFloatingPointTolerance"/> (see
    /// <see cref="Numbers.HonoursDefaultTolerance"/>), nor for <c>object</c> or user types, whose values may compare
    /// across types, structurally or through <see cref="IEquatable{T}"/>/<see cref="IComparable{T}"/> without a
    /// matching <c>GetHashCode</c>.
    /// </summary>
    private protected static bool DefaultComparerAgreesFor(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;
        return type.IsEnum ||
               type == typeof(string) ||
               type == typeof(bool) ||
               type == typeof(char) ||
               type == typeof(sbyte) ||
               type == typeof(byte) ||
               type == typeof(short) ||
               type == typeof(ushort) ||
               type == typeof(int) ||
               type == typeof(uint) ||
               type == typeof(long) ||
               type == typeof(ulong) ||
               type == typeof(decimal) ||
               type == typeof(Guid) ||
               type == typeof(DateTime) ||
               type == typeof(DateTimeOffset) ||
               type == typeof(TimeSpan);
    }

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

    // Generic over the memory type, so that a rule registered for Memory<T> or for ReadOnlyMemory<T> sees the value
    // it was registered for.
    private sealed class MemoryComparer<TMemory, T>(Equality<T> elements, Func<TMemory, ReadOnlyMemory<T>> contents)
        : IEqualityComparer<TMemory>
        where TMemory : struct
    {
        public bool Equals(TMemory expected, TMemory actual)
        {
            if (ReferenceEquals(elements, Equality<T>.Default) &&
                EqualityStep.ByRule(expected, actual) is { } step)
                return step.Verdict;

            return elements.MatchInOrder(contents(actual).Span, contents(expected).Span);
        }

        public int GetHashCode(TMemory obj) =>
            throw new NotSupportedException("Memories compared by Shouldly's equality can't be hashed.");
    }

    private sealed class SequenceComparer<T>(Equality<T> elements, bool ignoreOrder) : IEqualityComparer<IEnumerable<T>>
    {
        public bool Equals(IEnumerable<T>? expected, IEnumerable<T>? actual)
        {
            actual = NullEquivalence.Normalize(actual);
            expected = NullEquivalence.Normalize(expected);

            if (actual == null || expected == null)
                return actual == null && expected == null;

            bool anyOrder;
            if (ignoreOrder)
            {
                anyOrder = true;
            }
            else if (ReferenceEquals(elements, Equality<T>.Default))
            {
                // Shouldly's own rules take the step they take for any pair, so a rule that covers both sequences
                // decides, as one value, before they are walked. Their elements are then walked here rather than as
                // objects, so each keeps its declared type.
                var step = EqualityStep.For(expected, actual);
                if (step.Kind is EqualityStepKind.Rule or EqualityStepKind.Json)
                    return step.Verdict;

                anyOrder = step.Kind == EqualityStepKind.UnorderedCollection;
            }
            else
            {
                // The order a dictionary or a set enumerates in isn't part of its value (#1430).
                anyOrder = UnorderedCollection.IgnoresOrder(actual, expected);
            }

            return anyOrder
                ? UnorderedCollection.MatchOneForOne(actual, expected, (a, e) => elements.Equal(a, e))
                : MatchInOrder(actual, expected, (a, e) => elements.Equal(a, e));
        }

        public int GetHashCode(IEnumerable<T> obj) =>
            throw new NotSupportedException("Sequences compared by Shouldly's equality can't be hashed.");
    }
}

/// <summary>
/// How an assertion compares two values of type <typeparamref name="T"/>. See <see cref="Equality"/>.
/// </summary>
sealed class Equality<T> : Equality
{
    private static readonly bool s_defaultComparerAgrees = DefaultComparerAgreesFor(typeof(T));

    private readonly IEqualityComparer<T> _comparer;
    private readonly bool _hashes;

    internal Equality(IEqualityComparer<T> comparer, object? tolerance = null, Case? caseSensitivity = null,
        bool usesCustomComparer = false, bool ignoresOrder = false, Equality? elements = null, bool hashes = false)
        : base(tolerance, caseSensitivity, usesCustomComparer, ignoresOrder, elements)
    {
        _comparer = comparer;
        _hashes = hashes;
    }

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

    /// <summary>
    /// A comparer that gives the same answers as <see cref="Equal"/> and can hash, so a set check can match in one
    /// pass rather than pairwise; null when there's none. A comparer of the caller's is it. For Shouldly's own rules it
    /// is <see cref="System.Collections.Generic.EqualityComparer{T}.Default"/>, for the types whose own equality the
    /// rules reach unchanged, while no equality rule could apply to them.
    /// </summary>
    public IEqualityComparer<T>? HashingComparer =>
        _hashes ? _comparer
        : ReferenceEquals(this, Default) && s_defaultComparerAgrees &&
          (EqualityRules.IsEmpty || !EqualityRules.CouldApplyTo(typeof(T)))
            ? System.Collections.Generic.EqualityComparer<T>.Default
            : null;

    /// <summary>
    /// Whether <paramref name="actual"/> and <paramref name="expected"/> hold equal elements, compared by this, in the
    /// same order: <see cref="Equality.MatchInOrder{T}(IEnumerable{T}, IEnumerable{T}, Func{T, T, bool})"/> for spans, which can't be enumerated as sequences.
    /// </summary>
    public bool MatchInOrder(ReadOnlySpan<T> actual, ReadOnlySpan<T> expected)
    {
        if (actual.Length != expected.Length)
            return false;

#if NET8_0_OR_GREATER
        // A comparer that agrees and can hash can also be handed to the BCL, which compares the types whose equality
        // is bitwise without a call per element. Called on the expected span, so Equals is called on its elements.
        if (HashingComparer is { } agreeing)
            return expected.SequenceEqual(actual, agreeing);
#endif

        for (var i = 0; i < actual.Length; i++)
        {
            if (!Equal(actual[i], expected[i]))
                return false;
        }

        return true;
    }

    public override bool EqualObjects(object? actual, object? expected) => Equal((T?)actual, (T?)expected);
}
