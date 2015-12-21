using System;
using System.Collections.Generic;
using System.Threading;

namespace Shouldly
{
    public static class ShouldlyConfiguration
    {
#if DOTNET5_4
        static readonly AsyncLocal<bool?> DisableSourceInErrorsSetting = new AsyncLocal<bool?>();
#endif
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
#if DOTNET5_4
            DisableSourceInErrorsSetting.Value = true;
#else
            System.Runtime.Remoting.Messaging.CallContext.SetData("ShouldlyDisableSourceInErrors", true);
#endif
            return new EnableSourceInErrorsDisposable();
        }

        public static bool IsSourceDisabledInErrors()
        {
#if DOTNET5_4
            return DisableSourceInErrorsSetting.Value == true;
#else
            return (bool?) System.Runtime.Remoting.Messaging.CallContext.GetData("ShouldlyDisableSourceInErrors") == true;
#endif
        }

        class EnableSourceInErrorsDisposable : IDisposable
        {
            public void Dispose()
            {
#if DOTNET5_4
                DisableSourceInErrorsSetting.Value = null;
#else
                System.Runtime.Remoting.Messaging.CallContext.SetData("ShouldlyDisableSourceInErrors", null);
#endif
            }
        }

        public static double DefaultFloatingPointTolerance = 0.0d;
        public static TimeSpan DefaultTaskTimeout = TimeSpan.FromSeconds(10);
    }
}