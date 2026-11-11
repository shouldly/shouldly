using NUnit.Framework.Constraints;

namespace Shouldly.Internals;

/*
 * Code heavily influenced by code from xunit assert equality comparer
 * at https://github.com/xunit/xunit/blob/master/src/xunit2.assert/Asserts/Sdk/AssertEqualityComparer.cs
 */
class EqualityComparer<T> : IEqualityComparer<T>
{
    private static readonly IEqualityComparer DefaultInnerComparer = new EqualityComparerAdapter(new EqualityComparer<object>());

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

        // float/double/Half participate in
        // ShouldlyConfiguration.DefaultFloatingPointTolerance via Numerics.AreEqual.
        // System.Collections.Generic.EqualityComparer<T>.Default doesn't know
        // about that configuration, so keep them on the boxing path to preserve
        // tolerance semantics.
        if (t == typeof(float) || t == typeof(double)) return false;
#if NET5_0_OR_GREATER
        if (t == typeof(Half)) return false;
#endif

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

        // Need a typed Equals to dispatch through without boxing.
        return typeof(IEquatable<T>).IsAssignableFrom(t);
    }

    private readonly Func<IEqualityComparer> _innerComparerFactory;

    public EqualityComparer(IEqualityComparer? innerComparer = null)
    {
        // Use a thunk to delay evaluation of DefaultInnerComparer
        _innerComparerFactory = () => innerComparer ?? DefaultInnerComparer;
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

        // A registered equality rule decides before anything else, so a value it covers is never walked (x is the
        // expected value: callers compare as Equals(expected, actual)).
        if (EqualityRules.TryEquals(x, y, out var ruleResult))
            return ruleResult;

        // Strings are compared with their own Equals rather than walked as sequences of characters.
        if (x is string xString && y is string yString)
            return string.Equals(xString, yString);

        if (Numerics.IsNumericType(x) &&
            Numerics.IsNumericType(y))
        {
            return Numerics.AreEqual(x, y);
        }

        // Enumerable?
        if (x.TryGetEnumerable(out var enumerableX) &&
            y.TryGetEnumerable(out var enumerableY))
        {
            var enumeratorX = enumerableX.GetEnumerator();
            using var enumeratorXDispose = enumeratorX as IDisposable;
            var enumeratorY = enumerableY.GetEnumerator();
            using var enumeratorYDispose = enumeratorY as IDisposable;
            var equalityComparer = _innerComparerFactory();

            while (true)
            {
                var hasNextX = enumeratorX.MoveNext();
                var hasNextY = enumeratorY.MoveNext();

                if (!hasNextX || !hasNextY)
                    return hasNextX == hasNextY;

                if (!equalityComparer.Equals(enumeratorX.Current, enumeratorY.Current))
                    return false;
            }
        }

        // KeyValuePair? Compare its key and value, so a dictionary's entries compare like its values do under
        // ShouldBe and ShouldContainKeyAndValue, not by ValueType.Equals (#1408).
        if (KeyValuePairs.TryEquals(x, y, _innerComparerFactory(), out var keyValuePairResult))
            return keyValuePairResult;

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