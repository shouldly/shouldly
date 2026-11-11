namespace Shouldly.DifferenceHighlighting;

/// <summary>
/// What walking an actual and an expected sequence in step found, as far as a failure message shows them.
/// </summary>
readonly struct SequenceDifference
{
    public SequenceDifference(string text, int? firstIndex, bool firstIsPair, object? firstActual, object? firstExpected, bool cutShort)
    {
        Text = text;
        FirstIndex = firstIndex;
        FirstIsPair = firstIsPair;
        FirstActual = firstActual;
        FirstExpected = firstExpected;
        CutShort = cutShort;
    }

    /// <summary>The actual sequence's items, with those that differ from the expected sequence's marked.</summary>
    public string Text { get; }

    /// <summary>The index of the first position that differs, or null when none of those shown does.</summary>
    public int? FirstIndex { get; }

    /// <summary>
    /// Whether both sequences have an item at <see cref="FirstIndex"/>. When one of them ended there, they don't.
    /// </summary>
    public bool FirstIsPair { get; }

    /// <summary>The actual sequence's item at <see cref="FirstIndex"/>, when <see cref="FirstIsPair"/>.</summary>
    public object? FirstActual { get; }

    /// <summary>The expected sequence's item at <see cref="FirstIndex"/>, when <see cref="FirstIsPair"/>.</summary>
    public object? FirstExpected { get; }

    /// <summary>Whether the sequences go on past what a failure message shows, so the walk stopped there.</summary>
    public bool CutShort { get; }
}
