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
                "Shouldly.Tests.TestHelpers.Strange",
                "System.String"
            };
        }

        public static List<string> CompareAsObjectTypes { get; private set; }
    }
}