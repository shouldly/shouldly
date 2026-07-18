using Shouldly.Equivalency;

namespace Shouldly.MessageGenerators;

class ShouldBeEquivalentToMessageGenerator : ShouldlyMessageGenerator
{
    private const int IndentSize = 4;

    public override bool CanProcess(IShouldlyAssertionContext context) =>
        context.ShouldMethod == "ShouldBeEquivalentTo";

    public override string GenerateErrorMessage(IShouldlyAssertionContext context)
    {
        if (context is ShouldlyAssertionContext { EquivalencyDifferences: { Count: > 0 } differences })
            return GenerateFromDifferences(context, differences);

        return $"""
                Comparing object equivalence, at path:
                {FormatPath(context.CodePart, context.Path)}

                {GenerateValueMismatchBody(context.Expected, context.Actual)}
                """;
    }

    private static string GenerateFromDifferences(IShouldlyAssertionContext context, IReadOnlyList<EquivalencyDifference> differences)
    {
        if (differences.Count == 1)
        {
            return $"""
                    Comparing object equivalence, at path:
                    {FormatPath(context.CodePart, differences[0].PathSegments)}

                    {GenerateBody(differences[0])}
                    """;
        }

        var sb = new StringBuilder();
        sb.AppendLine($"Comparing object equivalence, found {differences.Count} differences:");
        foreach (var difference in differences)
        {
            sb.AppendLine();
            sb.AppendLine("at path:");
            sb.AppendLine(FormatPath(context.CodePart, difference.PathSegments));
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
                 which would make the assertion vacuously pass; cast the expectation to a concrete type or assert on specific members instead
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

    private static string FormatPath(string? codePart, IEnumerable<string>? path)
    {
        var result = new StringBuilder(codePart);
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
