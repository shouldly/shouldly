namespace Shouldly.Internals.Failures;

/// <summary>
/// The failures Shouldly's own assertions report, one per assertion and named exactly as the assertion. An
/// assertion that fails throws <c>Fail.Name.With(what the check established).For(actual, customMessage,
/// actualExpression)</c>.
/// </summary>
static class Fail
{
    public static readonly NullFailure ShouldBeNull = new();
    public static readonly NullFailure ShouldNotBeNull = new(negated: true);
}
