using System.Collections.Concurrent;

namespace Shouldly.Internals;

/// <summary>
/// The equality rules registered with <see cref="ShouldlyConfiguration.UseEquality{T}(Func{T, T, bool})"/>:
/// user-supplied definitions of equality for a type and its subtypes, which <see cref="Equality"/> consults before
/// its own rules, at any depth.
/// </summary>
/// <remarks>
/// Registration is copy-on-write: each one publishes a new, immutable state, so comparisons running in parallel
/// read a consistent set of rules without locking.
/// </remarks>
static class EqualityRules
{
    private static readonly object RegistrationLock = new();
    private static volatile State s_state = new([]);

    /// <summary>
    /// Whether no rule is registered: the common case, checked first so that comparisons pay nothing for rules.
    /// </summary>
    public static bool IsEmpty => s_state.Rules.Length == 0;

    public static void Add<T>(Func<T, T, bool> equal)
    {
        var rule = new Rule(typeof(T), (expected, actual) => equal((T)expected, (T)actual));
        lock (RegistrationLock)
        {
            s_state = new([.. s_state.Rules, rule]);
        }
    }

    /// <summary>
    /// For tests: restores the rules registered now when the result is disposed, so a test can register a rule
    /// (for a type as common as <see cref="string"/>) without it outliving the test. Only safe in a test that runs
    /// alone, as the rule applies to every comparison while registered.
    /// </summary>
    internal static IDisposable RestoreAfterwards() => new Restorer(s_state);

    private sealed class Restorer(State saved) : IDisposable
    {
        public void Dispose()
        {
            lock (RegistrationLock)
            {
                s_state = saved;
            }
        }
    }

    /// <summary>
    /// Whether a rule could decide the equality of a value of <paramref name="type"/>, for the shortcuts that compare
    /// without consulting rules (hashing, the BCL's typed comparer). A <see cref="Nullable{T}"/> is judged by its
    /// underlying type, as its values box to that type when a rule is consulted.
    /// </summary>
    public static bool CouldApplyTo(Type type)
    {
        type = Nullable.GetUnderlyingType(type) ?? type;
        foreach (var rule in s_state.Rules)
        {
            if (rule.Type.IsAssignableFrom(type))
                return true;
        }

        return false;
    }

    /// <summary>
    /// When a rule applies to both values, sets <paramref name="result"/> to its verdict and returns <c>true</c>.
    /// A rule applies when both values are instances of its type; when several do, the most specific wins, and of
    /// equally specific (or unrelated) ones, the one registered last. A value wrapped by
    /// captured sequence is judged as the sequence it captured.
    /// </summary>
    public static bool TryEquals(object expected, object actual, out bool result)
    {
        var state = s_state;
        if (state.Rules.Length == 0)
        {
            result = false;
            return false;
        }

        expected = CapturedSequence.Original(expected);
        actual = CapturedSequence.Original(actual);
        if (state.RuleFor(expected.GetType(), actual.GetType()) is not { } rule)
        {
            result = false;
            return false;
        }

        result = rule.Equal(expected, actual);
        return true;
    }

    /// <summary>
    /// The type the rule that applies to both values was registered for, or null when none applies. It is the rule
    /// <see cref="TryEquals"/> asks.
    /// </summary>
    public static Type? RuleTypeFor(object expected, object actual)
    {
        var state = s_state;
        if (state.Rules.Length == 0)
            return null;

        expected = CapturedSequence.Original(expected);
        actual = CapturedSequence.Original(actual);
        return state.RuleFor(expected.GetType(), actual.GetType())?.Type;
    }

    private sealed record Rule(Type Type, Func<object, object, bool> Equal);

    private sealed class State(Rule[] rules)
    {
        private readonly ConcurrentDictionary<(Type Expected, Type Actual), Rule?> _ruleByTypes = new();

        public Rule[] Rules { get; } = rules;

        public Rule? RuleFor(Type expectedType, Type actualType) =>
            _ruleByTypes.GetOrAdd((expectedType, actualType), FindRule);

        private Rule? FindRule((Type Expected, Type Actual) types)
        {
            var applicable = Array.FindAll(Rules, rule =>
                rule.Type.IsAssignableFrom(types.Expected) && rule.Type.IsAssignableFrom(types.Actual));

            // The last registered of the rules that no other applicable rule is more specific than.
            return Array.FindLast(applicable, rule =>
                !Array.Exists(applicable, other => other.Type != rule.Type && rule.Type.IsAssignableFrom(other.Type)));
        }
    }
}
