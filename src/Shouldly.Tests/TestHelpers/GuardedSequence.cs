using System.Collections;

namespace Shouldly.Tests.TestHelpers;

// An effectively infinite sequence (0, 1, 2, ...) that records how many elements were read
// and throws once a generous guard is passed, so a regression that enumerates the whole
// sequence fails fast instead of hanging the test run.
sealed class GuardedSequence(int guard = 100_000) : IEnumerable<int>
{
    public int ElementsRead { get; private set; }

    public bool Disposed { get; private set; }

    public IEnumerator<int> GetEnumerator()
    {
        try
        {
            for (var i = 0; ; i++)
            {
                if (i >= guard)
                    throw new InvalidOperationException($"Read more than {guard} elements of an infinite sequence.");

                ElementsRead = Math.Max(ElementsRead, i + 1);
                yield return i;
            }
        }
        finally
        {
            Disposed = true;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
