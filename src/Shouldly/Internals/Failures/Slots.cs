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

    /// <summary>
    /// These slots with the expected and actual values replaced, such as by their captures.
    /// </summary>
    public Slots WithValues(object? expected, object? actual) =>
        new(Shape, expected, actual, HasActual, Tolerance, CaseSensitivity, ExceptionMessage, Path);

    public MessageShape Shape { get; }
    public object? Expected { get; }
    public object? Actual { get; }

    /// <summary>Whether the class constructed takes an actual value at all.</summary>
    public bool HasActual { get; }

    public object? Tolerance { get; }
    public Case? CaseSensitivity { get; }
    public string? ExceptionMessage { get; }
    public IEnumerable<string>? Path { get; }

    /// <summary>
    /// Whether the class constructed was given a tolerance or a case option, which its message must show.
    /// </summary>
    public bool HasOption =>
        (Shape == MessageShape.Tolerance && Tolerance != null) ||
        (Shape == MessageShape.CaseSensitivity && CaseSensitivity != null);
}

/// <summary>
/// A failure read from <see cref="Slots"/>: its message, and the slot that held the value under test.
/// </summary>
readonly struct SlotReading(FailureMessage message, object? actual)
{
    public FailureMessage Message { get; } = message;
    public object? Actual { get; } = actual;
}
