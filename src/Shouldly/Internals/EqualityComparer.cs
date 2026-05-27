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
        if (s_useTypedEquatableFastPath)
        {
            // Value-type T with IEquatable<T> and no IEnumerable / float-tolerance
            // entanglement: BCL EqualityComparer<T>.Default dispatches to the
            // typed Equals(T) without boxing, matching the semantics the boxing
            // cascade below would produce for these types.
            return System.Collections.Generic.EqualityComparer<T>.Default.Equals(x!, y!);
        }

        if (ReferenceEquals(x, y))
            return true;

        if (x is null || y is null)
            return false;

        if (Numerics.IsNumericType(x) &&
            Numerics.IsNumericType(y))
        {
            var tolerance = Tolerance.Empty;
            return Numerics.AreEqual(x, y, ref tolerance);
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

        // Implements IEquatable<T>?
        if (x is IEquatable<T> equatable)
            return equatable.Equals(y);

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