using Shouldly.Internals;
using Shouldly.Internals.XunitV3Markers;

namespace Shouldly;

/// <summary>
/// Exception thrown when a Shouldly assertion fails
/// </summary>
[Serializable]
public class ShouldAssertException : Exception, IAssertionException
{
    /// <summary>
    /// Creates a new ShouldAssertException with the specified message
    /// </summary>
    public ShouldAssertException(string? message) : base(message)
    {
    }

    /// <summary>
    /// Creates a new ShouldAssertException with the specified message and inner exception
    /// </summary>
    public ShouldAssertException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    /// <summary>
    /// The failure of a custom assertion, for the assertion to throw. Pass the assertion's own value, custom
    /// message and captured expression, in that order:
    /// <code>
    /// public static void ShouldBeAwesome(this Person? actual, string? customMessage = null,
    ///     [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    /// {
    ///     if (actual is not { IsAwesome: true })
    ///         throw ShouldAssertException.For(actual, customMessage, actualExpression)
    ///             .Expected("an awesome person");
    /// }
    /// </code>
    /// The message shows the subject, a phrase taken from the calling method's name, and the actual value. The
    /// methods of <see cref="ShouldlyFailure"/> add an expected value, state the phrase, describe either value in
    /// words, leave the actual value out, or append context.
    /// </summary>
    /// <param name="actual">The value under test.</param>
    /// <param name="customMessage">The caller's own message, shown as additional info.</param>
    /// <param name="actualExpression">The source text of the value under test, captured by the assertion with <see cref="System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"/>.</param>
    /// <param name="shouldlyMethod">The name of the assertion, which the phrase is taken from. The compiler supplies it.</param>
    public static ShouldlyFailure For(object? actual, string? customMessage, string? actualExpression, [CallerMemberName] string shouldlyMethod = "") =>
        new(actual, customMessage, actualExpression, shouldlyMethod);

    private string? stackTrace;

    /// <inheritdoc/>
    public override string StackTrace => StackTraceHelpers.GetStackTrace(this, ref stackTrace);
}