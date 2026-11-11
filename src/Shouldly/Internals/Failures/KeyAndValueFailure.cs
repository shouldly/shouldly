namespace Shouldly.Internals.Failures;

/// <summary>
/// A dictionary should have contained a key with a value.
/// </summary>
sealed class KeyAndValueFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    /// <summary>
    /// The key and value it should have contained, whether the key was there, and the value found at it.
    /// </summary>
    public FailureMessage With(object? key, object? expectedValue, bool keyExists, object? valueAtKey) =>
        new(this, sentence => Word(sentence, key, expectedValue, new(keyExists, valueAtKey)));

    internal static string Word(Sentence sentence, object? key, object? expectedValue, DictionaryKeyLookup? lookup)
    {
        Debug.Assert(key is object);

        const string format =
            """
            {0}
                should contain key
            {1}
                with value
            {2}
            {3}
            """;

        var subject = sentence.Subject;
        var expected = expectedValue.ToStringAwesomely();
        var keyValue = key.ToStringAwesomely();

        if (lookup is { KeyExists: true })
        {
            var actualValueString = lookup.Value.ToStringAwesomely();
            var valueString =
                $"""
                     but value was
                 {actualValueString}
                 """;
            return string.Format(format, subject, keyValue, expected, valueString);
        }

        if (lookup is { KeyExists: false })
            return string.Format(format, subject, keyValue, expected, "    but the key does not exist");

        return string.Format(format, subject, keyValue, expected, "").TrimEnd();
    }
}
