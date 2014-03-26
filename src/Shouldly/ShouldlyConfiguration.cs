using System;
using System.Collections.Generic;

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
        public static double DefaultFloatingPointTolerance = 0.0d;
        public static TimeSpan DefaultTaskTimeout = TimeSpan.FromSeconds(10);
    }
}