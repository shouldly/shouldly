using Shouldly.Internals;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Shouldly.Configuration
{
    public class FirstNonShouldlyMethodFinder : ITestMethodFinder
    {
        static readonly Regex AnonMethod = new Regex(@"<(\w|_)+>b_.+");

        /// <summary>
        /// Increasing the offset will move past the first non-shouldly method
        /// Anonymous methods are not counted in the offset.
        /// This is useful when you have created a reusable method which is calling ShouldMatchApproved
        /// </summary>
        public int Offset { get; set; }

        public TestMethodInfo GetTestMethodInfo(StackTrace stackTrace, int startAt = 0)
        {
            foreach (var (i, frame) in stackTrace.GetFrames().AsIndexed().Skip(startAt))
            {
                if (frame.GetMethod() is { } method && !method.IsShouldlyMethod() && !IsCompilerGenerated(method))
                {
                    var callingFrame = stackTrace.GetFrame(i + Offset)
                        ?? throw new InvalidOperationException("There is no stack frame at the specified offset from the first non-Shouldly stack frame.");

                    return new TestMethodInfo(callingFrame);
                }
            }

            throw new InvalidOperationException("Cannot find a non-Shouldly method in the stack trace.");
        }

        static bool IsCompilerGenerated(MethodBase method)
        {
            return method.IsDefined(typeof(CompilerGeneratedAttribute), inherit: true) || AnonMethod.IsMatch(method.Name);
        }
    }
}
