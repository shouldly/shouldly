using System;
using System.Collections.Generic;

#if ShouldMatchApproved
using Shouldly.Configuration;
using System.Threading;
#endif

namespace Shouldly
{
    public static class ShouldlyConfiguration
    {
        static ShouldlyConfiguration()
        {
            CompareAsObjectTypes = new List<string>
            {
                "Newtonsoft.Json.Linq.JToken",
                "Shouldly.Tests.TestHelpers.Strange"
            };
        }

        public static List<string> CompareAsObjectTypes { get; private set; }
#if ShouldMatchApproved
        private static AsyncLocal<bool> ShouldlyDisableSourceInErrors = new AsyncLocal<bool>();
        private static Lazy<DiffToolConfiguration> _lazyDiffTools = new Lazy<DiffToolConfiguration>(() => new DiffToolConfiguration());
        public static DiffToolConfiguration DiffTools {
            get => _lazyDiffTools.Value;
            private set {
                _lazyDiffTools = new Lazy<DiffToolConfiguration>(() => value);
            }
        }

        public static ShouldMatchConfigurationBuilder ShouldMatchApprovedDefaults { get; private set; } =
            new ShouldMatchConfigurationBuilder(new ShouldMatchConfiguration
            {
                StringCompareOptions = StringCompareShould.IgnoreLineEndings,
                TestMethodFinder = new FirstNonShouldlyMethodFinder(),
                FileExtension = "txt",
                FilenameGenerator = (testMethodInfo, descriminator, type, extension)
                    => $"{testMethodInfo.DeclaringTypeName}.{testMethodInfo.MethodName}{descriminator}.{type}.{extension}"
            });

        /// <summary>
        /// When set to true shouldly will not try and create better error messages using your source code
        /// </summary>
        public static IDisposable DisableSourceInErrors()
        {
            ShouldlyDisableSourceInErrors.Value = true;
            return new EnableSourceInErrorsDisposable();
        }

        public static bool IsSourceDisabledInErrors()
        {
            return ShouldlyDisableSourceInErrors.Value == true;
        }

        class EnableSourceInErrorsDisposable : IDisposable
        {
            public void Dispose()
            {
                ShouldlyDisableSourceInErrors.Value = false;
            }
        }
#endif

        public static double DefaultFloatingPointTolerance = 0.0d;
        public static TimeSpan DefaultTaskTimeout = TimeSpan.FromSeconds(10);
    }
}