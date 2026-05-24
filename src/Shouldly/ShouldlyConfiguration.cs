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
    /// When set to true shouldly will not try and create better error messages using your source code
    /// </summary>
    public static IDisposable DisableSourceInErrors()
    {
        CallContext.LogicalSetData("ShouldlyDisableSourceInErrors", true);
        return new EnableSourceInErrorsDisposable();
    }

    /// <summary>
    /// Determines if source code should be excluded from error messages
    /// </summary>
    public static bool IsSourceDisabledInErrors()
    {
        if ((bool?)CallContext.LogicalGetData("ShouldlyDisableSourceInErrors") == true)
        {
            return true;
        }
#if NET8_0_OR_GREATER
        // For Native AOT let's disable source in errors until we have a better solution
        return !RuntimeFeature.IsDynamicCodeSupported;
#else
        return false;
#endif
    }

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
    /// Scoped to the logical call context, same pattern as <see cref="DisableSourceInErrors"/>.
    /// Flows down through async/await and Task.Run by default; concurrent
    /// contexts get their own value.
    /// </remarks>
    public static EscapeStyle EscapeStyle
    {
        get => (EscapeStyle?)CallContext.LogicalGetData(EscapeStyleKey) ?? EscapeStyle.CStyle;
        set => CallContext.LogicalSetData(EscapeStyleKey, value);
    }

    private const string EscapeStyleKey = "ShouldlyEscapeStyle";
}