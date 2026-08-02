namespace Shouldly;

/// <summary>
/// Configuration settings for Shouldly's behavior
/// </summary>
public static partial class ShouldlyConfiguration
{
    static ShouldlyConfiguration()
    {
        CompareAsObjectTypes =
        [
            "Newtonsoft.Json.Linq.JToken",
            "Shouldly.Tests.TestHelpers.Strange"
        ];
    }

    /// <summary>
    /// List of type names that should be compared as objects rather than using their Equals method
    /// </summary>
    public static List<string> CompareAsObjectTypes { get; }

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
    /// Default tolerance used for floating point comparisons
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
}
