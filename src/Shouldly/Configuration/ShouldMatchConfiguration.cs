using System;

namespace Shouldly.Configuration
{
    public delegate string FilenameGenerator(
        TestMethodInfo testMethodInfo, string? discriminator, string fileType, string fileExtension);

    public class ShouldMatchConfiguration
    {
        public ShouldMatchConfiguration()
        {
        }

        public ShouldMatchConfiguration(ShouldMatchConfiguration initialConfig)
        {
            StringCompareOptions = initialConfig.StringCompareOptions;
            FilenameDiscriminator = initialConfig.FilenameDiscriminator;
            PreventDiff = initialConfig.PreventDiff;
            FileExtension = initialConfig.FileExtension;
            TestMethodFinder = initialConfig.TestMethodFinder;
            ApprovalFileSubFolder = initialConfig.ApprovalFileSubFolder;
            Scrubber = initialConfig.Scrubber;
            FilenameGenerator = initialConfig.FilenameGenerator;
        }

        public StringCompareShould StringCompareOptions { get; set; } = StringCompareShould.IgnoreLineEndings;
        public string? FilenameDiscriminator { get; set; }
        public bool PreventDiff { get; set; }

        /// <summary>
        /// File extension without the.
        /// </summary>
        public string FileExtension { get; set; } = "txt";

        public ITestMethodFinder TestMethodFinder { get; set; } = new FirstNonShouldlyMethodFinder();
        public string? ApprovalFileSubFolder { get; set; }

        /// <summary>
        /// Scrubbers allow you to alter the received document before comparing it to approved.
        ///
        /// This is useful for replacing dates or dynamic data with fixed data
        /// </summary>
        public Func<string, string>? Scrubber { get; set; }

        public FilenameGenerator FilenameGenerator { get; set; } =
            (testMethodInfo, discriminator, type, extension)
                => $"{testMethodInfo.DeclaringTypeName}.{testMethodInfo.MethodName}{discriminator}.{type}.{extension}";
    }
}
