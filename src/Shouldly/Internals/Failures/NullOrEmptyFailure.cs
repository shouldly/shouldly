namespace Shouldly.Internals.Failures;

/// <summary>
/// A string or collection should have been null or empty, or should not have been.
/// </summary>
sealed class NullOrEmptyFailure(bool negated = false, [CallerMemberName] string name = "") : FailureWithoutFacts(name, negated)
{

    protected override string Word(Sentence sentence)
    {
        var codePart = sentence.Subject;
        var should = sentence.Phrase;
        var actual = sentence.ActualText;

        // A non-empty collection fails ShouldBeNullOrEmpty; report how many items it had, as ShouldBeEmpty does.
        if (!sentence.Negated && sentence.Actual is IEnumerable enumerable and not string)
        {
            var (count, noun) = enumerable.DescribeCountForMessage();
            return
                $"""
                 {codePart}
                     {should} but had
                 {count}
                     {noun} and was
                 {actual}
                 """;
        }

        var format = sentence.SubjectShows(sentence.ActualText) ?
            """
            {0}
                {1}
            """
            :
            """
            {0} ({2})
                {1}
            """;

        return string.Format(format, codePart, should, actual);
    }
}
