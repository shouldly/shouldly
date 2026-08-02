using System.ComponentModel;
using Shouldly.Internals;
using Shouldly.Internals.AssertionFactories;

namespace Shouldly;

/// <summary>
/// Extension methods for approval testing
/// </summary>
[ShouldlyMethods]
[EditorBrowsable(EditorBrowsableState.Never)]
public static class ShouldMatchApprovedTestExtensions
{
    /// <summary>
    /// Verifies that a string matches an approved file
    /// </summary>
    /// <param name="actual">The actual string to verify</param>
    /// <param name="configureOptions">Optional action to configure the approval options</param>
    /// <param name="customMessage">Optional custom message to display if the assertion fails</param>
    /// <param name="actualExpression">The source-level expression of the actual argument captured at the call site via <see cref="CallerArgumentExpressionAttribute"/>.</param>
    /// <param name="testMethodName">The name of the test method, captured at the call site via <see cref="CallerMemberNameAttribute"/>. Used to name the approval files. When wrapping this call in a helper method, capture the caller info on the helper and pass it through explicitly.</param>
    /// <param name="sourceFilePath">The path of the source file containing the test method, captured at the call site via <see cref="CallerFilePathAttribute"/>. The approval files are placed next to it and prefixed with its file name. When wrapping this call in a helper method, capture the caller info on the helper and pass it through explicitly.</param>
    public static void ShouldMatchApproved(this string actual, Action<ShouldMatchConfigurationBuilder>? configureOptions = null, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null,
        [CallerMemberName] string testMethodName = "",
        [CallerFilePath] string sourceFilePath = "")
    {
        var configurationBuilder = new ShouldMatchConfigurationBuilder(ShouldMatchConfiguration.ShouldMatchApprovedDefaults.Build());
        configureOptions?.Invoke(configurationBuilder);
        var config = configurationBuilder.Build();

        if (config.Scrubber != null)
            actual = config.Scrubber(actual);

        var resolvedSourceFilePath = DeterministicBuildHelpers.ResolveDeterministicPaths(sourceFilePath);
        var testMethodInfo = new TestMethodInfo(testMethodName, resolvedSourceFilePath);
        var discriminator = config.FilenameDiscriminator == null ? null : "." + config.FilenameDiscriminator;
        var outputFolder = testMethodInfo.SourceFileDirectory;

        if (string.IsNullOrEmpty(outputFolder))
            throw new($"Source information not available: the compiler did not supply a file path for the call site of '{testMethodName}'. If you are invoking ShouldMatchApproved through reflection or generated code, pass {nameof(testMethodName)} and {nameof(sourceFilePath)} explicitly.");

        if (!string.IsNullOrEmpty(config.ApprovalFileSubFolder))
        {
            outputFolder = Path.Combine(outputFolder, config.ApprovalFileSubFolder);
        }

        var approvedFile = Path.Combine(outputFolder, config.FilenameGenerator(testMethodInfo, discriminator, "approved", config.FileExtension));
        var receivedFile = Path.Combine(outputFolder, config.FilenameGenerator(testMethodInfo, discriminator, "received", config.FileExtension));

        // Check the resolved file paths, not the raw source directory — a custom FilenameGenerator
        // may produce an absolute path that resolves the deterministic prefix itself.
        if (DeterministicBuildHelpers.PathAppearsToBeDeterministic(approvedFile) ||
            DeterministicBuildHelpers.PathAppearsToBeDeterministic(receivedFile))
            throw new($"Unable to resolve source file from deterministic build source path. Test method: {testMethodInfo.SourceFileName}.{testMethodInfo.MethodName}");

        if (!string.IsNullOrEmpty(config.ApprovalFileSubFolder))
        {
            var directoryToCreate = Path.GetDirectoryName(receivedFile);
            if (!string.IsNullOrEmpty(directoryToCreate))
                Directory.CreateDirectory(directoryToCreate);
        }

        File.WriteAllText(receivedFile, actual);

        if (!File.Exists(approvedFile))
        {
            if (!config.PreventDiff && config.DiffViewer != null)
            {
                config.DiffViewer.Launch(receivedFile, approvedFile);
            }

            throw new ShouldMatchApprovedException($"""
                                                    Approval file {approvedFile}
                                                        does not exist
                                                    """, receivedFile, approvedFile, !config.PreventDiff && config.DiffViewer == null);
        }

        var approvedFileContents = File.ReadAllText(approvedFile);
        var receivedFileContents = File.ReadAllText(receivedFile);
        var assertion = StringShouldBeAssertionFactory
            .Create(approvedFileContents, receivedFileContents, config.StringCompareOptions, actualExpression: actualExpression);
        var contentsMatch = assertion.IsSatisfied();

        if (!contentsMatch)
        {
            if (!config.PreventDiff && config.DiffViewer != null)
            {
                config.DiffViewer.Launch(receivedFile, approvedFile);
            }

            throw new ShouldMatchApprovedException(assertion.GenerateMessage(customMessage), receivedFile, approvedFile, !config.PreventDiff && config.DiffViewer == null);
        }

        File.Delete(receivedFile);
    }
}
