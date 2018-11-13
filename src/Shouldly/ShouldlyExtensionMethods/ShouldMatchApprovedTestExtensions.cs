#if ShouldMatchApproved
using System;
using System.Diagnostics;
using System.IO;
using Shouldly.Configuration;
using Shouldly.Internals;
using Shouldly.Internals.AssertionFactories;

namespace Shouldly
{
    [ShouldlyMethods]
    public static class ShouldMatchApprovedTestExtensions
    {
        public static void ShouldMatchApproved(this string actual)
        {
            actual.ShouldMatchApproved(() => null, c => { });
        }
        public static void ShouldMatchApproved(this string actual, string customMessage)
        {
            actual.ShouldMatchApproved(() => customMessage, c => { });
        }

        public static void ShouldMatchApproved(this string actual, Action<ShouldMatchConfigurationBuilder> configureOptions)
        {
            actual.ShouldMatchApproved(() => null, configureOptions);
        }

        public static void ShouldMatchApproved(this string actual, 
            string customMessage,
            Action<ShouldMatchConfigurationBuilder> configureOptions)
        {
            actual.ShouldMatchApproved(() => customMessage, configureOptions);
        }

        public static void ShouldMatchApproved(this string actual, Func<string> customMessage, Action<ShouldMatchConfigurationBuilder> configureOptions)
        {
            var codeGetter = new ActualCodeTextGetter();
            var stackTrace = new StackTrace(true);
            codeGetter.GetCodeText(actual, stackTrace);

            var configurationBuilder = new ShouldMatchConfigurationBuilder(ShouldlyConfiguration.ShouldMatchApprovedDefaults.Build());
            configureOptions(configurationBuilder);
            var config = configurationBuilder.Build();

            if (config.Scrubber != null)
                actual = config.Scrubber(actual);

            var testMethodInfo = config.TestMethodFinder.GetTestMethodInfo(stackTrace, codeGetter.ShouldlyFrameIndex);
            var descriminator = config.FilenameDescriminator == null ? null : "." + config.FilenameDescriminator;
            var outputFolder = testMethodInfo.SourceFileDirectory;
            if (string.IsNullOrEmpty(outputFolder))
                throw new Exception($"Source information not available, make sure you are compiling with full debug information. Frame: {testMethodInfo.DeclaringTypeName}.{testMethodInfo.MethodName}");
            if (!string.IsNullOrEmpty(config.ApprovalFileSubFolder))
            {
                outputFolder = Path.Combine(outputFolder, config.ApprovalFileSubFolder);
                Directory.CreateDirectory(outputFolder);
            }

            var approvedFile = Path.Combine(outputFolder, config.FilenameGenerator(testMethodInfo, descriminator, "approved", config.FileExtension));
            var receivedFile = Path.Combine(outputFolder, config.FilenameGenerator(testMethodInfo, descriminator, "received", config.FileExtension));
            File.WriteAllText(receivedFile, actual);

            if (!File.Exists(approvedFile))
            {
                if (ConfigurationAllowsDiff(config))
                    ShouldlyConfiguration.DiffTools.GetDiffTool().Open(receivedFile, approvedFile, false);

                throw new ShouldMatchApprovedException($@"Approval file {approvedFile}
    does not exist", receivedFile, approvedFile);
            }

            var approvedFileContents = File.ReadAllText(approvedFile);
            var receivedFileContents = File.ReadAllText(receivedFile);
            var assertion = StringShouldBeAssertionFactory
                .Create(approvedFileContents, receivedFileContents, config.StringCompareOptions);
            var contentsMatch = assertion.IsSatisfied();

            if (!contentsMatch && ConfigurationAllowsDiff(config))
                ShouldlyConfiguration.DiffTools.GetDiffTool().Open(receivedFile, approvedFile, true);

            if (!contentsMatch)
                throw new ShouldMatchApprovedException(assertion.GenerateMessage(customMessage()), receivedFile, approvedFile);
            File.Delete(receivedFile);
        }

        static bool ConfigurationAllowsDiff(ShouldMatchConfiguration config)
        {
            return ShouldlyConfiguration.DiffTools.ShouldOpenDiffTool() && !config.PreventDiff;
        }
    }
}
#endif
