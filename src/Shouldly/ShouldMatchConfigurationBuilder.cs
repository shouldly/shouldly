namespace Shouldly;

public class ShouldMatchConfigurationBuilder
{
    private readonly ShouldMatchConfiguration _config;

    public ShouldMatchConfigurationBuilder(ShouldMatchConfiguration initialConfig) =>
        _config = new(initialConfig);

    public ShouldMatchConfigurationBuilder WithStringCompareOptions(StringCompareShould stringCompareOptions) =>
        Configure(c => c.StringCompareOptions = stringCompareOptions);

    public ShouldMatchConfigurationBuilder WithDiscriminator(string fileDiscriminator) =>
        Configure(c => c.FilenameDiscriminator = fileDiscriminator);

    public ShouldMatchConfigurationBuilder NoDiff() =>
        Configure(c => c.PreventDiff = true);

    public ShouldMatchConfigurationBuilder WithFileExtension(string fileExtension) =>
        Configure(c => c.FileExtension = fileExtension.TrimStart('.'));

    public ShouldMatchConfigurationBuilder WithFilenameGenerator(FilenameGenerator filenameGenerator) =>
        Configure(c => c.FilenameGenerator = filenameGenerator);

    /// <summary>
    /// Default is to ignore line endings
    /// </summary>
    public ShouldMatchConfigurationBuilder DoNotIgnoreLineEndings() =>
        Configure(c =>
        {
            if ((c.StringCompareOptions & StringCompareShould.IgnoreLineEndings) ==
                StringCompareShould.IgnoreLineEndings)
                c.StringCompareOptions &= ~StringCompareShould.IgnoreLineEndings;
        });

    /// <summary>
    /// Places the .approved and .received files into a subfolder
    /// </summary>
    public ShouldMatchConfigurationBuilder SubFolder(string subfolder) =>
        Configure(c => c.ApprovalFileSubFolder = subfolder);

    /// <summary>
    /// Tells shouldly to use this methods caller for naming. Useful when you have created a test helper
    /// </summary>
    public ShouldMatchConfigurationBuilder UseCallerLocation() =>
        Configure(c => c.TestMethodFinder =
            new FirstNonShouldlyMethodFinder
            {
                Offset = 1
            });

    /// <summary>
    /// Tells shouldly to use this methods caller for naming. Useful when you have created a test helper
    /// </summary>
    public ShouldMatchConfigurationBuilder LocateTestMethodUsingAttribute<T>()
        where T : Attribute =>
        Configure(c => c.TestMethodFinder = new FindMethodUsingAttribute<T>());

    public ShouldMatchConfigurationBuilder WithScrubber(Func<string, string> scrubber) =>
        Configure(c =>
        {
            if (c.Scrubber == null)
            {
                c.Scrubber = scrubber;
            }
            else
            {
                var existing = c.Scrubber;
                c.Scrubber = s => existing(scrubber(s));
            }
        });

    public ShouldMatchConfigurationBuilder Configure(Action<ShouldMatchConfiguration> configure)
    {
        configure(_config);
        return this;
    }

    public ShouldMatchConfiguration Build() => _config;
}