using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using NUnit.Framework.Constraints;

namespace Shouldly
{
    /*
     * Code heavily influenced by code from xunit assert equality comparer
     * at https://github.com/xunit/xunit/blob/master/src/xunit2.assert/Asserts/Sdk/AssertEqualityComparer.cs
     */
    internal class EqualityComparer<T> : IEqualityComparer<T>
    {
        static readonly IEqualityComparer DefaultInnerComparer = new EqualityComparerAdapter<object>(new EqualityComparer<object>());
        static readonly Type NullableType = typeof(Nullable<>);

        readonly Func<IEqualityComparer> _innerComparerFactory;

        public EqualityComparer(IEqualityComparer? innerComparer = null)
        {
            // Use a thunk to delay evaluation of DefaultInnerComparer
            _innerComparerFactory = () => innerComparer ?? DefaultInnerComparer;
        }

        public bool Equals([AllowNull] T x, [AllowNull] T y)
        {
            var type = typeof(T);

            if (ReferenceEquals(x, y))
                return true;

            // Null?
            if (!type.IsValueType() || (type.IsGenericType() && type.GetGenericTypeDefinition().IsAssignableFrom(NullableType)))
            {
                if (object.Equals(x, null))
                    return object.Equals(y, null);

                if (object.Equals(y, null))
                    return false;
            }

            if (Numerics.IsNumericType(x) &&
                Numerics.IsNumericType(y))
            {
                var tolerance = Tolerance.Empty;
                return Numerics.AreEqual(x, y, ref tolerance);
            }

            // Enumerable?
            if (x!.TryGetEnumerable(out var enumerableX) &&
                y!.TryGetEnumerable(out var enumerableY))
            {
                var enumeratorX = enumerableX.GetEnumerator();
                var enumeratorY = enumerableY.GetEnumerator();
                var equalityComparer = _innerComparerFactory();

                while (true)
                {
                    var hasNextX = enumeratorX.MoveNext();
                    var hasNextY = enumeratorY.MoveNext();

                    if (!hasNextX || !hasNextY)
                        return (hasNextX == hasNextY);

                    if (!equalityComparer.Equals(enumeratorX.Current, enumeratorY.Current))
                        return false;
                }
            }

            // Implements IEquatable<T>?
            if (x is IEquatable<T> equatable)
                return equatable.Equals(y!);

            // Implements IComparable<T>?
            if (x is IComparable<T> comparableGeneric)
                return comparableGeneric.CompareTo(y!) == 0;

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

        public int GetHashCode([DisallowNull] T obj)
            => throw new NotImplementedException();
    }
}