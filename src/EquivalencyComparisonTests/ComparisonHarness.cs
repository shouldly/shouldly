using System.Text;
using FluentAssertions;
using Shouldly;
using Xunit.Sdk;

namespace EquivalencyComparisonTests;

public enum Outcome
{
    /// <summary>The assertion passed.</summary>
    Pass,

    /// <summary>The assertion failed with an assertion exception.</summary>
    Fail,

    /// <summary>The library threw a non-assertion exception (e.g. NotSupportedException).</summary>
    Error,
}

public record ComparisonResult(
    Outcome Shouldly,
    string? ShouldlyMessage,
    Outcome FluentAssertions,
    string? FluentAssertionsMessage)
{
    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"Shouldly:         {Shouldly}");
        if (ShouldlyMessage != null)
            sb.AppendLine(Indent(ShouldlyMessage));
        sb.AppendLine($"FluentAssertions: {FluentAssertions}");
        if (FluentAssertionsMessage != null)
            sb.AppendLine(Indent(FluentAssertionsMessage));
        return sb.ToString();

        static string Indent(string message) =>
            "    " + message.Replace("\n", "\n    ");
    }
}

/// <summary>
/// Runs the same equivalency assertion through Shouldly's ShouldBeEquivalentTo and
/// FluentAssertions' BeEquivalentTo (v7, default options) and reports both outcomes,
/// so each test documents where the two libraries agree or diverge.
/// </summary>
public static class Compare
{
    public static ComparisonResult Run<T>(T? actual, T? expected)
    {
        var (shouldlyOutcome, shouldlyMessage) = RunShouldly(actual, expected);
        var (fluentOutcome, fluentMessage) = RunFluentAssertions(actual, expected);
        return new(shouldlyOutcome, shouldlyMessage, fluentOutcome, fluentMessage);
    }

    private static (Outcome, string?) RunShouldly<T>(T? actual, T? expected)
    {
        try
        {
            actual.ShouldBeEquivalentTo(expected);
            return (Outcome.Pass, null);
        }
        catch (ShouldAssertException ex)
        {
            return (Outcome.Fail, ex.Message);
        }
        catch (Exception ex)
        {
            return (Outcome.Error, $"{ex.GetType().Name}: {ex.Message}");
        }
    }

    private static (Outcome, string?) RunFluentAssertions<T>(T? actual, T? expected)
    {
        try
        {
            actual.Should().BeEquivalentTo(expected);
            return (Outcome.Pass, null);
        }
        catch (Exception ex) when (ex is XunitException || ex.GetType().FullName!.StartsWith("FluentAssertions.Execution", StringComparison.Ordinal))
        {
            return (Outcome.Fail, ex.Message);
        }
        catch (Exception ex)
        {
            return (Outcome.Error, $"{ex.GetType().Name}: {ex.Message}");
        }
    }
}

public static class ComparisonAssertions
{
    /// <summary>Asserts that both libraries produce the same outcome for this scenario.</summary>
    public static void ShouldAgree(this ComparisonResult result, Outcome expected)
    {
        if (result.Shouldly != expected || result.FluentAssertions != expected)
            throw new XunitException(
                $"Expected both libraries to {expected}, but got:\n{result}");
    }

    /// <summary>
    /// Asserts that the libraries diverge in the documented way. The <paramref name="because"/>
    /// text explains the behavioral difference this test pins down.
    /// </summary>
    public static void ShouldDiverge(this ComparisonResult result, Outcome shouldly, Outcome fluentAssertions, string because)
    {
        if (shouldly == fluentAssertions)
            throw new ArgumentException("Divergence requires different outcomes; use ShouldAgree instead.");

        if (result.Shouldly != shouldly || result.FluentAssertions != fluentAssertions)
            throw new XunitException(
                $"Expected divergence (Shouldly: {shouldly}, FluentAssertions: {fluentAssertions}) because {because}, but got:\n{result}");
    }
}
