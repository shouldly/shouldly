using System.Collections.Generic;
using System.Diagnostics;

namespace Shouldly.Tests.ShouldBe
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class DynamicShould
    {
        public static void HaveProperty(dynamic dynamicTestObject, string p)
        {
            var dynamicAsDictionary = (IDictionary<string, object>)dynamicTestObject;

            if (! dynamicAsDictionary.ContainsKey(p))
            {
                throw new ChuckedAWobbly(new ShouldlyMessage(p).ToString());
            }
        }
    }
}