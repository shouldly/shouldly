#if !PORTABLE
using System;

namespace Shouldly.Configuration
{
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

        public ShouldMatchConfigurationBuilder WithFileExtension(string fileExtension)
        {
            return Configure(c => c.FileExtension = fileExtension.TrimStart('.'));
        }

        public ShouldMatchConfigurationBuilder IgnoreLineEndings()
        {
            return Configure(c => c.StringCompareOptions = c.StringCompareOptions | StringCompareShould.IgnoreLineEndings);
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