#if ShouldMatchApproved
using System;
using System.Diagnostics;
using System.Reflection;
using System.Linq;

namespace Shouldly.Configuration
{
#if NETSTANDARD2_0
    public class FindMethodUsingAttribute<T> : ITestMethodFinder where T : Attribute
    {
        public TestMethodInfo GetTestMethodInfo(StackTrace stackTrace, int startAt = 0)
        {
            var frames = stackTrace.GetFrames();
            foreach (var frame in frames)
            {
                if (frame.GetMethod().GetType().GetTypeInfo().GetCustomAttributes(typeof(T), true).Any())
                    return new TestMethodInfo(frame);
            }
            throw new Exception($"Cannot find method in call stack with attribute {typeof(T).FullName}");
        }
    }
#else
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
                callingFrame = stackTrace.GetFrame(i++);
            } while (!callingFrame.GetMethod().GetCustomAttributes(typeof(T), true).Any());

            return new TestMethodInfo(callingFrame);
        }
    }
#endif
}
#endif