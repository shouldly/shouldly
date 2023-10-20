using System.Collections;

namespace Shouldly;

/*
 * Code heavily influenced by code from xunit assert equality comparer adapter
 * at https://github.com/xunit/xunit/blob/master/src/xunit2.assert/Asserts/Sdk/AssertEqualityComparerAdapter.cs
 */
class EqualityComparerAdapter : IEqualityComparer
{
    private readonly IEqualityComparer<object> _innerComparer;

    public EqualityComparerAdapter(IEqualityComparer<object> innerComparer)
    {
        _innerComparer = innerComparer;
    }

    public new bool Equals(object? x, object? y)
    {
        return _innerComparer.Equals(x, y);
    }

    public int GetHashCode(object obj)
    {
        throw new NotImplementedException();
    }
}