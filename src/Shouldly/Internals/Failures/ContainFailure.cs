namespace Shouldly.Internals.Failures;

/// <summary>
/// A string or collection should have contained something, or should not have.
/// </summary>
sealed class ContainFailure(bool negated = false, [CallerMemberName] string name = "") : Failure(name, negated)
{
    // A tolerance has its own wording. The wording mentions case only when it was ignored.
    protected override bool ShowsOption(Slots s) =>
        s.Shape == MessageShape.Tolerance || s.CaseSensitivity == Case.Insensitive;

    protected override SlotReading? FromSlots(Slots s) => s.Shape switch
    {
        MessageShape.Throw or MessageShape.Equivalence => null,
        MessageShape.Tolerance when s.Tolerance is { } tolerance => new(Within(s.Expected, tolerance), s.Actual),
        _ when s.Expected is Expression condition => new(WithCondition(condition), s.Actual),
        _ => new(With(s.Expected, s.CaseSensitivity), s.Actual)
    };

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
        var subject = sentence.Subject;
        var caseSensitivity = caseOption == Case.Insensitive ? " (case insensitive comparison)" : string.Empty;
        var actual = sentence.ActualText;
        var but = subject == actual ? sentence.Negated ? "did" : "did not" : $"""
             was actually
             {actual}
             """;

        return
            $"""
             {subject}
                 {sentence.Phrase}{caseSensitivity}
             {expectedValue.ToStringAwesomely()}
                 but {but}
             """;
    }
}
