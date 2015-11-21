using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

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

        /// <summary>
        /// When set to true shouldly will not try and create better error messages using your source code
        /// </summary>
        public static IDisposable DisableSourceInErrors()
        {
            CallContext.SetData("ShouldlyDisableSourceInErrors", true);
            return new EnableSourceInErrorsDisposable();
        }

        public static bool IsSourceDisabledInErrors()
        {
            return (bool?) CallContext.GetData("ShouldlyDisableSourceInErrors") == true;
        }

        class EnableSourceInErrorsDisposable : IDisposable
        {
            public void Dispose()
            {
                CallContext.SetData("ShouldlyDisableSourceInErrors", null);
            }
        }

        public static double DefaultFloatingPointTolerance = 0.0d;
        public static TimeSpan DefaultTaskTimeout = TimeSpan.FromSeconds(10);
    }
}