using System.ComponentModel;
using JetBrains.Annotations;

namespace Shouldly;

/// <summary>
/// Provides extension methods for validating that objects satisfy multiple conditions
/// </summary>
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static partial class ShouldSatisfyAllConditionsTestExtensions
{
    private const string ObsoleteMessage =
        "Use ShouldSatisfy instead. This overload cannot capture the asserted expression via " +
        "CallerArgumentExpression and falls back to stack-trace parsing, which is not trim- or AOT-safe.";

    private const string NotAotSafeMessage =
        "Without a CallerArgumentExpression the asserted expression is recovered by walking the stack " +
        "trace and reading source, which the trimmer cannot preserve. Use ShouldSatisfy instead.";

    /// <summary>
    /// Asserts that the actual value satisfies all specified conditions.
    /// </summary>
    public static void ShouldSatisfy<T>(this T actual, [InstantHandle] Action<T>[] conditions, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        ShouldSatisfy(actual, CreateParameterlessActions(actual, conditions), customMessage, actualExpression);
    }

    /// <summary>
    /// Asserts that the actual object satisfies all specified conditions.
    /// </summary>
    public static void ShouldSatisfy(this object? actual, [InstantHandle] Action[] conditions, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var errorMessages = new List<Exception>();
        foreach (var action in conditions)
        {
            try
            {
                action.Invoke();
            }
            catch (Exception exc)
            {
                errorMessages.Add(exc);
            }
        }

        if (errorMessages.Any())
        {
            var errorMessageString = BuildErrorMessageString(errorMessages);
            throw new ShouldAssertException(new ExpectedActualShouldlyMessage(errorMessageString, actual, customMessage, actualExpression: actualExpression).ToString());
        }
    }

    /// <summary>
    /// Asserts that the actual value satisfies all specified conditions.
    /// </summary>
    [Obsolete(ObsoleteMessage)]
    [RequiresUnreferencedCode(NotAotSafeMessage)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldSatisfyAllConditions<T>(this T actual, [InstantHandle] params Action<T>[] conditions)
    {
        SatisfyAllConditionsViaStackWalking(actual, null, CreateParameterlessActions(actual, conditions));
    }

    /// <summary>
    /// Asserts that the actual value satisfies all specified conditions with a custom message.
    /// </summary>
    [Obsolete(ObsoleteMessage)]
    [RequiresUnreferencedCode(NotAotSafeMessage)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldSatisfyAllConditions<T>(this T actual, string? customMessage, [InstantHandle] params Action<T>[] conditions)
    {
        SatisfyAllConditionsViaStackWalking(actual, customMessage, CreateParameterlessActions(actual, conditions));
    }

    /// <summary>
    /// Asserts that the actual object satisfies all specified conditions.
    /// </summary>
    [Obsolete(ObsoleteMessage)]
    [RequiresUnreferencedCode(NotAotSafeMessage)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldSatisfyAllConditions(this object? actual, [InstantHandle] params Action[] conditions)
    {
        SatisfyAllConditionsViaStackWalking(actual, null, conditions);
    }

    /// <summary>
    /// Asserts that the actual object satisfies all specified conditions with a custom message.
    /// </summary>
    [Obsolete(ObsoleteMessage)]
    [RequiresUnreferencedCode(NotAotSafeMessage)]
    [MethodImpl(MethodImplOptions.NoInlining)]
    public static void ShouldSatisfyAllConditions(this object? actual, string? customMessage, [InstantHandle] params Action[] conditions)
    {
        SatisfyAllConditionsViaStackWalking(actual, customMessage, conditions);
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private static void SatisfyAllConditionsViaStackWalking(object? actual, string? customMessage, Action[] conditions)
    {
        // The obsolete params overloads have no CallerArgumentExpression, so the asserted
        // expression is recovered by stack-walking. Opt out of the trip-wire that otherwise
        // forbids that fallback (see ShouldlyConfiguration.AllowStackWalking).
        using (ShouldlyConfiguration.AllowStackWalking())
        {
            ShouldSatisfy(actual, conditions, customMessage, actualExpression: null);
        }
    }

    private static Action[] CreateParameterlessActions<T>(T parameter, params Action<T>[] actions) {
        return actions.Select(a => new Action(() => a(parameter))).ToArray();
    }

    private static string BuildErrorMessageString(IEnumerable<Exception> errorMessages) =>
        AssertionErrorFormatter.FormatErrors(errorMessages.Select(e => e.Message));
}
