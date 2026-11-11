namespace Shouldly.Internals.Failures;

/// <summary>
/// What every wording is given about the failing assertion: its subject, its phrase, whether it is negated, and
/// the actual value.
/// </summary>
readonly struct Sentence
{
    public Sentence(string phrase, bool negated, string? subject, bool subjectFromSource, object? actual)
    {
        Phrase = phrase;
        Negated = negated;
        Subject = subject;
        SubjectFromSource = subjectFromSource;
        Actual = actual;
    }

    /// <summary>The source text of the actual value's expression, or what stands in for it.</summary>
    public string? Subject { get; }

    /// <summary>
    /// Whether the subject is source text. When no source text was captured, the formatted actual value stands in
    /// for it, and a wording has nothing to gain by repeating that value.
    /// </summary>
    public bool SubjectFromSource { get; }

    /// <summary>The value under test.</summary>
    public object? Actual { get; }

    /// <summary>What the assertion required of the subject, such as <c>should be null</c>.</summary>
    public string Phrase { get; }

    /// <summary>Whether the assertion requires something not to hold, so it fails when it does.</summary>
    public bool Negated { get; }

    /// <summary>The actual value as a failure message shows it.</summary>
    public string? ActualText => Actual.ToStringAwesomely();

    /// <summary>
    /// Whether the subject already reads as <paramref name="valueText"/>, which happens when there was no source
    /// text and the value stood in for it. Repeating the value would then add nothing.
    /// </summary>
    public bool SubjectShows(string? valueText) => Subject == valueText;
}
