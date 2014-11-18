using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;

namespace Shouldly
{
    [DebuggerStepThrough]
    [ShouldlyMethods]
    public static class DynamicShould
    {
        public static void HaveProperty(dynamic dynamicTestObject, string p)
        {
            if (dynamicTestObject is IDynamicMetaObjectProvider)
            {
                var dynamicAsDictionary = (IDictionary<string, object>)dynamicTestObject;

                if (!dynamicAsDictionary.ContainsKey(p))
                {
                    throw new ChuckedAWobbly(new ExpectedShouldlyMessage(p).ToString());
                }
            }
            else
            {
                var dynamicAsObject = (object)dynamicTestObject;
                if (!dynamicAsObject.GetType().GetProperties().Select(x => x.Name).Contains(p))
                {
                    throw new ChuckedAWobbly(new ExpectedShouldlyMessage(p).ToString());
                }
            }
        }
    }
}