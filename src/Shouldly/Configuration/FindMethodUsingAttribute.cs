using System;
using System.Diagnostics;

namespace Shouldly.Configuration
{
    public class FindMethodUsingAttribute<T> : ITestMethodFinder where T : Attribute
    {
        public TestMethodInfo GetTestMethodInfo(StackTrace stackTrace, int startAt = 0)
        {
            for (var i = startAt; stackTrace.GetFrame(i) is { } frame; i++)
            {
                if (frame.GetMethod() is { } method && method.IsDefined(typeof(T), inherit: true))
                {
                    return new TestMethodInfo(frame);
                }
            }

            throw new InvalidOperationException($"Cannot find a method in the stack trace with attribute {typeof(T).FullName}.");
        }
    }
}