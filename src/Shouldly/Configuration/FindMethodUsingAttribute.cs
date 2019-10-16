#if ShouldMatchApproved
using System;
using System.Diagnostics;

namespace Shouldly.Configuration
{
    public class FindMethodUsingAttribute<T> : ITestMethodFinder where T : Attribute
    {
        public TestMethodInfo GetTestMethodInfo(StackTrace stackTrace, int startAt = 0)
        {
            var i = startAt;
            StackFrame callingFrame;
            do
            {
                callingFrame = stackTrace.GetFrame(i++)
                    ?? throw new Exception($"Cannot find method in call stack with attribute {typeof(T).FullName}.");

            } while (!callingFrame.GetMethod().IsDefined(typeof(T), true));

            return new TestMethodInfo(callingFrame);
        }
    }
}
#endif