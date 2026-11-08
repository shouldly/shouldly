namespace Shouldly.Internals;

/// <summary>
/// Whether a dictionary assertion found its key, and the value it found for it.
/// </summary>
sealed class DictionaryKeyLookup(bool keyExists, object? value)
{
    public bool KeyExists { get; } = keyExists;

    public object? Value { get; } = value;

    /// <summary>
    /// The lookup the assertion carried on its context, or, for a message built outside Shouldly's own
    /// dictionary assertions, a lookup through the non-generic <see cref="IDictionary"/>. Pass
    /// <paramref name="readValue"/> as <c>false</c> when the message only needs <see cref="KeyExists"/>, so the
    /// fallback does not call the dictionary's indexer.
    /// </summary>
    public static DictionaryKeyLookup? From(IShouldlyAssertionContext context, bool readValue = true) =>
        context switch
        {
            ShouldlyAssertionContext { KeyLookup: { } lookup } => lookup,
            { Actual: IDictionary dictionary, Key: { } key } when dictionary.Contains(key) =>
                new(true, readValue ? dictionary[key] : null),
            { Actual: IDictionary } => new(false, null),
            _ => null
        };
}
