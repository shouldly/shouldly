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
    [MethodImpl(MethodImplOptions.NoInlining)]
    [RequiresUnreferencedCode("ShouldMatchApproved walks the stack trace to locate the test method and its source file. Methods and types reflected at runtime may be removed by the trimmer.")]
    public static void ShouldMatchApproved(this string actual, Action<ShouldMatchConfigurationBuilder>? configureOptions = null, string? customMessage = null,
        [CallerArgumentExpression(nameof(actual))] string? actualExpression = null)
    {
        var codeGetter = new ActualCodeTextGetter();
        var stackTrace = new StackTrace(true);
        codeGetter.GetCodeText(actual, stackTrace);

        var configurationBuilder = new ShouldMatchConfigurationBuilder(ShouldMatchConfiguration.ShouldMatchApprovedDefaults.Build());
        configureOptions?.Invoke(configurationBuilder);
        var config = configurationBuilder.Build();

        if (config.Scrubber != null)
            actual = config.Scrubber(actual);

        var testMethodInfo = config.TestMethodFinder.GetTestMethodInfo(stackTrace, codeGetter.ShouldlyFrameOffset);
        var discriminator = config.FilenameDiscriminator == null ? null : "." + config.FilenameDiscriminator;
        var outputFolder = testMethodInfo.SourceFileDirectory;

        if (string.IsNullOrEmpty(outputFolder))
            throw new($"Source information not available, make sure you are compiling with full debug information. Frame: {testMethodInfo.DeclaringTypeName}.{testMethodInfo.MethodName}");

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
            throw new($"Unable to resolve source file from deterministic build source path. Frame: {testMethodInfo.DeclaringTypeName}.{testMethodInfo.MethodName}");

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
