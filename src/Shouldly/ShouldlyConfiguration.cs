using Shouldly.Internals;

namespace Shouldly;

/// <summary>
/// Configuration settings for Shouldly's behavior
/// </summary>
public static partial class ShouldlyConfiguration
{
    /// <summary>
    /// Registers how Shouldly decides whether two values of type <typeparamref name="T"/> are equal, for a type whose
    /// own <see cref="object.Equals(object)"/> or enumeration doesn't capture its meaning. For example, to compare
    /// Newtonsoft.Json values as JSON:
    /// <code>ShouldlyConfiguration.UseEquality&lt;JToken&gt;(JToken.DeepEquals);</code>
    /// </summary>
    /// <remarks>
    /// The rule applies wherever Shouldly compares two values that are both instances of <typeparamref name="T"/>,
    /// including subtypes and implementations, at any depth: <c>ShouldBe</c>, <c>ShouldNotBe</c>, collection elements,
    /// dictionary keys and values, and the membership assertions (<c>ShouldContain</c>, <c>ShouldBeOneOf</c>, ...).
    /// <c>ShouldBeEquivalentTo</c> uses it too: two values the rule covers are compared by the rule, as one value,
    /// and not member by member, so the rule decides how much of them is compared.
    /// A value it applies to is never walked as a collection, except by <c>ignoreOrder: true</c>, which matches its
    /// elements in any order, each by its rules. <paramref name="equal"/> is called with the expected value first and
    /// never with <c>null</c>. When several rules apply, the one for the most specific type wins; of equally specific
    /// ones, the one registered last. Register rules once, before any assertion runs, for example from a module
    /// initializer.
    /// </remarks>
    public static void UseEquality<T>(Func<T, T, bool> equal)
    {
        if (equal == null)
            throw new ArgumentNullException(nameof(equal));

        EqualityRules.Add(equal);
    }

    /// <summary>
    /// Registers <paramref name="comparer"/> as how Shouldly decides whether two values of type
    /// <typeparamref name="T"/> are equal. See <see cref="UseEquality{T}(Func{T, T, bool})"/>.
    /// </summary>
    public static void UseEquality<T>(IEqualityComparer<T> comparer)
    {
        if (comparer == null)
            throw new ArgumentNullException(nameof(comparer));

        EqualityRules.Add<T>(comparer.Equals);
    }

    /// <summary>
    /// Suppresses the legacy stack-trace + source-file parser that recovers the actual argument's
    /// expression text when <see cref="System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"/>
    /// did not supply one (e.g. callers compiled against Shouldly's netstandard2.0 build with an older
    /// C# compiler). When the toggle is active and no CAE-supplied expression is available, error
    /// messages format the value alone (for example: <c>False should be True but was not</c>).
    /// </summary>
    /// <remarks>
    /// CAE-supplied expressions are always honoured — this toggle does not suppress them. On net8.0+
    /// the toggle is effectively a no-op because Shouldly never walks the stack to recover source
    /// text on those frameworks.
    /// </remarks>
    public static IDisposable DisableSourceInErrors()
    {
        CallContext.LogicalSetData("ShouldlyDisableSourceInErrors", true);
        return new EnableSourceInErrorsDisposable();
    }

    /// <summary>
    /// Returns true when the stack-trace fallback for recovering an assertion's call-site
    /// expression is suppressed. See <see cref="DisableSourceInErrors"/>.
    /// </summary>
    public static bool IsSourceDisabledInErrors() =>
        (bool?)CallContext.LogicalGetData("ShouldlyDisableSourceInErrors") == true;

    private class EnableSourceInErrorsDisposable : IDisposable
    {
        public void Dispose() =>
            CallContext.LogicalSetData("ShouldlyDisableSourceInErrors", null);
    }

    /// <summary>
    /// Linear tolerance used when an assertion compares two <see cref="float"/> or <see cref="double"/> values
    /// and wasn't given a tolerance of its own. Honoured by <c>ShouldBe</c>/<c>ShouldNotBe</c> (including
    /// collections, spans and memory), the dictionary value assertions, and the leaf values compared by
    /// <c>ShouldBeEquivalentTo</c>. Values compared with their own <c>Equals</c>, such as the members of a
    /// record passed to <c>ShouldBe</c>, are compared exactly. <see cref="decimal"/> and integer values are
    /// never affected. Defaults to 0 (exact comparison).
    /// </summary>
    public static double DefaultFloatingPointTolerance = 0.0d;

