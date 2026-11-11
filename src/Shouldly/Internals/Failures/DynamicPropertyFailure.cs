namespace Shouldly.Internals.Failures;

/// <summary>
/// A dynamic object should have had a property.
/// </summary>
sealed class DynamicPropertyFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    internal override SlotReading? FromSlots(Slots s) =>
        new(new(this, sentence => Word(sentence, s.Expected)), s.Actual);

    public FailureMessage With(string propertyName) =>
        new(this, sentence => Word(sentence, propertyName));

    internal static string Word(Sentence sentence, object? expectedValue)
    {
        Debug.Assert(expectedValue is object);

        var propertyName = expectedValue;
        var codePart = sentence.Subject;

        if (string.IsNullOrEmpty(codePart))
        {
            const string genericFormat = """Dynamic object should contain property "{0}" but does not.""";
            return string.Format(genericFormat, propertyName?.ToString()?.Trim());
        }

        const string format = """Dynamic object "{0}" should contain property "{1}" but does not.""";
        return string.Format(format, codePart!.Trim(), propertyName?.ToString()?.Trim());
    }
}
