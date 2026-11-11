namespace Shouldly.Internals.Failures;

/// <summary>
/// One kind of failure Shouldly reports. Each built-in assertion has an entry in <see cref="Fail"/>, named exactly
/// as the assertion, which says how its failure message is worded.
/// </summary>
abstract class Failure
{
    protected Failure(string name, bool negated)
    {
        Name = name;
        Negated = negated;
        Phrase = name.PascalToSpaced();
        FailureNames.Register(this);
    }

    /// <summary>The name of the assertion this failure belongs to, such as <c>ShouldBeNull</c>.</summary>
    public string Name { get; }

    /// <summary>What the assertion required of the subject, such as <c>should be null</c>.</summary>
    public string Phrase { get; }

    /// <summary>Whether the assertion requires something not to hold, so it fails when it does.</summary>
    public bool Negated { get; }

    /// <summary>
    /// This failure as a custom assertion states it through one of the public message classes, or null when
    /// those classes can't carry what its wording needs.
    /// </summary>
    internal virtual SlotReading? FromSlots(Slots slots) => null;
}

/// <summary>
/// A failure whose message needs nothing but the actual value.
/// </summary>
abstract class FailureWithoutFacts(string name, bool negated) : Failure(name, negated)
{
    /// <summary>The exception to throw for a failing assertion.</summary>
    public ShouldAssertException For(object? actual, string? customMessage, string? actualExpression) =>
        new FailureMessage(this, Word).For(actual, customMessage, actualExpression);

    /// <summary>The failure message, without throwing.</summary>
    public string Text(object? actual, string? customMessage, string? actualExpression) =>
        new FailureMessage(this, Word).Text(actual, customMessage, actualExpression);

    protected abstract string Word(Sentence sentence);
}

/// <summary>
/// The built-in failures by assertion name, so that a custom assertion named exactly like a built-in one gets its
/// wording.
/// </summary>
static class FailureNames
{
    private static readonly Dictionary<string, Failure> ByName = new(StringComparer.Ordinal);

    public static void Register(Failure failure)
    {
        lock (ByName)
            ByName.Add(failure.Name, failure);
    }

    public static Failure? Find(string name)
    {
        // Every catalogue entry registers itself as it is created, so the catalogue must have been.
        RuntimeHelpers.RunClassConstructor(typeof(Fail).TypeHandle);

        lock (ByName)
            return ByName.TryGetValue(name, out var failure) ? failure : null;
    }
}
