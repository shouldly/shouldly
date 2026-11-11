using System.Collections;
using Shouldly.Internals.Failures;

namespace Shouldly.Tests.InternalTests;

/// <summary>
/// The dictionary failures word what the assertion found: they are told whether the key was there, and never look
/// it up again.
/// </summary>
public class DictionaryFailureTests
{
    // The negative message only needs to know the key is present, so it must not read the value through the
    // indexer.
    [Fact]
    public void ShouldNotContainValueForKeyDoesNotReadTheValue()
    {
        var dictionary = new HashtableWithThrowingIndexer { ["a"] = 1 };

        var message = Fail.ShouldNotContainValueForKey.With("a", 1, keyExists: true).Text(dictionary, null, "dictionary");

        message.ShouldContain("but does");
    }

    [Fact]
    public void ShouldNotContainValueForKeyReportsAMissingKey()
    {
        IEnumerable<KeyValuePair<string, int>> pairs = new List<KeyValuePair<string, int>> { new("a", 1) };

        var message = Fail.ShouldNotContainValueForKey.With("z", 1, keyExists: false).Text(pairs, null, "pairs");

        message.ShouldEndWith("but the key does not exist");
    }

    private sealed class HashtableWithThrowingIndexer : Hashtable
    {
        public override object? this[object key]
        {
            get => throw new InvalidOperationException("The value should not be read.");
            set => base[key] = value;
        }
    }
}
