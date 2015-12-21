using System;
using System.Collections;
using System.Collections.Generic;
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

        public EqualityComparer(IEqualityComparer innerComparer = null)
        {
            // Use a thunk to delay evaluation of DefaultInnerComparer
            _innerComparerFactory = () => innerComparer ?? DefaultInnerComparer;
        }

        public bool Equals(T x, T y)
        {
            var type = typeof (T);

            if (ReferenceEquals(x, y))
                return true;

            // Null?
#if DOTNET5_4
            var typeInfo = type.GetTypeInfo();
            if (!typeInfo.IsValueType || (typeInfo.IsGenericType && type.GetGenericTypeDefinition().IsAssignableFrom(NullableType)))
#else
            if (!type.IsValueType || (type.IsGenericType && type.GetGenericTypeDefinition().IsAssignableFrom(NullableType)))
#endif
            {
                if (object.Equals(x, default(T)))
                    return object.Equals(y, default(T));

                if (object.Equals(y, default(T)))
                    return false;
            }

            if (Numerics.IsNumericType(x) && Numerics.IsNumericType(y))
            {
                var tollerance = Tolerance.Empty;
                return Numerics.AreEqual(x, y, ref tollerance);
            }

            // Implements IEquatable<T>?
            var equatable = x as IEquatable<T>;
            if (equatable != null)
                return equatable.Equals(y);

            // Implements IComparable<T>?
            var comparableGeneric = x as IComparable<T>;
            if (comparableGeneric != null)
                return comparableGeneric.CompareTo(y) == 0;

            // Implements IComparable?
            var comparable = x as IComparable;
            if (comparable != null)
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

            // Enumerable?
            var enumerableX = x as IEnumerable;
            var enumerableY = y as IEnumerable;

            if (enumerableX != null && enumerableY != null)
            {
                var enumeratorX = enumerableX.GetEnumerator();
                var enumeratorY = enumerableY.GetEnumerator();
                var equalityComparer = _innerComparerFactory();

                while (true)
                {
                    bool hasNextX = enumeratorX.MoveNext();
                    bool hasNextY = enumeratorY.MoveNext();

                    if (!hasNextX || !hasNextY)
                        return (hasNextX == hasNextY);

                    if (!equalityComparer.Equals(enumeratorX.Current, enumeratorY.Current))
                        return false;
                }
            }

            // Last case, rely on Object.Equals
            return Object.Equals(x, y);
        }

        public int GetHashCode(T obj)
        {
            throw new NotImplementedException();
        }
    }
}