    /// <summary>
    /// Default timeout period for asynchronous operations
    /// </summary>
    public static TimeSpan DefaultTaskTimeout = TimeSpan.FromSeconds(10);

    /// <summary>
    /// Controls the character set used for string difference markers.
    /// Unicode uses ▼/▲ markers, Ascii uses v/^ markers.
    /// </summary>
    public static DiffStyle DiffStyle { get; set; } = DiffStyle.Unicode;

    /// <summary>
    /// Controls how control characters are displayed in string difference output.
    /// CStyle uses escape sequences (\r, \n), ControlPictures uses Unicode symbols (␍, ␊),
    /// Descriptive uses ASCII-safe names (&lt;CR&gt;, &lt;LF&gt;).
    /// </summary>
    /// <remarks>
    /// Scoped to the logical call context. Flows down through async/await and
    /// Task.Run by default; concurrent contexts get their own value.
    /// </remarks>
    public static EscapeStyle EscapeStyle
    {
        get => (EscapeStyle?)CallContext.LogicalGetData(EscapeStyleKey) ?? EscapeStyle.CStyle;
        set => CallContext.LogicalSetData(EscapeStyleKey, value);
    }

    private const string EscapeStyleKey = "ShouldlyEscapeStyle";

    /// <summary>
    /// Maximum number of characters of the actual and expected values echoed back in string
    /// assertion failure messages. Longer values are truncated, and the message says so along
    /// with the value's full length.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This only bounds the verbatim echo. The <c>difference</c> section is always computed from
    /// the full, untruncated values, so lowering this can never hide a difference — it just trims
    /// the surrounding noise. Raise it when you want more of the raw value in the message.
    /// </para>
    /// <para>
    /// Scoped to the logical call context. Flows down through async/await and
    /// Task.Run by default; concurrent contexts get their own value.
    /// </para>
    /// </remarks>
    public static int MaxStringLengthInMessages
    {
        get => (int?)CallContext.LogicalGetData(MaxStringLengthInMessagesKey) ?? DefaultMaxStringLengthInMessages;
        set
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException(nameof(value), value,
                    $"{nameof(MaxStringLengthInMessages)} must be at least 1.");

            CallContext.LogicalSetData(MaxStringLengthInMessagesKey, value);
        }
    }

    private const int DefaultMaxStringLengthInMessages = 1000;

    private const string MaxStringLengthInMessagesKey = "ShouldlyMaxStringLengthInMessages";

    /// <summary>
    /// Maximum number of items of an enumerable echoed back in assertion failure messages.
    /// Longer enumerables are cut off after this many items and end with <c>...</c>.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This also bounds how far Shouldly reads a sequence to describe it, so a failing assertion
    /// on an infinite or very long lazy sequence returns promptly instead of hanging. It applies to
    /// the echoed values and to the <c>difference</c> section alike, and to nested enumerables.
    /// </para>
    /// <para>
    /// Scoped to the logical call context. Flows down through async/await and
    /// Task.Run by default; concurrent contexts get their own value.
    /// </para>
    /// </remarks>
    public static int MaxEnumerableItemsInMessages
    {
        get => (int?)CallContext.LogicalGetData(MaxEnumerableItemsInMessagesKey) ?? DefaultMaxEnumerableItemsInMessages;
        set
        {
            if (value < 1)
                throw new ArgumentOutOfRangeException(nameof(value), value,
                    $"{nameof(MaxEnumerableItemsInMessages)} must be at least 1.");

            CallContext.LogicalSetData(MaxEnumerableItemsInMessagesKey, value);
        }
    }

    private const int DefaultMaxEnumerableItemsInMessages = 1000;

    private const string MaxEnumerableItemsInMessagesKey = "ShouldlyMaxEnumerableItemsInMessages";
}
