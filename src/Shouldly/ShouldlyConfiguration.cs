using System;
using System.Collections.Generic;

#if ShouldMatchApproved
using Shouldly.Configuration;
#endif

#if ShouldMatchApproved && !CallContextPolyfill
using System.Runtime.Remoting.Messaging;
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
#endif

        /// <summary>
        /// When set to true shouldly will not try and create better error messages using your source code
        /// </summary>
        public static IDisposable DisableSourceInErrors()
        {
            CallContext.LogicalSetData("ShouldlyDisableSourceInErrors", true);
            return new EnableSourceInErrorsDisposable();
        }

        public static bool IsSourceDisabledInErrors()
        {
            return (bool?) CallContext.LogicalGetData("ShouldlyDisableSourceInErrors") == true;
        }

        class EnableSourceInErrorsDisposable : IDisposable
        {
            public void Dispose()
            {
                CallContext.LogicalSetData("ShouldlyDisableSourceInErrors", null);
            }
        }

        public static double DefaultFloatingPointTolerance = 0.0d;
        public static TimeSpan DefaultTaskTimeout = TimeSpan.FromSeconds(10);
    }
}