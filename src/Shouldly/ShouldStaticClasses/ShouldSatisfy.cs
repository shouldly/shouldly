using JetBrains.Annotations;

namespace Shouldly;

public static partial class Should
{
    /// <summary>
    /// Asserts that all the supplied conditions are satisfied, reporting every failure at once.
    /// </summary>
    /// <remarks>
    /// Unlike <see cref="ShouldSatisfyAllConditionsTestExtensions.ShouldSatisfy{T}"/>, there is no
    /// single value under test here — the conditions are arbitrary assertions — so this is a static
    /// method rather than an extension method. Each failing condition carries its own message.
    /// </remarks>
    public static void Satisfy([InstantHandle] Action[] conditions, string? customMessage = null,
        [CallerArgumentExpression(nameof(conditions))] string? actualExpression = null)
    {
        var errorMessageString = ShouldSatisfyAllConditionsTestExtensions.CollectConditionFailures(conditions);
        if (errorMessageString != null)
        {
            // There is no single value under test, so the captured conditions expression is threaded
            // through only to satisfy the CallerArgumentExpression convention/trip-wire; the message
            // itself has no subject and the generator does not render it (see ShouldSatisfyMessageGenerator).
            throw new ShouldAssertException(new ExpectedShouldlyMessage(errorMessageString, customMessage, actualExpression: actualExpression).ToString());
        }
    }
}
