#if ShouldMatchApproved
using System;
using System.Diagnostics;
using System.Linq;

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
                if (i >= stackTrace.FrameCount)
                {
                    throw new Exception($"Cannot find method in call stack with attribute {typeof(T).FullName}");
                }
                callingFrame = stackTrace.GetFrame(i++)!;
            } while (!callingFrame.GetMethod().GetCustomAttributes(typeof(T), true).Any());

            return new TestMethodInfo(callingFrame);
        }
    }
}
#endif