using Shouldly.Equivalency;

namespace Shouldly.Internals.Failures;

/// <summary>
/// An object graph should have been equivalent to another.
/// </summary>
sealed class EquivalentFailure([CallerMemberName] string name = "") : Failure(name, negated: false)
{
    protected override SlotReading? FromSlots(Slots s) =>
        s.Shape == MessageShape.Throw ? null : new(new(this, sentence => Word(sentence, s.Expected, null, s.Path)), s.Actual);

    private const int IndentSize = 4;

    /// <summary>
    /// The differences the comparison found.
    /// </summary>
    public FailureMessage With(IReadOnlyList<EquivalencyDifference> differences) =>
        new(this, sentence => Word(sentence, null, differences, null));

    internal static string Word(Sentence sentence, object? expectedValue, IReadOnlyList<EquivalencyDifference>? found, IEnumerable<string>? path)
    {
        if (found is { Count: > 0 } differences)
            return GenerateFromDifferences(sentence, differences);

        return $"""
                Comparing object equivalence, at path:
                {FormatPath(sentence.Subject, path)}

                {GenerateValueMismatchBody(expectedValue, sentence.Actual)}
                """;
    }

    private static string GenerateFromDifferences(Sentence sentence, IReadOnlyList<EquivalencyDifference> differences)
    {
        if (differences.Count == 1)
        {
            return $"""
                    Comparing object equivalence, at path:
                    {FormatPath(sentence.Subject, differences[0].PathSegments)}

                    {GenerateBody(differences[0])}
                    """;
        }

        var sb = new StringBuilder();
        sb.AppendLine($"Comparing object equivalence, found {differences.Count} differences:");
        foreach (var difference in differences)
        {
            sb.AppendLine();
            sb.AppendLine("at path:");
            sb.AppendLine(FormatPath(sentence.Subject, difference.PathSegments));
            sb.AppendLine();
            sb.AppendLine(GenerateBody(difference));
        }

        return sb.ToString().TrimEnd();
    }

    private static string GenerateBody(EquivalencyDifference difference) => difference.Kind switch
    {
        EquivalencyDifferenceKind.MissingMember =>
            $"""
                 Expected a public member named
             {difference.Expected.ToStringAwesomely()}
                 but was not found on
             {difference.Detail}
             """,

        EquivalencyDifferenceKind.MissingKey =>
            $"""
                 Expected key
             {difference.Expected.ToStringAwesomely()}
                 but was not found
             """,

        EquivalencyDifferenceKind.UnexpectedKeys =>
            $"""
                 Found key(s) not present on the expectation
             {difference.Actual.ToStringAwesomely()}
             """,

        EquivalencyDifferenceKind.MissingElement =>
            $"""
                 Expected an element equivalent to
             {difference.Expected.ToStringAwesomely()}
                 but was not found
             """,

        EquivalencyDifferenceKind.VacuousComparison =>
            $"""
                 Found no public members to compare on
             {difference.Detail}
                 which would make the assertion vacuously pass; assert on specific members instead
             """,

        // The detail of a value mismatch is what decided it, when that isn't plain from the two values.
        _ when difference.Detail is { } decidedBy =>
            $"""
             {GenerateValueMismatchBody(difference.Expected, difference.Actual)}
                 and {decidedBy}
             """,

        _ => GenerateValueMismatchBody(difference.Expected, difference.Actual),
    };

    private static string GenerateValueMismatchBody(object? expected, object? actual) =>
        $"""
             Expected value to be
         {expected.ToStringAwesomely()}
             but was
         {actual.ToStringAwesomely()}
         """;

    private static string FormatPath(string? subject, IEnumerable<string>? path)
    {
        var result = new StringBuilder(subject);
        if (path != null)
        {
            var i = 0;
            foreach (var part in path)
            {
                result.Append(' ', i++ * IndentSize);
                result.AppendLine(part);
            }
        }

        return result.ToString().TrimEnd();
    }
}
