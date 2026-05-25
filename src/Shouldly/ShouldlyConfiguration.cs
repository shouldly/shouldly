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
    /// When active, Shouldly omits the source-level expression of the actual argument from error messages
    /// and formats the value alone (for example: <c>False should be True but was not</c>).
    /// </summary>
    /// <remarks>
    /// Suppresses both sources of expression text: the legacy stack-trace + source-file parser and the
    /// modern compile-time capture supplied via <see cref="System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"/>.
    /// Most callers will not need to set this; it predates compile-time expression capture and was originally
    /// added to give users a fallback when PDBs/source files were unavailable. That fallback is no longer
    /// necessary, but the flag is retained as a preference toggle.
    /// </remarks>
    public static IDisposable DisableSourceInErrors()
    {
        CallContext.LogicalSetData("ShouldlyDisableSourceInErrors", true);
        return new EnableSourceInErrorsDisposable();
    }

    /// <summary>
    /// Returns true when expression text should be omitted from error messages. See <see cref="DisableSourceInErrors"/>.
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

    private static int _assertCallerArgumentExpressionIsUsedCount;

    /// <summary>
    /// Test infrastructure: arms a process-wide trip-wire that throws
    /// <see cref="InvalidOperationException"/> if Shouldly ever falls back to stack-trace
    /// parsing to recover an assertion's call-site expression. Use to prove that
    /// <see cref="System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"/> capture is
    /// wired all the way through to the assertion message on a given test run.
    /// Stack-walking that is deliberately requested (via <see cref="DisableSourceInErrors"/>) or
    /// suppressed (via <see cref="AllowStackWalking"/>) does not trigger the trip-wire.
    /// </summary>
    public static IDisposable AssertCallerArgumentExpressionIsUsed()
    {
        System.Threading.Interlocked.Increment(ref _assertCallerArgumentExpressionIsUsedCount);
        return new DisarmTripWireDisposable();
    }

    /// <summary>
    /// Test infrastructure: scoped opt-out of the trip-wire armed by
    /// <see cref="AssertCallerArgumentExpressionIsUsed"/>. Use this around code paths that
    /// legitimately cannot use <see cref="System.Runtime.CompilerServices.CallerArgumentExpressionAttribute"/>
    /// — for example, calls that go through dynamic dispatch (where CAE doesn't fire) or
    /// obsolete <c>params</c>-array overloads.
    /// </summary>
    public static IDisposable AllowStackWalking()
    {
        CallContext.LogicalSetData("ShouldlyAllowStackWalking", true);
        return new AllowStackWalkingDisposable();
    }

    internal static bool IsCallerArgumentExpressionRequired()
    {
        if (_assertCallerArgumentExpressionIsUsedCount == 0) return false;
        if ((bool?)CallContext.LogicalGetData("ShouldlyAllowStackWalking") == true) return false;
        if (IsSourceDisabledInErrors()) return false;
        return true;
    }

    private class DisarmTripWireDisposable : IDisposable
    {
        public void Dispose() => System.Threading.Interlocked.Decrement(ref _assertCallerArgumentExpressionIsUsedCount);
    }

    private class AllowStackWalkingDisposable : IDisposable
    {
        public void Dispose() => CallContext.LogicalSetData("ShouldlyAllowStackWalking", null);
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