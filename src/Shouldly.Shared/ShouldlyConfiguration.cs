using System;
using System.Collections.Generic;
#if !PORTABLE
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
#if !PORTABLE
        public static DiffToolConfiguration DiffTools { get; private set; } = new DiffToolConfiguration();

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
#endif

        public static double DefaultFloatingPointTolerance = 0.0d;
        public static TimeSpan DefaultTaskTimeout = TimeSpan.FromSeconds(10);
    }
}