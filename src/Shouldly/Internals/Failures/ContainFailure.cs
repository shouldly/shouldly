namespace Shouldly.Internals.Failures;

/// <summary>
/// A string or collection should have contained something, or should not have.
/// </summary>
sealed class ContainFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    /// <summary>
    /// A value that should, or should not, have been contained.
    /// </summary>
    public FailureMessage With(object? expected, Case? caseSensitivity = null) =>
        new(this, sentence => Word(sentence, expected, caseSensitivity));

    /// <summary>
    /// A value that should have been contained to within a tolerance.
    /// </summary>
    public FailureMessage Within(object? expected, object tolerance) =>
        new(this, sentence => ContainWithinWording.Word(sentence, expected, tolerance));

    /// <summary>
    /// A condition that some element, or <paramref name="matchCount"/> elements, should have satisfied.
    /// </summary>
    public FailureMessage WithCondition(Expression condition, int? matchCount = null) =>
        new(this, sentence => ContainConditionWording.Word(sentence, condition, condition: null, matchCount));

    /// <summary>
    /// A condition no element should have satisfied, and the elements that did.
    /// </summary>
    public FailureMessage WithConditionMatched(Expression condition, object? matchingElements) =>
        new(this, sentence => ContainConditionWording.Word(sentence, matchingElements, condition, matchCount: null));

    internal static string Word(Sentence sentence, object? expectedValue, Case? caseOption)
    {
        var codePart = sentence.Subject;
        var caseSensitivity = caseOption == Case.Insensitive ? " (case insensitive comparison)" : string.Empty;
        var actual = sentence.ActualText;
        var but = codePart == actual ? sentence.Negated ? "did" : "did not" : $"""
             was actually
             {actual}
             """;

        return
            $"""
             {codePart}
                 {sentence.Phrase}{caseSensitivity}
             {expectedValue.ToStringAwesomely()}
                 but {but}
             """;
    }
}
