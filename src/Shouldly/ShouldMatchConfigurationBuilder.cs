using System.ComponentModel;

namespace Shouldly;

/// <summary>
/// Builder for creating ShouldMatchConfiguration instances
/// </summary>
public class ShouldMatchConfigurationBuilder
{
    private readonly ShouldMatchConfiguration _config;

    /// <summary>
    /// Initializes a new instance of the ShouldMatchConfigurationBuilder class
    /// </summary>
    /// <param name="initialConfig">The initial configuration to use</param>
    public ShouldMatchConfigurationBuilder(ShouldMatchConfiguration initialConfig) =>
        _config = new(initialConfig);

    /// <summary>
    /// Sets the string comparison options
    /// </summary>
    /// <param name="stringCompareOptions">The string comparison options to use</param>
    /// <returns>The configuration builder for method chaining</returns>
    public ShouldMatchConfigurationBuilder WithStringCompareOptions(StringCompareShould stringCompareOptions) =>
        Configure(c => c.StringCompareOptions = stringCompareOptions);

    /// <summary>
    /// Sets the filename discriminator
    /// </summary>
    /// <param name="fileDiscriminator">The discriminator to add to the filename</param>
    /// <returns>The configuration builder for method chaining</returns>
    public ShouldMatchConfigurationBuilder WithDiscriminator(string fileDiscriminator) =>
        Configure(c => c.FilenameDiscriminator = fileDiscriminator);

    /// <summary>
    /// Prevents showing a diff when the test fails
    /// </summary>
    /// <returns>The configuration builder for method chaining</returns>
    public ShouldMatchConfigurationBuilder NoDiff() =>
        Configure(c => c.PreventDiff = true);

    /// <summary>
    /// Sets the file extension for approval files
    /// </summary>
    /// <param name="fileExtension">The file extension to use</param>
    /// <returns>The configuration builder for method chaining</returns>
    public ShouldMatchConfigurationBuilder WithFileExtension(string fileExtension) =>
        Configure(c => c.FileExtension = fileExtension.TrimStart('.'));

    /// <summary>
    /// Sets the filename generator for approval files
    /// </summary>
    /// <param name="filenameGenerator">The filename generator to use</param>
    /// <returns>The configuration builder for method chaining</returns>
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
    /// No longer supported: ShouldMatchApproved no longer walks the stack. Have your helper method
    /// capture <see cref="CallerMemberNameAttribute"/>/<see cref="CallerFilePathAttribute"/> parameters
    /// and pass them to ShouldMatchApproved's <c>testMethodName</c>/<c>sourceFilePath</c> parameters.
    /// </summary>
    [Obsolete("ShouldMatchApproved no longer walks the stack; it captures the test method via [CallerMemberName]/[CallerFilePath]. Have your helper method capture the same caller info parameters and pass them through to ShouldMatchApproved's testMethodName/sourceFilePath parameters.", error: true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ShouldMatchConfigurationBuilder UseCallerLocation() => this;

    /// <summary>
    /// No longer supported: ShouldMatchApproved no longer walks the stack. Have your helper method
    /// capture <see cref="CallerMemberNameAttribute"/>/<see cref="CallerFilePathAttribute"/> parameters
    /// and pass them to ShouldMatchApproved's <c>testMethodName</c>/<c>sourceFilePath</c> parameters.
    /// </summary>
    [Obsolete("ShouldMatchApproved no longer walks the stack; it captures the test method via [CallerMemberName]/[CallerFilePath]. Have your helper method capture the same caller info parameters and pass them through to ShouldMatchApproved's testMethodName/sourceFilePath parameters.", error: true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public ShouldMatchConfigurationBuilder LocateTestMethodUsingAttribute<T>()
        where T : Attribute => this;

    /// <summary>
    /// Sets a scrubber function to modify the received content before comparison
    /// </summary>
    /// <param name="scrubber">The scrubber function to apply</param>
    /// <returns>The configuration builder for method chaining</returns>
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

    /// <summary>
    /// Configures the underlying configuration object
    /// </summary>
    /// <param name="configure">The action to configure the configuration</param>
    /// <returns>The configuration builder for method chaining</returns>
    public ShouldMatchConfigurationBuilder Configure(Action<ShouldMatchConfiguration> configure)
    {
        configure(_config);
        return this;
    }

    /// <summary>
    /// Builds the final configuration object
    /// </summary>
    /// <returns>The configured ShouldMatchConfiguration</returns>
    public ShouldMatchConfiguration Build() => _config;
}