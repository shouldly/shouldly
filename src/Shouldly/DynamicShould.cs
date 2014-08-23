using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Linq;
using Microsoft.CSharp.RuntimeBinder;
using Shouldly.Internals;

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
                var stackTrace = new StackTrace(true);
                StackFrame stackFrameForCallingMethod = null;

                foreach (var stackFrame in stackTrace.GetFrames().Skip(1)) // Skip the current method call, i.e the method we are in right now
                {
                    // Having a method that takes a dynamic really stuffs up the stack trace because the runtime binder
                    // has to inject a whole heap of methods. Our normal way of walking up the stack trace doesn't really work.
                    // The following two lines seem to work for now, but this feels like a hack. The conditions to be able to 
                    // walk up stack trace until we get to the calling method might have to be updated regularly as we find more
                    // scanarios. Alternately, it could be replaced with a more robust implementation
                    if (stackFrame.GetMethod().DeclaringType != null &&
                         !stackFrame.GetMethod().DeclaringType.FullName.StartsWith("System.Dynamic"))
                    {
                        stackFrameForCallingMethod = stackFrame;
                        break;
                    }
                }

                var testContext = new DynamicTestContext()
                                        {
                                            DynamicTestObject = dynamicTestObject,
                                            HavePropertyName = p,
                                            CallingMethodStackFrame = stackFrameForCallingMethod
                                        };

                throw new ChuckedAWobbly(new ShouldlyMessage(testContext).ToString());
            }
        }

    }
}