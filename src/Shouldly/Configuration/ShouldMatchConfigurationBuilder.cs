#if ShouldMatchApproved
using System;

namespace Shouldly.Configuration
{
    public class ShouldMatchConfigurationBuilder
    {
        readonly ShouldMatchConfiguration _config;

        public ShouldMatchConfigurationBuilder(ShouldMatchConfiguration initialConfig)
        {
            _config = new ShouldMatchConfiguration(initialConfig);
        }

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

        public ShouldMatchConfigurationBuilder WithFileExtension(string fileExtension)
        {
            return Configure(c => c.FileExtension = fileExtension.TrimStart('.'));
        }

        public ShouldMatchConfigurationBuilder WithFilenameGenerator(FilenameGenerator filenameGenerator)
        {
            return Configure(c => c.FilenameGenerator = filenameGenerator);
        }

        /// <summary>
        /// Default is to ignore line endings
        /// </summary>
        public ShouldMatchConfigurationBuilder DoNotIgnoreLineEndings()
        {
            return Configure(c =>
            {
                if ((c.StringCompareOptions & StringCompareShould.IgnoreLineEndings) ==
                    StringCompareShould.IgnoreLineEndings)
                    c.StringCompareOptions &= ~StringCompareShould.IgnoreLineEndings;
            });
        }

        /// <summary>
        /// Places the .approved and .received files into a subfolder
        /// </summary>
        public ShouldMatchConfigurationBuilder SubFolder(string subfolder)
        {
            return Configure(c => c.ApprovalFileSubFolder = subfolder);
        }

        /// <summary>
        /// Tells shouldly to use this methods caller for naming. Useful when you have created a test helper
        /// </summary>
        public ShouldMatchConfigurationBuilder UseCallerLocation()
        {
            return Configure(c => c.TestMethodFinder = new FirstNonShouldlyMethodFinder
            {
                Offset = 1
            });
        }

        /// <summary>
        /// Tells shouldly to use this methods caller for naming. Useful when you have created a test helper
        /// </summary>
        public ShouldMatchConfigurationBuilder LocateTestMethodUsingAttribute<T>() where T : Attribute
        {
            return Configure(c => c.TestMethodFinder = new FindMethodUsingAttribute<T>());
        }

        public ShouldMatchConfigurationBuilder WithScrubber(Func<string, string> scrubber)
        {
            return Configure(c =>
            {
                if (c.Scrubber == null)
                    c.Scrubber = scrubber;
                else
                {
                    var existing = c.Scrubber;
                    c.Scrubber = s => existing(scrubber(s));
                }
            });
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
}
#endif