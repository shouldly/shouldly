using Shouldly.Configuration;

namespace Shouldly;

public static partial class ShouldlyConfiguration
{
    static ShouldlyConfiguration()
    {
        CompareAsObjectTypes = new()
        {
            "Newtonsoft.Json.Linq.JToken",
            "Shouldly.Tests.TestHelpers.Strange"
        };
    }

    public static List<string> CompareAsObjectTypes { get; }

    public static ShouldMatchConfigurationBuilder ShouldMatchApprovedDefaults { get; } =
        new(new()
        {
            StringCompareOptions = StringCompareShould.IgnoreLineEndings,
            TestMethodFinder = new FirstNonShouldlyMethodFinder(),
            FileExtension = "txt",
            FilenameGenerator = (testMethodInfo, discriminator, type, extension)
                => $"{testMethodInfo.DeclaringTypeName}.{testMethodInfo.MethodName}{discriminator}.{type}.{extension}"
        });

    /// <summary>
    /// When set to true shouldly will not try and create better error messages using your source code
    /// </summary>
    public static IDisposable DisableSourceInErrors()
    {
        CallContext.LogicalSetData("ShouldlyDisableSourceInErrors", true);
        return new EnableSourceInErrorsDisposable();
    }

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

    public static double DefaultFloatingPointTolerance = 0.0d;
    public static TimeSpan DefaultTaskTimeout = TimeSpan.FromSeconds(10);
}