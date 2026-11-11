namespace Shouldly.Internals.Failures;

/// <summary>
/// A dictionary should not have contained a value at a key.
/// </summary>
sealed class ValueForKeyFailure([CallerMemberName] string name = "") : Failure(name, negated: true)
{
    /// <summary>
    /// The key and the value it should not have held, and whether the key was there.
    /// </summary>
    public FailureMessage With(object? key, object? unexpectedValue, bool keyExists) =>
        new(this, sentence => Word(sentence, key, unexpectedValue, new(keyExists, null)));

    internal static string Word(Sentence sentence, object? key, object? expectedValue, DictionaryKeyLookup? lookup)
    {
        Debug.Assert(key is object);

        const string format =
            """
            {0}
                should not contain key
            {1}
                with value
            {2}
                {3}
            """;

        var codePart = sentence.Subject;
        var expected = expectedValue.ToStringAwesomely();
        var keyValue = key.ToStringAwesomely();

        if (lookup is { KeyExists: true })
            return string.Format(format, codePart, keyValue, expected, "but does");

        if (lookup is { KeyExists: false })
            return string.Format(format, codePart, keyValue, expected, "but the key does not exist");

        return string.Format(format, codePart, keyValue, expected, "").TrimEnd();
    }
}
