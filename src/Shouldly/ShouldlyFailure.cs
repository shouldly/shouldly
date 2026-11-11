using Shouldly.Internals;
using Shouldly.Internals.Failures;

namespace Shouldly;

/// <summary>
/// The failure of a custom assertion, created by <see cref="ShouldAssertException.For"/> and thrown by the
/// assertion. Its message shows the subject, a phrase saying what the assertion required, and the actual value.
/// The methods here add to that or change it; each returns the same failure, so they chain.
/// </summary>
/// <remarks>
/// Shouldly resolves the subject, formats values, highlights differences between an expected and an actual value,
/// and appends the custom message. Values are formatted as they are passed in, so the message is unaffected if a
/// value is disposed or changed after the assertion throws. A sequence is read once, and no further than a message
/// shows.
/// </remarks>
public sealed class ShouldlyFailure : ShouldAssertException
{
    private readonly object? _actual;
    private readonly string? _customMessage;
    private readonly string? _subject;
    private readonly bool _subjectFromSource;
    private string _phrase;
    private PlainLines _lines;
    private List<KeyValuePair<string, string>>? _context;

    internal ShouldlyFailure(object? actual, string? customMessage, string? actualExpression, string shouldlyMethod)
        : base(null)
    {
        // The message reads a sequence more than once: to format it, to stand in for the subject and to highlight
        // differences. A sequence that may be single-pass is read once here and its snapshot used from then on.
        actual = CapturedSequence.ForMessage(actual);

        _actual = actual;
        _customMessage = customMessage;
        _phrase = shouldlyMethod.PascalToSpaced();
        _lines = PlainLines.For(actual);
        _subject = Subject.Resolve(shouldlyMethod, actual, actualExpression, null, out _subjectFromSource);
    }

    /// <summary>
    /// Adds the expected value, shown the way Shouldly formats values. Without it the message has no expected line.
    /// </summary>
    /// <param name="expected">The value the assertion compared the actual value against.</param>
    public ShouldlyFailure Expected(object? expected)
    {
        _lines = _lines.WithExpected(CapturedSequence.ForMessage(expected), _actual);
        return this;
    }

    /// <summary>
    /// Describes what was expected in words, shown as written. Use it when the expectation isn't a value, such as
    /// <c>at least 1 element</c>.
    /// </summary>
    /// <param name="description">What was expected. Null leaves the message as it was.</param>
    public ShouldlyFailure ExpectedInWords(string description)
    {
        if (description != null)
            _lines = _lines.WithExpectedInWords(description);
        return this;
    }

    /// <summary>
    /// Describes what was found in words, shown as written in place of the actual value. Use it when a sentence
    /// says more than the value would, such as <c>no value at '/items'</c>.
    /// </summary>
    /// <param name="description">What was found. Null leaves the message as it was.</param>
    public ShouldlyFailure ActualInWords(string description)
    {
        if (description != null)
            _lines = _lines.WithActualInWords(description);
        return this;
    }

    /// <summary>
    /// Leaves the actual value out of the message, for an assertion with nothing useful to show for it.
    /// </summary>
    public ShouldlyFailure WithoutActual()
    {
        _lines = _lines.WithoutActual();
        return this;
    }

    /// <summary>
    /// States the phrase that says what the assertion required, such as <c>should have error</c>. Without it the
    /// phrase comes from the name of the method that called <see cref="ShouldAssertException.For"/>:
    /// <c>ShouldBeAwesome</c> reads <c>should be awesome</c>.
    /// </summary>
    /// <param name="phrase">The phrase. Null or empty leaves the message as it was.</param>
    public ShouldlyFailure Phrase(string phrase)
    {
        if (!string.IsNullOrEmpty(phrase))
            _phrase = phrase;
        return this;
    }

    /// <summary>
    /// Appends a labelled block after the message, such as the response an HTTP assertion was made against. Call
    /// it again for further blocks; they appear in the order added.
    /// </summary>
    /// <param name="label">What the block is, shown above it.</param>
    /// <param name="text">The block's text. A null or empty block is left out.</param>
    public ShouldlyFailure Context(string label, string? text)
    {
        if (!string.IsNullOrEmpty(text))
            (_context ??= []).Add(new(label, text!));
        return this;
    }

    /// <summary>
    /// The failure message, as stated so far.
    /// </summary>
    public override string Message
    {
        get
        {
            // Only the text captured so far is assembled here. The values themselves are not read again.
            var sentence = new Sentence(_phrase, CustomAssertionMessage.IsNegated(_phrase), _subject, _subjectFromSource, actual: null);
            var message = FailureMessage.WithAdditionalInfo(PlainWording.Word(sentence, _lines), _customMessage);
            if (_context == null)
                return message;

            var builder = new StringBuilder(message);
            foreach (var block in _context)
            {
                builder.Append(LineBreak).Append(LineBreak).Append(block.Key).Append(':');
                foreach (var line in block.Value.Split('\n'))
                    builder.Append(LineBreak).Append("    ").Append(line.TrimEnd('\r'));
            }

            return builder.ToString();
        }
    }

    // One line break, as the raw string literals the rest of a message is built from have it.
    private const string LineBreak = """


        """;
}
