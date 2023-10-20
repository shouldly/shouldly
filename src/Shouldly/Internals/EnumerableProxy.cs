using System.Collections;

namespace Shouldly.Internals;

interface IEnumerableProxy
{
    object ProxiedValue { get; }
}

sealed class EnumerableProxy<T> : IEnumerable<T>, IEnumerableProxy
{
    public static IEnumerable<T>? WrapNonCollection(IEnumerable<T>? baseEnum)
    {
        if (baseEnum is (null or IReadOnlyCollection<T> or ICollection<T> or ICollection))
        {
            return baseEnum;
        }

        if (baseEnum is EnumerableProxy<T>)
        {
            throw new ArgumentException("Value already wrapped.", nameof(baseEnum));
        }

        return new EnumerableProxy<T>(baseEnum, baseEnum.ToList());
    }

    public IEnumerable<T> ProxiedValue { get; }
    object IEnumerableProxy.ProxiedValue => ProxiedValue;

    private readonly IEnumerable<T> _baseReentrable;

    private EnumerableProxy(IEnumerable<T> baseEnum, IEnumerable<T> baseReentrable)
    {
        ProxiedValue = baseEnum;
        _baseReentrable = baseReentrable;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _baseReentrable.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}