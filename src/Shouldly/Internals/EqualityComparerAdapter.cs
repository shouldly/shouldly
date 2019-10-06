using System;
using System.Collections;
using System.Collections.Generic;

namespace Shouldly
{
    /*
     * Code heavily influenced by code from xunit assert equality comparer adapter
     * at https://github.com/xunit/xunit/blob/master/src/xunit2.assert/Asserts/Sdk/AssertEqualityComparerAdapter.cs
     */

    internal class EqualityComparerAdapter<T> : IEqualityComparer
    {
        private readonly IEqualityComparer<T> _innerComparer;

        public EqualityComparerAdapter(IEqualityComparer<T> innerComparer)
        {
            _innerComparer = innerComparer;
        }

        public new bool Equals(object? x, object? y)
        {
            return _innerComparer.Equals((T)x!, (T)y!);
        }

        public int GetHashCode(object obj)
        {
            throw new NotImplementedException();
        }
    }
}