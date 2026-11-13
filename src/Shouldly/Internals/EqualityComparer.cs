namespace Shouldly.Internals;

/*
 * Code heavily influenced by code from xunit assert equality comparer
 * at https://github.com/xunit/xunit/blob/master/src/xunit2.assert/Asserts/Sdk/AssertEqualityComparer.cs
 */
class EqualityComparer<T> : IEqualityComparer<T>
{
    // Compares the items of two sequences, and of two tuples, as values of any type.
    private static readonly IEqualityComparer<object?> ItemComparer = new EqualityComparer<object?>();

    // Static per-T strategy: decided once at first use, then cached. The CLR
    // specialises this generic class per closed T, so each closed instantiation
    // gets its own field initialised exactly once. JITs treat the load as a
    // constant after init, so the branch in Equals folds to the chosen path.
    private static readonly bool s_useTypedEquatableFastPath = ShouldUseTypedEquatableFastPath();

    private static bool ShouldUseTypedEquatableFastPath()
    {
        var t = typeof(T);

        // Reference types already avoid boxing on every branch of Equals
        // below — the gain is exclusive to value-type T.
        if (!t.IsValueType) return false;

        // System.Collections.Generic.EqualityComparer<T>.Default doesn't know
        // ShouldlyConfiguration.DefaultFloatingPointTolerance, so the types it
        // reaches stay on the boxing path, where Numbers compares them.
        if (Numbers.HonoursDefaultTolerance(t)) return false;

        // Value types that are themselves IEnumerable (e.g. ImmutableArray<T>)
        // currently recurse into element-wise comparison via TryGetEnumerable.
        // Short-circuiting to IEquatable<T> would compare the wrapper instead
        // (e.g. underlying array reference) — a behaviour change.
        if (typeof(IEnumerable).IsAssignableFrom(t)) return false;

        // Memory<T>/ReadOnlyMemory<T> aren't IEnumerable, but the cascade below walks their contents too
        // (TryGetEnumerable), each element by its rules. Their IEquatable<T> compares which array they wrap instead.
        if (t.IsGenericType &&
            t.GetGenericTypeDefinition() is var definition &&
            (definition == typeof(Memory<>) || definition == typeof(ReadOnlyMemory<>)))
            return false;

        // A ValueTuple's IEquatable<T> compares a collection-typed item by reference. The cascade below compares
        // its items, each by its rules (#1429).
        if (Tuples.ComparesItemByItem(t)) return false;

        // Need a typed Equals to dispatch through without boxing.
        return typeof(IEquatable<T>).IsAssignableFrom(t);
    }

    public bool Equals(T? x, T? y)
    {
        if (s_useTypedEquatableFastPath && (EqualityRules.IsEmpty || !EqualityRules.CouldApplyTo(typeof(T))))
        {
            // Value-type T with IEquatable<T> and no IEnumerable / float-tolerance
            // entanglement: BCL EqualityComparer<T>.Default dispatches to the
            // typed Equals(T) without boxing, matching the semantics the boxing
            // cascade below would produce for these types.
            return System.Collections.Generic.EqualityComparer<T>.Default.Equals(x!, y!);
        }

        if (ReferenceEquals(x, y))
            return true;

        // A default ImmutableArray<T> counts as null: it equals null or another default, and is never enumerated (#1406).
        if (NullEquivalence.IsNull(x))
            return NullEquivalence.IsNull(y);

        if (NullEquivalence.IsNull(y))
            return false;

        // x is the expected value: callers compare as Equals(expected, actual).
        var step = EqualityStep.For(x, y);
        switch (step.Kind)
        {
            case EqualityStepKind.Rule:
            case EqualityStepKind.Json:
                return step.Verdict;

            case EqualityStepKind.String:
                return string.Equals((string)(object)x, (string)(object)y);

            case EqualityStepKind.Number:
                return Numbers.Equal(x, y);

            case EqualityStepKind.UnorderedCollection:
                return UnorderedCollection.MatchOneForOne(
                    step.ActualItems!.Cast<object?>(),
                    step.ExpectedItems!.Cast<object?>(),
                    (actualItem, expectedItem) => ItemComparer.Equals(expectedItem, actualItem));

            case EqualityStepKind.Sequence:
                return Equality.MatchInOrder(
                    step.ActualItems!.Cast<object?>(),
                    step.ExpectedItems!.Cast<object?>(),
                    (actualItem, expectedItem) => ItemComparer.Equals(expectedItem, actualItem));

            case EqualityStepKind.Tuple:
                // Each item compared by Shouldly's equality, not by the tuple's own Equals, which compares a
                // collection by reference.
                Tuples.TryEquals(x, y, ItemComparer, out var tupleResult);
                return tupleResult;

            default:
                return OwnEquality(x!, y!);
        }
    }

    // The last step: what the type itself defines.
    private static bool OwnEquality([DisallowNull] T x, [DisallowNull] T y)
    {
        // A type's own equality decides before CompareTo, which only says how to sort: a type may sort by one key and
        // be equal by all its fields (#1428). Each step looks at what the runtime type defines, so ShouldBe<Person>
        // and ShouldBe<object> agree.

        // Implements IEquatable<T>?
        if (x is IEquatable<T> equatable)
            return equatable.Equals(y);

        // Implements IEquatable<U> for the runtime types? Reached when T has lost the element type — the elements
        // of a collection compared through the object comparer, or ShouldBe<object> — so a typed Equals isn't
        // ignored in favour of reference equality (#825).
        if (RuntimeEquatable.TryEquals(x, y, out var runtimeEquatableResult))
            return runtimeEquatableResult;

        // Overrides Equals(object)?
        if (OverriddenEquals.IsDefinedBy(x))
            return x.Equals(y);

        // Only a type that doesn't define equality is compared with CompareTo.

        // Implements IComparable<T>?
        if (x is IComparable<T> comparableGeneric)
            return comparableGeneric.CompareTo(y) == 0;

        // Implements IComparable?
        if (x is IComparable comparable)
        {
            try
            {
                return comparable.CompareTo(y) == 0;
            }
            catch (ArgumentException)
            {
                // Thrown when two comparable types are not compatible, i.e string and int
            }
        }

        // Last case, rely on Object.Equals
        return object.Equals(x, y);
    }

    public int GetHashCode(T obj) => throw new NotImplementedException();
}