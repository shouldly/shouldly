using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using DiffEngine;
using Shouldly.Configuration;
using Shouldly.Internals;
using Shouldly.Internals.AssertionFactories;

namespace Shouldly
{
    [ShouldlyMethods]
    public static partial class ShouldMatchApprovedTestExtensions
    {
        public static void ShouldMatchApproved(this string actual, Action<ShouldMatchConfigurationBuilder>? configureOptions = null, string? customMessage = null)
        {
            var codeGetter = new ActualCodeTextGetter();
            var stackTrace = new StackTrace(true);
            codeGetter.GetCodeText(actual, stackTrace);

            var configurationBuilder = new ShouldMatchConfigurationBuilder(ShouldlyConfiguration.ShouldMatchApprovedDefaults.Build());
            configureOptions?.Invoke(configurationBuilder);
            var config = configurationBuilder.Build();

            if (config.Scrubber != null)
                actual = config.Scrubber(actual);

            var testMethodInfo = config.TestMethodFinder.GetTestMethodInfo(stackTrace, codeGetter.ShouldlyFrameOffset);
            var discriminator = config.FilenameDiscriminator == null ? null : "." + config.FilenameDiscriminator;
            var outputFolder = testMethodInfo.SourceFileDirectory;
            if (string.IsNullOrEmpty(outputFolder))
                throw new Exception($"Source information not available, make sure you are compiling with full debug information. Frame: {testMethodInfo.DeclaringTypeName}.{testMethodInfo.MethodName}");
            if (!string.IsNullOrEmpty(config.ApprovalFileSubFolder))
            {
                outputFolder = Path.Combine(outputFolder, config.ApprovalFileSubFolder);
                Directory.CreateDirectory(outputFolder);
            }

            var approvedFile = Path.Combine(outputFolder, config.FilenameGenerator(testMethodInfo, discriminator, "approved", config.FileExtension));
            var receivedFile = Path.Combine(outputFolder, config.FilenameGenerator(testMethodInfo, discriminator, "received", config.FileExtension));
            File.WriteAllText(receivedFile, actual);

            if (!File.Exists(approvedFile))
            {
                if (!config.PreventDiff)
                {
                    DiffRunner.Launch(receivedFile, approvedFile);
                }

                throw new ShouldMatchApprovedException($@"Approval file {approvedFile}
    does not exist", receivedFile, approvedFile);
            }

            var approvedFileContents = File.ReadAllText(approvedFile);
            var receivedFileContents = File.ReadAllText(receivedFile);
            var assertion = StringShouldBeAssertionFactory
                .Create(approvedFileContents, receivedFileContents, config.StringCompareOptions);
            var contentsMatch = assertion.IsSatisfied();

            if (!contentsMatch)
            {
                if (!config.PreventDiff)
                {
                    DiffRunner.Launch(receivedFile, approvedFile);
                }

                throw new ShouldMatchApprovedException(assertion.GenerateMessage(customMessage), receivedFile, approvedFile);
            }

            File.Delete(receivedFile);
        }

        public static void ShouldMatchApproved(this byte[] actual, Action<ShouldMatchConfigurationBuilder>? configureOptions = null, string? customMessage = null)
        {
            var codeGetter = new ActualCodeTextGetter();
            var stackTrace = new StackTrace(true);
            codeGetter.GetCodeText(actual, stackTrace);

            var configurationBuilder = new ShouldMatchConfigurationBuilder(ShouldlyConfiguration.ShouldMatchApprovedDefaults.Build());
            configureOptions?.Invoke(configurationBuilder);
            var config = configurationBuilder.Build();

            var testMethodInfo = config.TestMethodFinder.GetTestMethodInfo(stackTrace, codeGetter.ShouldlyFrameOffset);
            var discriminator = config.FilenameDiscriminator == null ? null : "." + config.FilenameDiscriminator;
            var outputFolder = testMethodInfo.SourceFileDirectory;
            if (string.IsNullOrEmpty(outputFolder))
                throw new Exception($"Source information not available, make sure you are compiling with full debug information. Frame: {testMethodInfo.DeclaringTypeName}.{testMethodInfo.MethodName}");
            if (!string.IsNullOrEmpty(config.ApprovalFileSubFolder))
            {
                outputFolder = Path.Combine(outputFolder, config.ApprovalFileSubFolder);
                Directory.CreateDirectory(outputFolder);
            }

            var approvedFile = Path.Combine(outputFolder, config.FilenameGenerator(testMethodInfo, discriminator, "approved", config.FileExtension));
            var receivedFile = Path.Combine(outputFolder, config.FilenameGenerator(testMethodInfo, discriminator, "received", config.FileExtension));
            File.WriteAllBytes(receivedFile, actual);

            if (!File.Exists(approvedFile))
            {
                if (!config.PreventDiff)
                {
                    DiffRunner.Launch(receivedFile, approvedFile);
                }

                throw new ShouldMatchApprovedException($@"Approval file {approvedFile}
    does not exist", receivedFile, approvedFile);
            }

            var approvedFileContents = File.ReadAllBytes(approvedFile);
            var contentsMatch = Enumerable.SequenceEqual(approvedFileContents, actual);

            if (!contentsMatch)
            {
                if (!config.PreventDiff)
                {
                    DiffRunner.Launch(receivedFile, approvedFile);
                }

                throw new ShouldMatchApprovedException($@"Approval file {approvedFile}
    does not match", receivedFile, approvedFile);
            }

            File.Delete(receivedFile);
        }
    }
}