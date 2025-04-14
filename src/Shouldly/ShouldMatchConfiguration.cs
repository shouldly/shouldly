namespace Shouldly;

/// <summary>
/// Configuration for approval testing
/// </summary>
public class ShouldMatchConfiguration
{
    /// <summary>
    /// Default configuration for approval testing
    /// </summary>
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
    /// Initializes a new instance of the ShouldMatchConfiguration class with default values
    /// </summary>
    public ShouldMatchConfiguration()
    {
    }

    /// <summary>
    /// Initializes a new instance of the ShouldMatchConfiguration class with values from another configuration
    /// </summary>
    /// <param name="initialConfig">The configuration to copy values from</param>
    public ShouldMatchConfiguration(ShouldMatchConfiguration initialConfig)
    {
        StringCompareOptions = initialConfig.StringCompareOptions;
        FilenameDiscriminator = initialConfig.FilenameDiscriminator;
        PreventDiff = initialConfig.PreventDiff;
        DiffViewer = initialConfig.DiffViewer;
        FileExtension = initialConfig.FileExtension;
        TestMethodFinder = initialConfig.TestMethodFinder;
        ApprovalFileSubFolder = initialConfig.ApprovalFileSubFolder;
        Scrubber = initialConfig.Scrubber;
        FilenameGenerator = initialConfig.FilenameGenerator;
    }

    /// <summary>
    /// Options for string comparison
    /// </summary>
    public StringCompareShould StringCompareOptions { get; set; } = StringCompareShould.IgnoreLineEndings;

    /// <summary>
    /// Optional discriminator to add to the filename
    /// </summary>
    public string? FilenameDiscriminator { get; set; }

    /// <summary>
    /// Whether to prevent showing a diff when the test fails
    /// </summary>
    public bool PreventDiff { get; set; } = false;

    /// <summary>
    /// The diff viewer to use when the test fails
    /// </summary>
    public IDiffViewer? DiffViewer { get; set; }

    /// <summary>
    /// File extension without the period
    /// </summary>
    public string FileExtension { get; set; } = "txt";

    /// <summary>
    /// The test method finder to use to locate the test method
    /// </summary>
    public ITestMethodFinder TestMethodFinder { get; set; } = new FirstNonShouldlyMethodFinder();

    /// <summary>
    /// Optional subfolder for approval files
    /// </summary>
    public string? ApprovalFileSubFolder { get; set; }

    /// <summary>
    /// Scrubbers allow you to alter the received document before comparing it to approved.
    ///
    /// This is useful for replacing dates or dynamic data with fixed data
    /// </summary>
    public Func<string, string>? Scrubber { get; set; }

    /// <summary>
    /// Generator for approval file names
    /// </summary>
    public FilenameGenerator FilenameGenerator { get; set; } =
        (testMethodInfo, discriminator, type, extension)
            => $"{testMethodInfo.DeclaringTypeName}.{testMethodInfo.MethodName}{discriminator}.{type}.{extension}";
}