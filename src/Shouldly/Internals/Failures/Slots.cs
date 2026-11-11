namespace Shouldly.Internals.Failures;

/// <summary>
/// Which of the public message classes a custom assertion constructed.
/// </summary>
enum MessageShape
{
    Expected,
    Actual,
    ExpectedActual,
    Tolerance,
    CaseSensitivity,
    IgnoringOrder,
    Throw,
    Equivalence
}

/// <summary>
/// What a custom assertion passed to one of the public message classes. The slots have no fixed meaning: which
/// of them holds the value under test depends on the assertion.
/// </summary>
readonly struct Slots
{
    public Slots(MessageShape shape, object? expected = null, object? actual = null, bool hasActual = false,
        object? tolerance = null, Case? caseSensitivity = null, string? exceptionMessage = null, IEnumerable<string>? path = null)
    {
        Shape = shape;
        Expected = expected;
        Actual = actual;
        HasActual = hasActual;
        Tolerance = tolerance;
        CaseSensitivity = caseSensitivity;
        ExceptionMessage = exceptionMessage;
        Path = path;
    }

    public MessageShape Shape { get; }
    public object? Expected { get; }
    public object? Actual { get; }

    /// <summary>Whether the class constructed takes an actual value at all.</summary>
    public bool HasActual { get; }

    public object? Tolerance { get; }
    public Case? CaseSensitivity { get; }
    public string? ExceptionMessage { get; }
    public IEnumerable<string>? Path { get; }
}

/// <summary>
/// A failure read from <see cref="Slots"/>: its message, and the slot that held the value under test.
/// </summary>
readonly struct SlotReading(FailureMessage message, object? actual)
{
    public FailureMessage Message { get; } = message;
    public object? Actual { get; } = actual;
}
