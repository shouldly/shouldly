// TODO Try and get this working with PCL
#if !PORTABLE
using System;
using System.Diagnostics;
using System.IO;
using Shouldly.Internals;
using Shouldly.Internals.AssertionFactories;

namespace Shouldly
{
    [ShouldlyMethods]
    public static class ShouldMatchApprovedTestExtensions
    {
        #if net35 || net40
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
            ShouldMatchApproved(actual, codeGetter.CallingFrame.GetMethod().Name, codeGetter.FileName, codeGetter.LineNumber, customMessage, configureOptions);
        }

        static void ShouldMatchApproved(
            this string actual,
            string memberName,
            string sourceFilePath,
            int sourceLineNumber,
            Func<string> customMessage,
            Action<ShouldMatchConfigurationBuilder> configureOptions)
#else
        public static void ShouldMatchApproved(this string actual,
            Func<string> customMessage,
            Action<ShouldMatchConfigurationBuilder> configureOptions,
            [System.Runtime.CompilerServices.CallerMemberName] string memberName = "",
            [System.Runtime.CompilerServices.CallerFilePath] string sourceFilePath = "",
            [System.Runtime.CompilerServices.CallerLineNumber] int sourceLineNumber = 0)
#endif
        {
            var configurationBuilder = new ShouldMatchConfigurationBuilder();
            configureOptions(configurationBuilder);
            var config = configurationBuilder.Build();
            var descriminator = config.FilenameDescriminator == null ? null : "." + config.FilenameDescriminator;
            var directoryName = Path.GetDirectoryName(sourceFilePath);
            var approvedFile = Path.Combine(directoryName, $"{memberName}{descriminator}.approved.txt");
            var receivedFile = Path.Combine(directoryName, $"{memberName}{descriminator}.received.txt");
            File.WriteAllText(receivedFile, actual);

            if (!File.Exists(approvedFile))
            {
                if (ConfigurationAllowsDiff(config))
                    ShouldlyConfiguration.DiffTools.GetDiffTool().Open(receivedFile, approvedFile);

                throw new ShouldAssertException($@"Approval file {approvedFile}
    does not exist");
            }

            var approvedFileContents = File.ReadAllText(approvedFile);
            var receivedFileContents = File.ReadAllText(receivedFile);
            var assertion = StringShouldBeAssertionFactory
                .Create(approvedFileContents, receivedFileContents, config.StringCompareOptions);
            var contentsMatch = assertion.IsSatisfied();

            if (!contentsMatch && ConfigurationAllowsDiff(config))
                ShouldlyConfiguration.DiffTools.GetDiffTool().Open(receivedFile, approvedFile);

            if (!contentsMatch)
                throw new ShouldAssertException(assertion.GenerateMessage(customMessage()));
            File.Delete(receivedFile);
        }

        static bool ConfigurationAllowsDiff(ShouldMatchConfiguration config)
        {
            return ShouldlyConfiguration.DiffTools.ShouldOpenDiffTool() && !config.PreventDiff;
        }
    }

    public class ShouldMatchConfigurationBuilder
    {
        readonly ShouldMatchConfiguration _config = new ShouldMatchConfiguration();

        public ShouldMatchConfigurationBuilder WithStringCompareOptions(StringCompareShould stringCompareOptions)
        {
            return Configure(c => c.StringCompareOptions = stringCompareOptions);
        }

        public ShouldMatchConfigurationBuilder WithDescriminator(string fileDescriminator)
        {
            return Configure(c => c.FilenameDescriminator = fileDescriminator);
        }

        public ShouldMatchConfigurationBuilder NoDiff()
        {
            return Configure(c => c.PreventDiff = true);
        }

        public ShouldMatchConfigurationBuilder IgnoreLineEndings()
        {
            return Configure(c => c.StringCompareOptions = c.StringCompareOptions | StringCompareShould.IgnoreLineEndings);
        }

        public ShouldMatchConfigurationBuilder Configure(Action<ShouldMatchConfiguration> configure)
        {
            configure(_config);
            return this;
        }

        public ShouldMatchConfiguration Build()
        {
            return _config;
        }
    }

    public class ShouldMatchConfiguration
    {
        public StringCompareShould StringCompareOptions { get; set; }
        public string FilenameDescriminator { get; set; }
        public bool PreventDiff { get; set; }
    }
}
#endif
