namespace Shouldly.Internals;

/// <summary>
/// Whether a dictionary assertion found its key, and the value it found for it.
/// </summary>
sealed class DictionaryKeyLookup(bool keyExists, object? value)
{
    public bool KeyExists { get; } = keyExists;

    public object? Value { get; } = value;
}
