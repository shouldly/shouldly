using System.Collections;

namespace Shouldly.Tests.TestHelpers;

/// <summary>
/// A sequence that throws if it is enumerated a second time, to prove an assertion reads it only once.
/// </summary>
sealed class SinglePass<T>(params T[] items) : IEnumerable<T>
{
    private bool _enumerated;

    public IEnumerator<T> GetEnumerator()
    {
        if (_enumerated)
            throw new InvalidOperationException("This sequence can only be enumerated once.");

        _enumerated = true;
        return ((IEnumerable<T>)items).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
